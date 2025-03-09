using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
 
namespace EmployeeManagementSystem.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> _toDoList = new List<ToDoItem>();
 
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_toDoList);
        }
 
        [HttpPost]
        public IActionResult Add([FromBody] ToDoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Task))
            {
                return BadRequest("Task cannot be empty.");
            }
 
            item.Id = _toDoList.Count + 1;
            _toDoList.Add(item);
            return Ok(item);
        }
 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _toDoList.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound("To-Do item not found.");
            }
 
            _toDoList.Remove(item);
            return Ok($"Deleted item with ID {id}");
        }
    }
 
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
    }
}
 