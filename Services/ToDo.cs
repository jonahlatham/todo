using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.data;
using todo.Models;

namespace todo.Services
{
    public interface IToDoService
    {
        List<todo.Models.ToDo> GetTodos();
        void PostTodo(Body todo);
        void DeleteTodo(int id);
        void PutTodo(Models.ToDo body);
    }
    public class ToDoService : IToDoService
    {
        private CoreContext _context;
        public ToDoService(CoreContext context)
        {
            _context = context;
        }

        public List<todo.Models.ToDo> GetTodos()
        {
            var results = _context.ToDo.Select(e => new todo.Models.ToDo()
            {
                Id = e.Id,
                Item = e.Item,
                IsComplete = e.IsComplete
            }).OrderBy(w => w.Id).ToList();
            return results;
        }
        public void PostTodo(Body todo)
        {
            var results = _context.ToDo.Add(new todo.data.ToDo()
            {
                Item = todo.Item,
                IsComplete = false
            });
            _context.SaveChanges();
        }
        public void DeleteTodo(int id)
        {
            var results = _context.ToDo.SingleOrDefault(e => e.Id == id);
            _context.ToDo.Remove(results);
            _context.SaveChanges();
        }
        public void PutTodo(Models.ToDo body)
        {
            var results = _context.ToDo.Where(e => e.Id == body.Id).SingleOrDefault();
            results.IsComplete = results.IsComplete == true ? false : true;
            if (body.Item != results.Item)
            {
                results.Item = body.Item;
            }
            _context.SaveChanges();
        }
    }
}