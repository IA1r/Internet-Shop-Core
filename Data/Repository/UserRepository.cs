using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Core.Interface.UoW;
using Data.UoW;
using Core.Model;
using Core.Interface.Repository;

namespace Data.Repository
{
	public class UserRepository : IUserRepository
	{
		private IUnitOfWork unitOfWork;
		public UserRepository(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public string[] GetUsers()
		{
			string[] arr = { "", "" };
			return arr;
		}
	}
}
