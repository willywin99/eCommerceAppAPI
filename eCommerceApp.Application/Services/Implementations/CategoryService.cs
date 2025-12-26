using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await categoryInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category added!") :
                new ServiceResponse(false, "Category failed to be added");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await categoryInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category deleted!") :
               new ServiceResponse(false, "Category failed to be deleted");
        }

        public Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetCategory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            throw new NotImplementedException();
        }
    }
}

public class ProductService(IGeneric<Product> productInterface, IMapper mapper) : IProductService
{
    public async Task<ServiceResponse> AddAsync(CreateProduct product)
    {
        
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await productInterface.DeleteAsync(id);
        return result > 0 ? new ServiceResponse(true, "Product deleted!") :
            new ServiceResponse(false, "Product failed to be deleted");
    }

    public async Task<IEnumerable<GetProduct>> GetAllAsync()
    {
        var rawData = await productInterface.GetAllAsync();
        if (!rawData.Any()) return [];

        return mapper.Map<IEnumerable<GetProduct>>(rawData);
    }

    public async Task<GetProduct> GetByIdAsync(Guid id)
    {
        var rawData = await productInterface.GetByIdAsync(id);
        if (rawData == null) return new GetProduct();

        return mapper.Map<GetProduct>(rawData);
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
    {
        var mappedData = mapper.Map<Product>(product);
        int result = await productInterface.UpdateAsync(mappedData);
        return result > 0 ? new ServiceResponse(true, "Product updated!") :
            new ServiceResponse(false, "Product failed to be updated");
    }
}
