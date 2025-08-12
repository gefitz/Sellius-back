using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.DTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Utils;
using Sellius.API.Services.Clientes;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;

namespace Sellius.API.Controllers.Clientes
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }
        [HttpPost("obterClientes")]
        [Authorize(Roles ="Funcionario,Adm,Gerente")]
        public async Task<IActionResult> ObterClientes([FromBody] PaginacaoTabelaResult<ClienteTabelaResult, FiltroCliente> clienteDTO)
        {
            clienteDTO.Filtro.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.BuscarClientes(clienteDTO);
            if (!ret.success)
            {
                return BadRequest(ret);
            }
            return Ok(ret);
        }
        [HttpPost]
        [Authorize(Roles ="Funcionario,Adm,Gerente")]
        public async Task<IActionResult> Cadastrar(ClienteDTO cliente)
        {
            cliente.EmpresaId = TokenService.RecuperaIdEmpresa(User);

            if (!ModelState.IsValid)
                {
                    var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(Response<ClienteDTO>.Failed(menssagemErro));
                }


            var response = await _service.CadastrarCliente(cliente);
            if (response.success)
                return Ok(response);

            return BadRequest(response);

        }
        [HttpDelete]
        [Authorize(Roles = "Funcionario,Adm,Gerente")]
        public async Task<IActionResult> InativarCliente(int id)
        {
            var ret = await _service.InativarCliente(id);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
        [HttpPut]
        [Authorize(Roles = "Funcionario,Adm,Gerente")]
        public async Task<IActionResult> UpdateCliente(ClienteDTO cliente)
        {
            cliente.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<ClienteDTO>.Failed(menssagemErro));
            }
            var ret = await _service.UpdateCliente(cliente);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }

    }
}
