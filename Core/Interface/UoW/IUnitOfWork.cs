using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.UoW
{
	public interface IUnitOfWork : IDisposable
	{
		DbSet<T> Set<T>() where T : class;
		EntityState Entry<TEntity>(TEntity entity) where TEntity : class;
		Task<int> SaveAsync();
	}
}
