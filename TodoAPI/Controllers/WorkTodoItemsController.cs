using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTodoItemsController : ControllerBase
    {
        private static IUnitOfWork _unitOfWork;
        private static UnityContainerResolver _resolver;

        public WorkTodoItemsController() 
        {
            _resolver = new UnityContainerResolver();
        }
        // GET: api/<WorkTodoItemsController>
        [HttpGet]
        public async Task<IEnumerable<TodoItemDTO>> Get()
        {
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();

            return await _unitOfWork.TodoItemPgRepository.GetAll().Select(x => ItemToDTOPg(x)).ToListAsync();
        }

        // GET api/<WorkTodoItemsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> Get(int id)
        {
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItem = _unitOfWork.TodoItemPgRepository.GetByID(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTOPg(todoItem);
        }

        // POST api/<WorkTodoItemsController>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> Post(TodoItemDTO todoItemDTO)
        {
            var todoItem = new Repository.ModelsPostgres.TodoItem
            {
                Id = todoItemDTO.Id,
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItemAdded = _unitOfWork.TodoItemPgRepository.Add(todoItem);

            return CreatedAtAction(
                nameof(Get),
                new { id = todoItemAdded.Id },
                ItemToDTOPg(todoItemAdded));
        }

        // PUT api/<WorkTodoItemsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var oldEntity = _unitOfWork.TodoItemPgRepository.GetByID(id);
            if (oldEntity == null)
            {
                return NotFound();
            }
            oldEntity.Id = todoItemDTO.Id;
            oldEntity.Name = todoItemDTO.Name;
            oldEntity.IsComplete = todoItemDTO.IsComplete;

            var todoItem = _unitOfWork.TodoItemPgRepository.Update(oldEntity);

            if (todoItem == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<WorkTodoItemsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItem = _unitOfWork.TodoItemPgRepository.GetByID(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _unitOfWork.TodoItemPgRepository.Delete(id);

            return NoContent();
        }

        private static TodoItemDTO ItemToDTOPg(Repository.ModelsPostgres.TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
