using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo.Models;
using todo.Services;

namespace toDo.Controllers
{
    // [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public List<todo.Models.ToDo> GetTodos()
        {
            var results = _toDoService.GetTodos();
            return results;
        }

        [HttpPost]
        public void PostTodo([FromBody] Body todo)
        {
            _toDoService.PostTodo(todo);
        }

        [HttpDelete]
        public void DeleteTodo([FromQuery] int id)
        {
            _toDoService.DeleteTodo(id);
        }

        [HttpPut]
        public void PutTodoComplete([FromBody] ToDo body)
        {
            _toDoService.PutTodo(body);
        }
    }
}