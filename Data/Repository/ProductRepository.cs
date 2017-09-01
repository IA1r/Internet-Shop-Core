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
	public class ProductRepository : IProductRepository
	{
		private IUnitOfWork unitOfWork;
		public ProductRepository(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

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
