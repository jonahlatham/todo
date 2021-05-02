namespace todo.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public bool IsComplete { get; set; }
    }

    public class Body
    {
        public string Item { get; set; }
    }
}