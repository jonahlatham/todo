namespace toDoApi.Data.Entities
{
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength (10, MinimumLength = 4, ErrorMessage = "Your password must be between 4-10 Characters")]
        public string Password { get; set; }
    }
}