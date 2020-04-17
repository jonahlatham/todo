namespace toDoApi.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}