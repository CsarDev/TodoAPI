using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepository<T> where T:class
    {
        T GetByID(int Id);
        T Add(T entity);
        T Update(T entity);

        void Delete(int Id);

        DbSet<T> GetAll();
        void SaveChanges();
    }
}
