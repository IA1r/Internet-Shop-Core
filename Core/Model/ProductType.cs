using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
	public class ProductType
	{
		public ProductType()
		{
			this.Products = new HashSet<Product>();
		}

		public int ProductTypeID { get; set; }
		public string Value { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
