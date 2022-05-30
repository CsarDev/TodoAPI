using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelsPostgres
{
    public class PostgresDbContext: DbContext
    {
        private const string connectionString = "User ID =postgres;Password=postgres;Server=localhost;Port=5432;Database=TodoWork; Integrated Security=true;Pooling=true;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
