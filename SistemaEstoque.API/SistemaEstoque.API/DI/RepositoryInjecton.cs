using Sellius.API.Infra.Repository.CidadeEstado;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;
using Sellius.API.Infra.Repository.Cliente;
using Sellius.API.Infra.Repository.Cliente.Interfaces;
using Sellius.API.Infra.Repository.Empresa;
using Sellius.API.Infra.Repository.Empresa.Interfaces;
using Sellius.API.Infra.Repository.Fornecedor;
using Sellius.API.Infra.Repository.Fornecedor.Interfaces;
using Sellius.API.Infra.Repository.Pedidos;
using Sellius.API.Infra.Repository.Pedidos.Interfaces;
using Sellius.API.Infra.Repository.Product;
using Sellius.API.Infra.Repository.Product.Interface;
using Sellius.API.Infra.Repository.Users;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.DI;

public static class RepositoryInjecton
{
    public static IServiceCollection AddRepository(this IServiceCollection service) =>
        service
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ILoginRepository, LoginRepository>()
            .AddScoped<ITpUserRepository, TpUserRepository>()
            .AddScoped<IMenuRepository, MenuRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IPriceTableRepository, PriceTableRepository>()
            .AddScoped<ITypeProductRepository, TypeProductRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IGroupCustomerRepository, GroupCustomerRepository>()
            .AddScoped<ISegmentationRepository, SegmentationRepository>()
            .AddScoped<ISupplierRepository, SupplierRepository>()
            .AddScoped<IPedidoRepository, PedidoRepository>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<IStateRepository, StateRespository>()
            .AddScoped<IEnterpriseRepository, EnterpriseRepository>();
}
