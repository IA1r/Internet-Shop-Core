using Core.Interface.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.Interface.Repository;
using Data.Repository;

namespace Business.Manager
{
	public class UserManager : IUserManager
	{
		private IUserRepository userRepository;
		public UserManager(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public string[] GetUsers()
		{
			return userRepository.GetUsers();
		}
	}
}
