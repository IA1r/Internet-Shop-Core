using Core.Interface.UoW;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Dto;
using Core.Interface.Repository;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
	public class CartRepository : ICartRepository
	{
		private IUnitOfWork unitOfWork;
		public CartRepository(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task AddProduct(int productID, string userID)
		{
			if (!await this.unitOfWork.Set<Product>().AnyAsync(p => p.ProductID == productID))
				throw new ArgumentException("Product not found");

			if (!this.unitOfWork.Set<ShoppingCart>().Any(sc => sc.ShoppingCartID == userID))
			{
				ShoppingCart cart = new ShoppingCart { ShoppingCartID = userID };

				await this.unitOfWork.Set<ShoppingCart>().AddAsync(cart);
				await this.unitOfWork.SaveAsync();

				await this.unitOfWork.Set<CartContent>().AddAsync(new CartContent { ShoppingCartID = cart.ShoppingCartID, ProductID = productID });
				await this.unitOfWork.SaveAsync();
			}
			else
			{
				await this.unitOfWork.Set<CartContent>()
				.AddAsync(new CartContent
				{
					ProductID = productID,
					ShoppingCartID = userID
				});
				await this.unitOfWork.SaveAsync();
			}
		}

		public async Task<ShoppingCartDto> GetShoppingCartProducts(string id)
		{
			if (!await this.unitOfWork.Set<ShoppingCart>().AnyAsync(sc => sc.ShoppingCartID == id))
				return null;

			ShoppingCartDto shoppingCart = await this.unitOfWork.Set<ShoppingCart>()
				.Where(sc => sc.ShoppingCartID == id)
				.Select(sc => new ShoppingCartDto
				{
					ShoppingCartID = sc.ShoppingCartID,
					Products = sc.CartContents.Select(p => new ProductDto
					{
						ProductID = p.ProductID,
						CartContentID = p.CartContentID,
						Type = p.Product.ProductType.Value,
						Characteristic = p.Product.ProductCharacteristics
						.Select(pc => new
						{
							Key = pc.Characteristic.Name,
							Value = pc.Value
						}).ToDictionary(d => d.Key, d => d.Value)
					}).ToArray()
				}).SingleOrDefaultAsync();

			double totalPrice = 0.0;
			foreach (ProductDto product in shoppingCart.Products)
			{
				totalPrice += Convert.ToDouble(product.Characteristic["Price"], CultureInfo.InvariantCulture);
			}
			shoppingCart.TotalPrice = totalPrice;

			return shoppingCart;
		}

		public async Task DeleteItem(int cartContentID)
		{
			if (!await this.unitOfWork.Set<CartContent>().AnyAsync(cc => cc.CartContentID == cartContentID))
				throw new ArgumentException("Item not found");

			CartContent cartItem = await this.unitOfWork.Set<CartContent>().FindAsync(cartContentID);
			this.unitOfWork.Set<CartContent>().Remove(cartItem);
			await this.unitOfWork.SaveAsync();
		}

		public async Task Checkout(OrderDto order)
		{
			await this.unitOfWork.Set<Order>().AddAsync(new Order
			{
				UserID = order.UserID,
				GuestID = order.GuestID,
				UserName = order.UserName,
				Phone = order.Phone,
				DeliveryAddress = order.DeliveryAddress,
				TotalPrice = order.TotalPrice,
				Date = DateTime.Now
			});

			await this.unitOfWork.SaveAsync();

			int orderID = (await this.unitOfWork.Set<Order>().LastOrDefaultAsync(or => or.UserID == order.UserID && or.GuestID == order.GuestID)).OrderID;

			ShoppingCart cart = await this.unitOfWork.Set<ShoppingCart>().FirstOrDefaultAsync(cc => cc.ShoppingCartID == order.UserID || cc.ShoppingCartID == order.GuestID);

			CartContent[] cartContents = await this.unitOfWork.Set<CartContent>()
									.Where(cc => cc.ShoppingCartID == cart.ShoppingCartID)
									.ToArrayAsync();

			foreach (CartContent item in cartContents)
			{
				await this.unitOfWork.Set<OrderContent>().AddAsync(new OrderContent { OrderID = orderID, ProductID = item.ProductID });
			}

			foreach (CartContent cartContent in cartContents)
			{
				this.unitOfWork.Set<CartContent>().Remove(cartContent);
			}

			this.unitOfWork.Set<ShoppingCart>().Remove(cart);

			await this.unitOfWork.SaveAsync();
		}
	}
}
