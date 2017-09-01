using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.Model
{
	public class User: IdentityUser
	{
		public User()
		{
			this.Orders = new HashSet<Order>();
		}

		public string Country { get; set; }
		public string Avatar { get; set; }
		public string Year { get; set; }
		
		public virtual ICollection<Order> Orders { get; set; }
	}
}
