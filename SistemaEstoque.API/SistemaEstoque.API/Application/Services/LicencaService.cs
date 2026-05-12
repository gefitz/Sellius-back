using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Domain.Enums;
using Sellius.API.Enums;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class LicencaService
    {
        private readonly IDbMethods<License> _repository;

        public LicencaService(IDbMethods<License> repository)
        {
            _repository = repository;
        }

        public async Task<int> GerarLicenca(TypeLicense typeLicense)
        {
            License licenca = new License();
            licenca.dthInicioLincenca = DateTime.UtcNow;
            licenca.TipoLincenca = typeLicense;
            #region Define a config da Licenca
            switch (typeLicense)
            {
                case TypeLicense.Start:
                    licenca.ValorMensal = 49.90m;
                    licenca.dthVencimento = DateTime.UtcNow.AddMonths(1);
                    licenca.UsuairosIncluirFree = 2;
                    licenca.ValorPorUsuario = 10m;
                    break;
                case TypeLicense.Pro:
                    licenca.ValorMensal = 99.90m;
                    licenca.dthVencimento = DateTime.UtcNow.AddMonths(1);
                    licenca.UsuairosIncluirFree = 5;
                    licenca.ValorPorUsuario = 8m;
                    break;
                case TypeLicense.Master:
                    licenca.ValorMensal = 199.90m;
                    licenca.dthVencimento = DateTime.UtcNow.AddMonths(1);
                    licenca.UsuairosIncluirFree = 15;
                    licenca.ValorPorUsuario = 6m;
                    break;
                default:
                    licenca = null;
                    break;
            }
            #endregion

            if(!await _repository.Create(licenca))
                return 0;
            return licenca.id;
        }
    }
}
