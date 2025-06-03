using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Common.Dto;
using ProductAPI.Common;
using ProductAPI.Data;
using ProductAPI.Entities;
using ProductAPI.Services.Products.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Services.Products
{
    public class ProductAppService : AsyncCrudAppServiceBase<Product, ProductDto, int, GetAllInputDto, ProductDto, ProductDto>, IProductAppService
    {

        public ProductAppService(ProductDbContext _dbContext, IMapper _mapper) : base(_dbContext, _mapper)
        { }

        public override async Task<ProductDto> CreateAsync([FromBody]ProductDto productDto)
        {
            if (productDto.Id > 0)
            {
                var existing = await _dbSet.FirstOrDefaultAsync(x => x.Id == productDto.Id);
                if (existing == null) throw new Exception("Category not found");

                _mapper.Map(productDto, existing); // map DTO into the tracked entity
                _dbContext.Update(existing);
            }
            else
            {
                var newEntity = _mapper.Map<Product>(productDto);
                await _dbSet.AddAsync(newEntity);
            }

            await _dbContext.SaveChangesAsync();

            // Always return updated entity from DB
            var saved = await _dbSet.FirstOrDefaultAsync(x => x.Id == productDto.Id);
            return _mapper.Map<ProductDto>(saved);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var product = await _dbSet.FindAsync(id);
            if (product == null) return false;

            _dbSet.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
