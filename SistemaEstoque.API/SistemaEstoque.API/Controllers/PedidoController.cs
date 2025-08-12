using Sellius.API.Models;
using Sellius.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Services;
using Sellius.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly PedidoServices _service;
        public PedidoController(PedidoServices service)
        {
            _service = service;

        }
        [HttpPost("obterTodosPedidos")]
        public async Task<IActionResult> obterTodosPedidos(PaginacaoTabelaResult<PedidoTabela, PedidoDTO> pedidoDTO)
        {
            var response = await _service.obterTodosPedidos(pedidoDTO);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("novoPedido")]
        [Authorize(Roles = "Funcionario,Admin,Gerente")]
        public async Task<IActionResult> Cadastrar(PedidoDTO pedido)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<PedidoDTO>.Failed(menssagemErro));
            }
            var response = await _service.CadastrarPedido(pedido);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);


        }
        [HttpPut]
        [Authorize(Roles = "Funcionario,Admin,Gerente")]
        public async Task<IActionResult> UpdatePedido(PedidoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<PedidoDTO>.Failed(menssagemErro));
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
