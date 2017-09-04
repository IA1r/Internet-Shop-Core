using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interface.Manager;
using Core.Dto;
using Shop.RequestModel;
using Core.Model;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Shop.ResopnseModel;

namespace Shop.Controllers
{
	/// <summary>
	/// Implemets API controller to manage products
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Produces("application/json")]
	[Route("api/ProductAPI/[action]")]
	public class ProductAPIController : Controller
	{
		/// <summary>
		/// The product manager
		/// </summary>
		private IProductManager productManager;

		/// <summary>
		/// The cart manager
		/// </summary>
		private ICartManager cartManager;

		/// <summary>
		/// The response status
		/// </summary>
		private ResponseStatusModel responseStatus;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductAPIController"/> class.
		/// </summary>
		/// <param name="productManager">The product manager.</param>
		/// <param name="cartManager">The cart manager.</param>
		public ProductAPIController(IProductManager productManager, ICartManager cartManager)
		{
			this.productManager = productManager;
			this.cartManager = cartManager;
		}

		/// <summary>
		/// Gets the products.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			ProductDto[] produtcs = await this.productManager.GetProducts();
			this.responseStatus = new ResponseStatusModel { Success = true };
			return Ok(new { ResponseStatus = this.responseStatus, Products = produtcs });
		}

		/// <summary>
		/// Gets the products by specified type.
		/// </summary>
		/// <param name="id">The type identifier.</param>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProducts(int id)
		{
			ProductDto[] produtcs = await this.productManager.GetProducts(id);
			this.responseStatus = new ResponseStatusModel { Success = true };
			return Ok(new { ResponseStatus = this.responseStatus, Products = produtcs });
		}

		/// <summary>
		/// Gets the specified product.
		/// </summary>
		/// <param name="id">The product identifier.</param>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			ProductDto product = await this.productManager.GetProduct(id);
			if (product != null)
			{
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, Product = product });
			}
			else
			{
				this.responseStatus = new ResponseStatusModel { Code = 404, Message = $"Produc with id - {id} not Found", Success = false };
				return NotFound(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Adds the product to shopping cart.
		/// </summary>
		/// <param name="id">The product identifier.</param>
		[HttpPost("{id}")]
		public async Task<IActionResult> AddProduct(int id)
		{
			try
			{
				if (User.Identity.IsAuthenticated)
					await this.cartManager.AddProduct(id, HttpContext.Session.Get<UserDto>("current-user").ID);
				else
				{
					if (HttpContext.Session.Get<string>("guestID") == null)
					{
						string anonymID = Guid.NewGuid().ToString();
						HttpContext.Session.Set<string>("guestID", anonymID);
					}
					await this.cartManager.AddProduct(id, HttpContext.Session.Get<string>("guestID"));
				}

				this.responseStatus = new ResponseStatusModel { Success = true, Message = "Product successfully added to the Shopping Cart" };
				return Ok(new { ResponseStatus = responseStatus });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Code = 404, Message = ex.Message, Success = false };
				return NotFound(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Gets the shopping cart.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetShoppingCart()
		{
			try
			{
				ShoppingCartDto cart;
				if (User.Identity.IsAuthenticated)
				{
					cart = await this.cartManager.GetShoppingCartProducts(HttpContext.Session.Get<UserDto>("current-user").ID);
					if (cart != null)
						cart.UserName = HttpContext.Session.Get<UserDto>("current-user").Name;
				}
				else
				{
					cart = await this.cartManager.GetShoppingCartProducts(HttpContext.Session.Get<string>("guestID"));
					if (cart != null)
						cart.UserName = "Guest";
				}
				if (cart == null)
				{
					this.responseStatus = new ResponseStatusModel { Success = true, Message = "Shopping Cart is empty.", Code = 204 };
					return NoContent();
				}

				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, Cart = cart });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = true, Message = ex.Message, Code = 204 };
				return NoContent();
			}
		}

		/// <summary>
		/// Deletes the product from, shopping cart.
		/// </summary>
		/// <param name="id">The product identifier.</param>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteItem(int id)
		{
			try
			{
				await this.cartManager.DeleteItem(id);
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = ex.Message, Code = 404 };
				return NotFound(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Checkouts from shopping cart.
		/// </summary>
		/// <param name="request">The request.</param>
		[HttpPost]
		public async Task<IActionResult> Checkout([FromBody]CheckoutRequestModel request)
		{
			string guestID = HttpContext.Session.Get<string>("guestID");
			string userID = (HttpContext.Session.Get<UserDto>("current-user"))?.ID;

			try
			{
				await this.cartManager.Checkout(new OrderDto
				{
					UserID = userID,
					GuestID = guestID,
					UserName = request.UserName,
					Phone = request.Phone,
					DeliveryAddress = request.DeliveryAddress,
					TotalPrice = request.TotalPrice
				});

				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus });
			}
			catch (ArgumentException ex)
			{
				return NotFound(new { Success = false, Error = ex.Message });
			}

		}

		/// <summary>
		/// Searches the product by keyword.
		/// </summary>
		/// <param name="keyword">The keyword.</param>
		[HttpGet("{keyword}")]
		public async Task<IActionResult> SearchProduct(string keyword)
		{
			ProductDto[] products = await this.productManager.SearchProducts(keyword);
			if (products != null && products.Length > 0)
			{
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, Products = products });
			}

			this.responseStatus = new ResponseStatusModel { Success = false, Message = "No result." };
			return Ok(new { ResponseStatus = this.responseStatus });
		}
	}
}
