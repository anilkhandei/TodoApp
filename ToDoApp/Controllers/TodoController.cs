using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL;
using ToDoApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoAppContext _context;

        public TodoController(TodoAppContext context)
        {
            _context = context;
            if (_context.todoItems.Count() == 0)
            {
                _context.todoItems.Add(new TodoItem { Name = "Get Groceries" });
                _context.SaveChanges();
            }
        }


        //GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetoDoItems()
        {
            return await _context.todoItems.ToListAsync();
        }

        //GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoitem = await _context.todoItems.FindAsync(id);

            if (todoitem == null)
                return NotFound();

            return todoitem;
        }

        //POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.todoItems.Add(item);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        //PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveTodoItem(long id, TodoItem item)
        {
            if (item.Id != id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTodoItem(long id)
        {
            var todoitem = await _context.todoItems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            _context.todoItems.Remove(todoitem);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
