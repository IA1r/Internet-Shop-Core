using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.RequestModel
{
    public class CheckoutRequestModel
    {
		public string UserName { get; set; }
		public string Phone { get; set; }
		public string DeliveryAddress { get; set; }
		public double TotalPrice { get; set; }

	}
}
