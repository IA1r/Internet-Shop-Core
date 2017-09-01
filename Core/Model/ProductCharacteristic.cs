using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
	public class ProductCharacteristic
	{
		public int ProductCharacteristicID { get; set; }
		public int ProductID { get; set; }
		public int CharacteristicID { get; set; }
		public string Value { get; set; }

		public virtual Characteristic Characteristic { get; set; }
		public virtual Product Product { get; set; }
	}
}
