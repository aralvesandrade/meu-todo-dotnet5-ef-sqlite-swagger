using System.Collections.Generic;
using System.Threading.Tasks;
using MeuTodo.Data;
using MeuTodo.Models;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext dbContext)
        {
            var todos = await dbContext.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }

        [HttpGet("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext dbContext, [FromRoute] int id)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext dbContext, [FromBody] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo {
                Title = model.Title
            };

            try
            {
                await dbContext.AddAsync<Todo>(todo);
                await dbContext.SaveChangesAsync();
                 
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Created($"v1/todos/{todo.Id}", todo);
        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext dbContext, [FromBody] CreateTodoViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            todo.Title = model.Title;

            try
            {
                dbContext.Update<Todo>(todo);
                await dbContext.SaveChangesAsync();
                 
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Ok(todo);
        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext dbContext, [FromRoute] int id)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                dbContext.Remove(todo);
                await dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}