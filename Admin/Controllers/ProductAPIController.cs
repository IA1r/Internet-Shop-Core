using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interface.Manager;
using Core.Dto;
using Admin.RequestModel;
using System.IO;
using Core.GoogleAPI;
using Admin.ResopnseModel;

namespace Admin.Controllers
{
	/// <summary>
	/// Implemets API controller to manage produtcs
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
		/// The response status
		/// </summary>
		private ResponseStatusModel responseStatus;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductAPIController"/> class.
		/// </summary>
		/// <param name="productManager">The product manager.</param>
		public ProductAPIController(IProductManager productManager)
		{
			this.productManager = productManager;
		}

		/// <summary>
		/// Gets the all of products from database
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
		/// Gets the products by type.
		/// </summary>
		/// <param name="productTypeID">The product type identifier.</param>
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
		/// <param name="id">The identifier.</param>
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
		/// Gets the product types.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetProductTypes()
		{
			ProductTypeDto[] types = await this.productManager.GetProductTypes();
			this.responseStatus = new ResponseStatusModel { Success = true };
			return Ok(new { ResponseStatus = this.responseStatus, Types = types });

		}

		/// <summary>
		/// Initializes the dictionary fields.
		/// </summary>
		/// <param name="id">The identifier.</param>
		[HttpGet("{id}")]
		public async Task<IActionResult> InitDictionaryFields(int id)
		{
			try
			{
				ProductDto emptyProduct = await this.productManager.InitDictionaryFields(id);
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, Product = emptyProduct });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = ex.Message, Code = 400 };
				return BadRequest(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Adds the product to database.
		/// </summary>
		/// <param name="request">The request.</param>
		[HttpPost]
		public async Task<IActionResult> AddProductToDB([FromBody]ProductRequestModel request)
		{
			try
			{
				int productID = await this.productManager.AddProductToDB(new ProductDto
				{
					ProductTypeID = request.ProductTypeID,
					Characteristic = request.Characteristic
				});

				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, ProducID = productID });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = ex.Message, Code = 400 };
				return BadRequest(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Updates the product.
		/// </summary>
		/// <param name="request">The request.</param>
		[HttpPost]
		public async Task<IActionResult> UpdateProduct([FromBody]ProductRequestModel request)
		{
			try
			{
				await this.productManager.UpdateProduct(new ProductDto
				{
					ProductID = request.ProductID,
					ProductTypeID = request.ProductTypeID,
					Characteristic = request.Characteristic
				});

				this.responseStatus = new ResponseStatusModel { Success = true, Message = "Data successfully updated" };
				return Ok(new { ResponseStatus = this.responseStatus });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = ex.Message, Code = 400 };
				return BadRequest(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Uploads product image.
		/// </summary>
		/// <param name="id">The product identifier.</param>
		/// <param name="file">The file.</param>
		[HttpPost("{id}")]
		public async Task<IActionResult> ImageUpload(int id, IFormFile file)
		{
			try
			{
				string imageID = await this.productManager.UpdateProductImage(id, file);
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, ImageID = imageID });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = ex.Message, Code = 400 };
				return BadRequest(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Finds the product.
		/// </summary>
		/// <param name="id">The product identifier.</param>
		[HttpGet("{id}")]
		public async Task<IActionResult> FindProduct(int id)
		{
			ProductDto product = await this.productManager.FindProduct(id);
			if (product != null)
			{
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, Product = product });
			}

			this.responseStatus = new ResponseStatusModel { Success = false, Message = "No result." };
			return Ok(new { ResponseStatus = this.responseStatus });
		}

		/// <summary>
		/// Searches the product.
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
