﻿using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.PagingRequest;

namespace ERP_Service.Domain.Abstractions.Repository.Products;

public interface IProductRepository
{
	Task<int> Create(Product model);
	Task<bool> Delete(int id);
	Task<IEnumerable<Product>> GetAll(OptionFilterProduct option);
	Task<Product> GetById(int id);
	Task<bool> Update(Product model);
}
