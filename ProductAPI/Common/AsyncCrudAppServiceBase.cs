using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Common
{
    public class AsyncCrudAppServiceBase<TEntity, TEntityDto, TKey, TGetAllInput, TCreateInput, TUpdateInput>
     where TEntity : class, new()
    {
        protected readonly DbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;

        public AsyncCrudAppServiceBase(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntityDto> GetByIdAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TEntityDto>(entity);
        }

        public virtual async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<List<TEntityDto>>(entities);
        }

        public virtual async Task<TEntityDto> CreateAsync(TCreateInput input)
        {
            var entity = _mapper.Map<TEntity>(input);
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TEntityDto>(entity);
        }

        public virtual async Task<TEntityDto> UpdateAsync(TKey id, TUpdateInput input)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            _mapper.Map(input, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TEntityDto>(entity);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
