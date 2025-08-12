using Sellius.API.Enums;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class LicencaService
    {
        private readonly IDbMethods<LicencaModel> _repository;

        public LicencaService(IDbMethods<LicencaModel> repository)
        {
            _repository = repository;
        }

        public async Task<int> GerarLicenca(TipoLicenca tipoLicenca)
        {
            LicencaModel licenca = new LicencaModel();
            licenca.dthInicioLincenca = DateTime.Now;
            licenca.TipoLincenca = tipoLicenca;
            #region Define a config da Licenca
            switch (tipoLicenca)
            {
                case TipoLicenca.Start:
                    licenca.ValorMensal = 49.90m;
                    licenca.dthVencimento = DateTime.Now.AddMonths(1);
                    licenca.UsuairosIncluirFree = 2;
                    licenca.ValorPorUsuario = 10m;
                    break;
                case TipoLicenca.Pro:
                    licenca.ValorMensal = 99.90m;
                    licenca.dthVencimento = DateTime.Now.AddMonths(1);
                    licenca.UsuairosIncluirFree = 5;
                    licenca.ValorPorUsuario = 8m;
                    break;
                case TipoLicenca.Master:
                    licenca.ValorMensal = 199.90m;
                    licenca.dthVencimento = DateTime.Now.AddMonths(1);
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
