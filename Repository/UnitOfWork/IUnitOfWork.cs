using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItemRepository { get; }
        ITodoItemPgRepository TodoItemPgRepository { get; }
    }
}
