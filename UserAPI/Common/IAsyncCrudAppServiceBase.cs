namespace UserAPI.Common
{
    public interface IAsyncCrudAppServiceBase<TEntityDto, TKey, in TGetAllInput, in TCreateInput, in TUpdateInput>
    {
        Task<TEntityDto> GetByIdAsync(TKey id);
        Task<List<TEntityDto>> GetAllAsync();
        Task<TEntityDto> CreateAsync(TEntityDto input);
        Task<TEntityDto> UpdateAsync(TKey id, TEntityDto input);
        Task DeleteAsync(TKey id);
    }
}
