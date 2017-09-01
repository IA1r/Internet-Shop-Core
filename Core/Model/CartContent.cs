using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
	public class CartContent
	{
		public int CartContentID { get; set; }
		public int ProductID { get; set; }
		public string ShoppingCartID { get; set; }

		public virtual Product Product { get; set; }
		public virtual ShoppingCart ShoppingCart { get; set; }
	}
}
