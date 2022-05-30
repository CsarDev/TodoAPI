using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public ITodoItemRepository TodoItemRepository { get; private set; }
        public ITodoItemPgRepository TodoItemPgRepository { get; private set; }

        public UnitOfWork(ITodoItemRepository todoItemRepository, ITodoItemPgRepository todoItemPgRepository)
        {
            TodoItemRepository = todoItemRepository;
            TodoItemPgRepository = todoItemPgRepository;
        }
    }
}
