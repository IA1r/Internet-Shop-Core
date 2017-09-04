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
	/// <summary>
	/// Implements functionality to manage products
	/// </summary>
	/// <seealso cref="Core.Interface.Manager.IProductManager" />
	public class ProductManager : IProductManager
	{
		/// <summary>
		/// The product repository
		/// </summary>
		private IProductRepository productRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductManager"/> class.
		/// </summary>
		/// <param name="productRepository">The product repository.</param>
		public ProductManager(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		/// <summary>
		/// Gets the all of products from database.
		/// </summary>
		/// <returns>Array of products</returns>
		public async Task<ProductDto[]> GetProducts()
		{
			return await this.productRepository.GetProducts();
		}

		/// <summary>
		/// Gets the products by type.
		/// </summary>
		/// <param name="productTypeID">The product type identifier.</param>
		/// <returns>Array of products</returns>
		public async Task<ProductDto[]> GetProducts(int productTypeID)
		{
			return await this.productRepository.GetProducts(productTypeID);
		}

		/// <summary>
		/// Gets the specified product.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Product</returns>
		public async Task<ProductDto> GetProduct(int id)
		{
			return await this.productRepository.GetProduct(id);
		}

		/// <summary>
		/// Gets the all of product types.
		/// </summary>
		/// <returns>Array of product types</returns>
		public async Task<ProductTypeDto[]> GetProductTypes()
		{
			return await this.productRepository.GetProductTypes();
		}

		/// <summary>
		/// Initializes the dictionary fields for client side.
		/// </summary>
		/// <param name="id">The product type identifier.</param>
		/// <returns>Empty product for specified type</returns>
		public async Task<ProductDto> InitDictionaryFields(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Invalid type ID");

			return await this.productRepository.InitDictionaryFields(id);
		}

		/// <summary>
		/// Adds the product to database.
		/// </summary>
		/// <param name="productDto">The product dto.</param>
		/// <returns>The product identifier</returns>
		/// <exception cref="ArgumentException">Invalid type ID</exception>
		public async Task<int> AddProductToDB(ProductDto productDto)
		{
			if (productDto.ProductTypeID <= 0)
				throw new ArgumentException("Invalid type ID");

			return await this.productRepository.AddProductToDB(productDto);
		}

		/// <summary>
		/// Updates the product.
		/// </summary>
		/// <param name="productDto">The product dto.</param>
		public async Task UpdateProduct(ProductDto productDto)
		{
			await this.productRepository.UpdateProduct(productDto);
		}

		/// <summary>
		/// Updates the product image.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <param name="file">The image file.</param>
		/// <returns>The image identifier</returns>
		/// <exception cref="ArgumentException">Some troublehappened</exception>
		public async Task<string> UpdateProductImage(int productID, IFormFile file)
		{
			if (file.Length <= 0 || productID <= 0)
				throw new ArgumentException("Some troublehappened");

			return await this.productRepository.UpdateProductImage(productID, file);
		}

		/// <summary>
		/// Searches the products by keyword.
		/// </summary>
		/// <param name="keyword">The keyword.</param>
		/// <returns>Array of products</returns>
		public async Task<ProductDto[]> SearchProducts(string keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
				return null;

			return await this.productRepository.SearchProducts(keyword);
		}

		/// <summary>
		/// Finds the product by identifier.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <returns>Product</returns>
		public async Task<ProductDto> FindProduct(int productID)
		{
			if (productID <= 0)
				return null;

			return await this.productRepository.FindProduct(productID);
		}
	}
}
