using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Services;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class EmpresaController : Controller
    {
        private EmpresaService _service;
        public EmpresaController(EmpresaService service)
        {
            _service = service;
        }

        [HttpPost("cadastroNovaEmpresa")]
        public async Task<IActionResult> CadastrarEmpresaAsync(CadastroNovoEmpresaDTO nova)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<EnterpriseRegister>.Failed(menssagemErro));
            }
            var response = await _service.CadastrarNovaEmpresa(nova);

            if (!response.success)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpPost("obterTodasEmpresas")]
        [Authorize(Roles = "Interno")]
        public async Task<IActionResult> obterTodasEmpresas(PaginationTableResult<> paginacao)
        {
            var ret = await _service.obterTodasEmpresas(paginacao);
            if(!ret.success)
                return BadRequest(ret);
            return Ok(ret);
        }
        [HttpPut]
        [Authorize(Roles = "Interno")]
        public async Task<IActionResult> UpdateEmpresa(EnterpriseRegister dTO)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<EnterpriseRegister>.Failed(menssagemErro));
            }
            var response = await _service.UpdateEmpresa(dTO);
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Interno")]
        public async Task<IActionResult> InativarEmpresa(int id)
        {
            if(id <= 0)
            {
                return BadRequest(Response<EnterpriseRegister>.Failed("O id deve ser maior que zero"));
            }
            var ret = await _service.InativarEmpresa(id);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
    }
}
