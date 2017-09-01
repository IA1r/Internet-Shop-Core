using Core.Dto;
using Core.Interface.Manager;
using Core.Interface.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
	public class ProductManager : IProductManager
	{
		private IProductRepository productRepository;
		public ProductManager(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task<ProductDto[]> GetProducts()
		{
			return await this.productRepository.GetProducts();
		}

		public async Task<ProductDto[]> GetProducts(int productTypeID)
		{
			return await this.productRepository.GetProducts(productTypeID);
		}

		public async Task<ProductDto> GetProduct(int id)
		{
			return await this.productRepository.GetProduct(id);
		}

		public async Task<ProductTypeDto[]> GetProductTypes()
		{
			return await this.productRepository.GetProductTypes();
		}
		public async Task<ProductDto> InitDictionaryFields(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Invalid type ID");

			return await this.productRepository.InitDictionaryFields(id);
		}
		public async Task<int> AddProductToDB(ProductDto productDto)
		{
			if (productDto.ProductTypeID <= 0)
				throw new ArgumentException("Invalid type ID");

			return await this.productRepository.AddProductToDB(productDto);
		}
		public async Task UpdateProduct(ProductDto productDto)
		{
			await this.productRepository.UpdateProduct(productDto);
		}
		public async Task<string> UpdateProductImage(int productID, IFormFile file)
		{
			if (file.Length <= 0 || productID <= 0)
				throw new ArgumentException("Some troublehappened");

			return await this.productRepository.UpdateProductImage(productID, file);
		}

		public async Task<ProductDto[]> SearchProducts(string keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
				return null;

			return await this.productRepository.SearchProducts(keyword);
		}

		public async Task<ProductDto> FindProduct(int productID)
		{
			if (productID <= 0)
				return null;

			return await this.productRepository.FindProduct(productID);
		}
	}
}
