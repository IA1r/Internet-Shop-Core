using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
	public class Product
	{
		public Product()
		{
			this.ProductCharacteristics = new HashSet<ProductCharacteristic>();
		}
		public int ProductID { get; set; }
		public int ProductTypeID { get; set; }

		public virtual ProductType ProductType { get; set; }
		public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
	}
}
