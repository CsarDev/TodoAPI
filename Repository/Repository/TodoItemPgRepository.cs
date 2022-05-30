using Repository.ModelsPostgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TodoItemPgRepository : GenericRepository<TodoItem>, ITodoItemPgRepository
    {
        public TodoItemPgRepository(PostgresDbContext context): base(context)
        {

        }
    }
}
