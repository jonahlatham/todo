using System;
using System.Collections.Generic;

namespace todo.data
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public bool IsComplete { get; set; }
    }
}
