using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
	public class Order
	{
		public Order()
		{
			this.OrderContents = new HashSet<OrderContent>();
		}

		public int OrderID { get; set; }
		public string UserID { get; set; }
		public string GuestID { get; set; }
		public string UserName { get; set; }
		public string Phone { get; set; }
		public string DeliveryAddress { get; set; }
		public double TotalPrice { get; set; }
		public bool IsApprove { get; set; }
		public DateTime Date { get; set; }

		public virtual User User { get; set; }
		public virtual ICollection<OrderContent> OrderContents { get; set; }
	}
}
