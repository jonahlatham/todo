using System;
using System.Collections.Generic;

namespace toDoApi.Data.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }
    }
}
