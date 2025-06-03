using ProductAPI.Common.Dto;
using ProductAPI.Common;
using ProductAPI.Services.Products.Dto;

namespace ProductAPI.Services.Products
{
    public interface IProductAppService : IAsyncCrudAppServiceBase<ProductDto, int, GetAllInputDto, ProductDto, ProductDto>
    {
    }
}
