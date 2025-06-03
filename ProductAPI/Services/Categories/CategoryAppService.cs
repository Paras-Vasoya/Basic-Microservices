using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Common;
using ProductAPI.Common.Dto;
using ProductAPI.Data;
using ProductAPI.Entities;
using ProductAPI.Services.Categories.Dto;

namespace ProductAPI.Services.Categories
{
    public class CategoryAppService: AsyncCrudAppServiceBase<Category, CategoryDto, int, GetAllInputDto, CategoryDto, CategoryDto>, ICategoryAppService
    {
        public CategoryAppService(ProductDbContext _dbContext, IMapper _mapper) : base(_dbContext, _mapper) { 
        }

        public override async Task<CategoryDto> CreateAsync(CategoryDto categoryDto)
        {
            if (categoryDto.Id > 0)
            {
                var existing = await _dbSet.FirstOrDefaultAsync(x => x.Id == categoryDto.Id);
                if (existing == null) throw new Exception("Category not found");

                _mapper.Map(categoryDto, existing); // map DTO into the tracked entity
                _dbContext.Update(existing);
            }
            else
            {
                var newEntity = _mapper.Map<Category>(categoryDto);
                await _dbSet.AddAsync(newEntity);
            }

            await _dbContext.SaveChangesAsync();

            // Always return updated entity from DB
            var saved = await _dbSet.FirstOrDefaultAsync(x => x.Id == categoryDto.Id);
            return _mapper.Map<CategoryDto>(saved);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var category = await _dbSet.FindAsync(id);
            if (category == null) return false;

            _dbSet.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
