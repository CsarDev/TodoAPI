using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Repository.UnitOfWork
{
	public class UnityContainerResolver
	{
		private UnityContainer container;

		public UnityContainerResolver()
		{
			container = new UnityContainer();
			RegisterTypes();
		}

		public void RegisterTypes()
		{
			container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));
			container.RegisterType<ITodoItemRepository, TodoItemRepository>();
			container.RegisterType<ITodoItemPgRepository, TodoItemPgRepository>();
			container.AddNewExtension<DependencyOfDependencyExtension>();


			container.RegisterType<IUnitOfWork, UnitOfWork>();
		}

		public UnitOfWork Resolver()
		{
			return container.Resolve<UnitOfWork>();
		}
	}
}
