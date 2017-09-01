using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	public class ShoppingCartDto
	{
		public string ShoppingCartID { get; set; }
		public string UserName { get; set; }
		public string Phone { get; set; }
		public double TotalPrice { get; set; }

		public ProductDto[] Products { get; set; }
	}
}
