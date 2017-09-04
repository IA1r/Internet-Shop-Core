using Core.Interface.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Dto;
using Core.Model;
using Core.Interface.Repository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Core.GoogleAPI;

namespace Data.Repository
{
	/// <summary>
	/// Implements repository for products.
	/// </summary>
	/// <seealso cref="Core.Interface.Repository.IProductRepository" />
	public class ProductRepository : IProductRepository
	{
		/// <summary>
		/// The unit of work
		/// </summary>
		private IUnitOfWork unitOfWork;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductRepository"/> class.
		/// </summary>
		/// <param name="unitOfWork">The unit of work.</param>
		public ProductRepository(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Gets the all of products from database.
		/// </summary>
		/// <returns>Array of products</returns>
		public async Task<ProductDto[]> GetProducts()
		{
			ProductDto[] products = await this.unitOfWork.Set<Product>()
					.Select(p => new ProductDto
					{
						ProductID = p.ProductID,
						Type = p.ProductType.Value,
						Characteristic = p.ProductCharacteristics
						.Where(ch => ch.Characteristic.Name == "Name" || ch.Characteristic.Name == "Price" ||
									ch.Characteristic.Name == "Image" || ch.Characteristic.Name == "Genre")
						.Select(pc => new
						{
							Key = pc.Characteristic.Name,
							Value = pc.Value
						})
						.ToDictionary(d => d.Key, d => d.Value)
					})
					.ToArrayAsync();

			return products;
		}

		/// <summary>
		/// Gets the products by type.
		/// </summary>
		/// <param name="productTypeID">The product type identifier.</param>
		/// <returns>Array of products</returns>
		public async Task<ProductDto[]> GetProducts(int productTypeID)
		{
			ProductDto[] products = await this.unitOfWork.Set<Product>()
					.Where(p => p.ProductTypeID == productTypeID)
					.Select(p => new ProductDto
					{
						ProductID = p.ProductID,
						Type = p.ProductType.Value,
						Characteristic = p.ProductCharacteristics
						.Where(ch => ch.Characteristic.Name == "Name" || ch.Characteristic.Name == "Price" ||
									ch.Characteristic.Name == "Image" || ch.Characteristic.Name == "Genre")
						.Select(pc => new
						{
							Key = pc.Characteristic.Name,
							Value = pc.Value
						})
						.ToDictionary(d => d.Key, d => d.Value)
					})
					.ToArrayAsync();

			return products;
		}

		/// <summary>
		/// Gets the specified product.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Product</returns>
		public async Task<ProductDto> GetProduct(int id)
		{
			if (!await this.unitOfWork.Set<Product>().AnyAsync(p => p.ProductID == id))
				return null;

			ProductDto product = await this.unitOfWork.Set<Product>()
				.Where(p => p.ProductID == id)
				.Select(p => new ProductDto
				{
					ProductID = p.ProductID,
					Type = p.ProductType.Value,
					Characteristic = p.ProductCharacteristics
					.Select(pc => new
					{
						Key = pc.Characteristic.Name,
						Value = pc.Value
					})
					.ToDictionary(d => d.Key, d => d.Value)
				})
				.SingleOrDefaultAsync();

			return product;
		}

		/// <summary>
		/// Gets the all of product types.
		/// </summary>
		/// <returns>Array of product types</returns>
		public async Task<ProductTypeDto[]> GetProductTypes()
		{
			ProductTypeDto[] types = await this.unitOfWork.Set<ProductType>()
				.Select(pt => new ProductTypeDto
				{
					ProductTypeID = pt.ProductTypeID,
					Value = pt.Value
				}).ToArrayAsync();

			return types;
		}

		/// <summary>
		/// Initializes the dictionary fields for client side.
		/// </summary>
		/// <param name="id">The product type identifier.</param>
		/// <returns>Empty product for specified type</returns>
		public async Task<ProductDto> InitDictionaryFields(int id)
		{
			ProductDto product = await this.unitOfWork.Set<Product>()
				.Where(p => p.ProductTypeID == id)
				.Select(p => new ProductDto
				{
					Type = null,
					Characteristic = p.ProductCharacteristics
					.Select(pc => new
					{
						Key = pc.Characteristic.Name,
						Value = ""
					})
					.ToDictionary(d => d.Key, d => d.Value)
				})
				.FirstOrDefaultAsync();

			return product;
		}

		/// <summary>
		/// Adds the product to database.
		/// </summary>
		/// <param name="productDto">The product dto.</param>
		/// <returns>The product identifier</returns>
		public async Task<int> AddProductToDB(ProductDto productDto)
		{
			Product product = new Product { ProductTypeID = productDto.ProductTypeID };
			await this.unitOfWork.Set<Product>().AddAsync(product);
			await this.unitOfWork.SaveAsync();


			int[] characteristicIDs = await this.unitOfWork.Set<ProductCharacteristic>()
				.Where(pc => pc.Product.ProductTypeID == product.ProductTypeID)
				.Select(c => c.CharacteristicID).ToArrayAsync();

			foreach (var item in productDto.Characteristic.Select((characteristic, i) => new { characteristic.Key, i }))
			{
				this.unitOfWork.Set<ProductCharacteristic>().Add(new ProductCharacteristic
				{
					CharacteristicID = characteristicIDs[item.i],
					ProductID = product.ProductID,
					Value = productDto.Characteristic[item.Key]
				});
			};
			await this.unitOfWork.SaveAsync();

			return product.ProductID;
		}

		/// <summary>
		/// Updates the product.
		/// </summary>
		/// <param name="productDto">The product dto.</param>
		/// <exception cref="ArgumentException">Product not found</exception>
		public async Task UpdateProduct(ProductDto productDto)
		{
			if (!await this.unitOfWork.Set<Product>().AnyAsync(p => p.ProductID == productDto.ProductID))
				throw new ArgumentException("Product not found");

			ProductCharacteristic[] productCharacteristics = await this.unitOfWork.Set<ProductCharacteristic>()
				.Where(pc => pc.ProductID == productDto.ProductID)
				.ToArrayAsync();

			foreach (var item in productDto.Characteristic.Select((characteristic, i) => new { characteristic.Key, i }))
			{
				productCharacteristics[item.i].Value = productDto.Characteristic[item.Key];
			};

			await this.unitOfWork.SaveAsync();
		}

		/// <summary>
		/// Updates the product image.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <param name="file">The image file.</param>
		/// <returns>The image identifier</returns>
		public async Task<string> UpdateProductImage(int productID, IFormFile file)
		{
			string imageID = null;

			using (var fileStream = file.OpenReadStream())
			using (var ms = new MemoryStream())
			{
				fileStream.CopyTo(ms);
				imageID = DriveAPI.UploadImage(ms, file.FileName);
			}

			ProductCharacteristic characteristic = await this.unitOfWork.Set<ProductCharacteristic>()
				.Where(pc => pc.ProductID == productID && pc.Characteristic.Name == "Image")
				.FirstOrDefaultAsync();

			DriveAPI.DeleteFile(characteristic.Value);
			characteristic.Value = imageID;

			await this.unitOfWork.SaveAsync();

			return imageID;
		}

		/// <summary>
		/// Searches the products by keyword.
		/// </summary>
		/// <param name="keyword">The keyword.</param>
		/// <returns>Array of products</returns>
		public async Task<ProductDto[]> SearchProducts(string keyword)
		{
			ProductDto[] produtcs = await this.unitOfWork.Set<ProductCharacteristic>()
				.Where(pc => pc.Characteristic.Name == "Name" && pc.Value.Contains(keyword))
				.Select(p => new ProductDto
				{
					ProductID = p.ProductID,
					Type = p.Product.ProductType.Value,
					Characteristic = p.Product.ProductCharacteristics
					.Select(pc => new
					{
						Key = pc.Characteristic.Name,
						Value = pc.Value
					})
					.ToDictionary(d => d.Key, d => d.Value)
				}).ToArrayAsync();

			return produtcs;
		}

		/// <summary>
		/// Finds the product by identifier.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <returns>Product</returns>
		public async Task<ProductDto> FindProduct(int productID)
		{
			ProductDto produtc = await this.unitOfWork.Set<Product>()
				.Where(p => p.ProductID == productID)
				.Select(p => new ProductDto
				{
					ProductID = p.ProductID,
					Type = p.ProductType.Value,
					Characteristic = p.ProductCharacteristics
					.Select(pc => new
					{
						Key = pc.Characteristic.Name,
						Value = pc.Value
					})
					.ToDictionary(d => d.Key, d => d.Value)
				}).SingleOrDefaultAsync();

			return produtc;
		}
	}
}
