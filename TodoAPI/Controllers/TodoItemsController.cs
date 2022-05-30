using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Repository.UnitOfWork;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        //private readonly Repository.Models.TodoHomeContext _context;

        private static IUnitOfWork _unitOfWork;
        private static UnityContainerResolver _resolver;

        public TodoItemsController(/*Repository.Models.TodoHomeContext context*/)
        {
            _resolver = new UnityContainerResolver();
            //_context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            //return await _context.TodoItems
            //    .Select(x => ItemToDTO(x))
            //    .ToListAsync();
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            //var a =_unitOfWork.TodoItemPgRepository.GetAll().Select(x => ItemToDTOPg(x)).ToListAsync();
            //return await _unitOfWork.TodoItemPgRepository.GetAll().Select(x => ItemToDTOPg(x)).ToListAsync();

            return await _unitOfWork.TodoItemRepository.GetAll().Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItem = _unitOfWork.TodoItemRepository.GetByID(id);
            //var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var oldEntity = _unitOfWork.TodoItemRepository.GetByID(id);
            if (oldEntity == null)
            {
                return NotFound();
            }
            oldEntity.Id = todoItemDTO.Id;
            oldEntity.Name = todoItemDTO.Name;
            oldEntity.IsComplete = todoItemDTO.IsComplete;


            var todoItem = _unitOfWork.TodoItemRepository.Update(oldEntity);

            //var todoItem = await _context.TodoItems.FindAsync(id);
            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            //todoItem.Name = todoItemDTO.Name;
            //todoItem.IsComplete = todoItemDTO.IsComplete;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            //{
            //    return NotFound();/
            //}
            if(todoItem == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new Repository.Models.TodoItem
            {
                Id = todoItemDTO.Id,
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            //_context.TodoItems.Add(todoItem);
            //await _context.SaveChangesAsync();

            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItemAdded = _unitOfWork.TodoItemRepository.Add(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItemAdded.Id },
                ItemToDTO(todoItemAdded));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            //var todoItem = await _context.TodoItems.FindAsync(id);
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItem = _unitOfWork.TodoItemRepository.GetByID(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _unitOfWork.TodoItemRepository.Delete(id);

            return NoContent();
        }

        private bool TodoItemExists(int id)
        {
            _unitOfWork = (IUnitOfWork)_resolver.Resolver();
            var todoItem = _unitOfWork.TodoItemRepository.GetByID(id);
            return !(todoItem == null);
        }

        private static TodoItemDTO ItemToDTO(Repository.Models.TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        private static TodoItemDTO ItemToDTOPg (Repository.ModelsPostgres.TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        private static Repository.Models.TodoItem DTOToItem(TodoItemDTO todoItemDTO) =>
            new Repository.Models.TodoItem
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete,
            };
    }
}