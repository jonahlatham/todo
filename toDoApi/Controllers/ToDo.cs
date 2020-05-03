using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using toDoApi.Data;
using toDoApi.Models;

namespace toDoApi.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class ToDoController : ControllerBase
    {

        private readonly ILogger<ToDoController> _logger;
        private CoreContext _context;

        public ToDoController (ILogger<ToDoController> logger, CoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public List<ToDo> Get ()
        {
            var id = int.Parse (User.Claims.FirstOrDefault (i => i.Type == "id").Value);
            return _context.ToDo.Where (e => e.UserId == id).Select (e => new ToDo ()
            {
                Id = e.Id,
                    Name = e.Name,
                    IsComplete = Convert.ToBoolean (e.IsComplete)
            }).OrderBy (x => x.Id).ToList ();
        }

        [HttpPost]
        public List<ToDo> posty (ToDo toodles)
        {
            var newTodo = new Data.Entities.ToDo ()
            {
                Name = toodles.Name,
                IsComplete = toodles.IsComplete
            };
            _context.ToDo.Add (newTodo);
            _context.SaveChanges ();
            return _context.ToDo.Select (e => new ToDo ()
            {
                Id = e.Id,
                    Name = e.Name,
                    IsComplete = Convert.ToBoolean (e.IsComplete)
            }).OrderBy (x => x.Id).ToList ();
        }

        [HttpDelete]
        [Route ("{id}")]
        public List<ToDo> exterminator (int id)
        {
            var todo = _context.ToDo.Single (e => e.Id == id);
            _context.ToDo.Remove (todo);

            _context.SaveChanges ();
            return _context.ToDo.Select (e => new ToDo ()
            {
                Id = e.Id,
                    Name = e.Name,
                    IsComplete = Convert.ToBoolean (e.IsComplete)
            }).OrderBy (x => x.Id).ToList ();
        }

        [HttpPut]
        public List<ToDo> theTurnTables (ToDo abc)
        {
            var std = _context.ToDo.SingleOrDefault (x => x.Id == abc.Id);
            std.Name = abc.Name != std.Name && abc.Name != null ? abc.Name : std.Name;
            std.IsComplete = abc.IsComplete != std.IsComplete && abc.IsComplete != null ? abc.IsComplete : std.IsComplete;
            _context.SaveChanges ();
            return _context.ToDo.Select (e => new ToDo ()
            {
                Id = e.Id,
                    Name = e.Name,
                    IsComplete = Convert.ToBoolean (e.IsComplete)
            }).OrderBy (x => x.Id).ToList ();
        }
    }
}