using System;
using System.Collections.Generic;

namespace toDoApi.Data.Entities
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsComplete { get; set; }
    }
}
