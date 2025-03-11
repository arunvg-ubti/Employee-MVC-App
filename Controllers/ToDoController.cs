using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> toDoList = new List<ToDoItem>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("DEBUG: ToDoController GET called");
            return Ok(toDoList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ToDoItem item)
        {
            Console.WriteLine($"DEBUG: ToDoController POST called with task: {item.Task}");
            if (item == null || string.IsNullOrEmpty(item.Task))
            {
                return BadRequest("Task cannot be null or empty.");
            }
            item.Id = nextId++;
            toDoList.Add(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine($"DEBUG: ToDoController DELETE called for id: {id}");
            var item = toDoList.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            toDoList.Remove(item);
            return Ok();
        }
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
    }
}