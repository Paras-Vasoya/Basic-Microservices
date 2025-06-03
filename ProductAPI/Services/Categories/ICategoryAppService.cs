using ProductAPI.Common;
using ProductAPI.Common.Dto;
using ProductAPI.Services.Categories.Dto;

namespace ProductAPI.Services.Categories 
{
    public interface ICategoryAppService : IAsyncCrudAppServiceBase<CategoryDto, int, GetAllInputDto, CategoryDto, CategoryDto>
    {
    }
}
