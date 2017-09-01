using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	public class OrderDto
	{
		public int OrderID { get; set; }
		public string UserID { get; set; }
		public string GuestID { get; set; }
		public string UserName { get; set; }
		public string Phone { get; set; }
		public string DeliveryAddress { get; set; }
		public double TotalPrice { get; set; }
		public DateTime Date { get; set; }
		public bool IsApprove { get; set; }
		public ProductDto[] Products { get; set; }
	}
}
