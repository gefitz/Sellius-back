using Sellius.API.Models;
using Sellius.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Services;
using Sellius.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Utils;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly PedidoService _service;
        public PedidoController(PedidoService service)
        {
            _service = service;

        }
        [HttpPost("obterTodosPedidos")]
        public async Task<IActionResult> obterTodosPedidos(PaginationTableResult<> pedidoDTO)
        {
            pedidoDTO.Filtro.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.obterTodosPedidos( pedidoDTO);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("novoPedido")]
        public async Task<IActionResult> Cadastrar(SaleOrderRegister pedido)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<SaleOrderRegister>.Failed(menssagemErro));
            }
            pedido.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            pedido.UsuarioId = TokenService.RecuperaIdUsuario(User);
            var response = await _service.CadastrarPedido(pedido);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);


        }
        [HttpPut]
        public async Task<IActionResult> UpdatePedido(SaleOrderRegister dto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<SaleOrderRegister>.Failed(menssagemErro));
            }
            var ret = await _service.UpdatePedido(dto);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }

    }
}
