using Core.Interface.UoW;
using Core.Interface.Context;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.UoW
{
	public class UnitOfWork : IUnitOfWork
	{
		private InternetShopContext context;
		private bool isDisposed;

		public UnitOfWork(IInternetShopContext context)
		{
			this.context = context as InternetShopContext;
		}

		public DbSet<T> Set<T>() where T : class
		{
			return this.context.Set<T>();

		}


		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					this.context.Dispose();
				}
			}
			this.isDisposed = true;
		}


		public async Task<int> SaveAsync()
		{
			return await this.context.SaveChangesAsync();
		}

		public EntityState Entry<TEntity>(TEntity entity) where TEntity : class
		{
			return this.context.Entry(entity).State = EntityState.Modified;
		}
	}
}
