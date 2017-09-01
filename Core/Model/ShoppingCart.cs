using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
	public class ShoppingCart
	{
		public ShoppingCart()
		{
			this.CartContents = new HashSet<CartContent>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string ShoppingCartID { get; set; }

		public virtual ICollection<CartContent> CartContents { get; set; }
	}
}
