using Core.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
	/// <summary>
	/// Represents methods for ProductRepository. 
	/// </summary>
	public interface IProductRepository
	{
		/// <summary>
		/// Gets the all of products from database.
		/// </summary>
		/// <returns>Array of products</returns>
		Task<ProductDto[]> GetProducts();

		/// <summary>
		/// Gets the products by type.
		/// </summary>
		/// <param name="productTypeID">The product type identifier.</param>
		/// <returns>Array of products</returns>
		Task<ProductDto[]> GetProducts(int productTypeID);

		/// <summary>
		/// Gets the specified product.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Product</returns>
		Task<ProductDto> GetProduct(int id);

		/// <summary>
		/// Gets the all of product types.
		/// </summary>
		/// <returns>Array of product types</returns>
		Task<ProductTypeDto[]> GetProductTypes();

		//// <summary>
		/// Initializes the dictionary fields for client side.
		/// </summary>
		/// <param name="id">The product type identifier.</param>
		/// <returns>Empty product for specified type</returns>
		Task<ProductDto> InitDictionaryFields(int id);

		/// <summary>
		/// Adds the product to database.
		/// </summary>
		/// <param name="productDto">The product dto.</param>
		/// <returns>The product identifier</returns>
		Task<int> AddProductToDB(ProductDto productDto);

		/// <summary>
		/// Updates the product.
		/// </summary>
		/// <param name="productDto">The product dto.</param>
		Task UpdateProduct(ProductDto productDto);

		/// <summary>
		/// Updates the product image.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <param name="file">The image file.</param>
		/// <returns>The image identifier</returns>
		Task<string> UpdateProductImage(int productID, IFormFile file);

		/// <summary>
		/// Searches the products by keyword.
		/// </summary>
		/// <param name="keyword">The keyword.</param>
		/// <returns>Array of products</returns>
		Task<ProductDto[]> SearchProducts(string keyword);

		/// <summary>
		/// Finds the product by identifier.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <returns>Product</returns>
		Task<ProductDto> FindProduct(int productID);
	}
}
