using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
	public class Characteristic
	{
		public Characteristic()
		{
			this.ProductCharacteristics = new HashSet<ProductCharacteristic>();
		}

		public int CharacteristicID { get; set; }
		public string Name { get; set; }

		public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
	}
}
