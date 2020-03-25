using System;
using System.Collections.Generic;

namespace toDoApi.Data.Entities
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}