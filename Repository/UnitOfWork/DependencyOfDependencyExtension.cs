using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.ModelsPostgres;
using Unity;
using Unity.Extension;

namespace Repository.UnitOfWork
{
    public class DependencyOfDependencyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<DbContext, TodoHomeContext>();
            Container.RegisterType<DbContext, PostgresDbContext >();
        }
    }
}
