using Core.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
	public interface IProductRepository
	{
		Task<ProductDto[]> GetProducts();
		Task<ProductDto[]> GetProducts(int productTypeID);
		Task<ProductDto> GetProduct(int id);
		Task<ProductTypeDto[]> GetProductTypes();
		Task<ProductDto> InitDictionaryFields(int id);
		Task<int> AddProductToDB(ProductDto productDto);
		Task UpdateProduct(ProductDto productDto);
		Task<string> UpdateProductImage(int productID, IFormFile file);
		Task<ProductDto[]> SearchProducts(string keyword);
		Task<ProductDto> FindProduct(int productID);
	}
}
