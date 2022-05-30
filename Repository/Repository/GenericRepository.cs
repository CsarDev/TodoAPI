using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
	public class GenericRepository<T> : IRepository<T> where T : class
	{
		public DbContext _context { get; set; }

		public GenericRepository(DbContext context)
		{
			this._context = context;
		}

		public T GetByID(int Id)
		{
			return _context.Set<T>().Find(Id);
		}

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            this.SaveChanges();
            return entity;
        }

        public void Delete(int Id)
        {
            var entity =  _context.Set<T>().Find(Id);

            if (entity == null)
            {
                return ;
            }

            _context.Set<T>().Remove(entity);

            this.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T Update(T entity)
        {           
            _context.Set<T>().Update(entity);
            this.SaveChanges();
            return entity;
        }

        public DbSet<T> GetAll()
        {
            return _context.Set<T>();
        }
    }
}
