using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using toDoApi.Data;
using toDoApi.Models;

namespace toDoApi.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;
        private CoreContext _context;

        private readonly IConfiguration _config;

        public AuthenticationController (ILogger<AuthenticationController> logger, CoreContext context, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _context = context;
        }

        // https://localhost:5001/authentication/register
        [HttpPost ("register")]
        public IActionResult Register (Register register)
        {
            register.Email = register.Email.ToLower ();

            if (UserExists (register.Email))
                throw (new Exception ("Email already exists"));

            var userToCreate = new Data.Entities.User
            {
                Email = register.Email
            };

            byte[] passwordHash, passwordSalt;
            CreatePassowordHash (register.Password, out passwordHash, out passwordSalt);

            userToCreate.Password = passwordHash;
            userToCreate.Salt = passwordSalt;

            _context.User.Add (userToCreate);
            _context.SaveChanges ();

            return StatusCode (201);
        }

        public bool UserExists (string email)
        {
            if (_context.User.Any (x => x.Email == email))
                return true;

            return false;
        }

        private void CreatePassowordHash (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }

        // https://localhost:5001/authentication/login
        [HttpPost ("login")]
        public IActionResult Login (User user)
        {
            var youzer = _context.User.FirstOrDefault (x => x.Email == user.Email);

            if (youzer == null)
                return null;

            if (!VerifyPasswordHash (user.Password, youzer.Password, youzer.Salt))
                return null;

            var userFromRepo = new User ()
            {
                Id = youzer.Id,
                Email = youzer.Email
            };
            if (userFromRepo == null)
                return Unauthorized ();

            var claims = new []
            {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString ()),
                new Claim (ClaimTypes.Name, userFromRepo.Email)
            };
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config.GetSection ("AppSettings:Token").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();
            var token = tokenHandler.CreateToken (tokenDescriptor);

            return Ok (new
            {
                token = tokenHandler.WriteToken (token)
            });
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt))
            {
                var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++)
                {

                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}