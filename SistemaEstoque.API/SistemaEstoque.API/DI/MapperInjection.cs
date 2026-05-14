using Sellius.API.Application.Mappers;
using Sellius.API.Application.Mappers.Interfaces;

namespace Sellius.API.DI;

public static class MapperInjection
{
    public static IServiceCollection AddMapper(this IServiceCollection services) =>
        services
            .AddSingleton<IUserMapper, UserMapper>()
            .AddSingleton<IAuthenticationMapper, AuthenticationMapper>()
            .AddSingleton<IProductMapper, ProductMapper>()
            .AddSingleton<ICustomerMapper, CustomerMapper>()
            .AddSingleton<ISupplierMapper, SupplierMapper>()
            .AddSingleton<ISaleOrderMapper, SaleOrderMapper>()
            .AddSingleton<ITypeUserMapper, TypeUserMapper>()
            .AddSingleton<IMenuMapper, MenuMapper>()
            .AddSingleton<IEnterpriseMapper, EnterpriseMapper>()
            .AddSingleton<ICityMapper, CityMapper>()
            .AddSingleton<IStateMapper, StateMapper>()
            .AddSingleton<IGroupCustomerMapper, GroupCustomerMapper>()
            .AddSingleton<ISegmentationMapper, SegmentationMapper>()
            .AddSingleton<ITypeProductMapper, TypeProductMapper>()
            .AddSingleton<IPriceTableMapper, PriceTableMapper>();
}
