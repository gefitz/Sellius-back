using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Services;
using Sellius.API.Utils;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class MenuController : Controller
    {
        private MenuService _service;

        public MenuController(MenuService service)
        {
            _service = service;
        }

        [HttpGet("recuperaMenus")]
        public async Task<IActionResult> recuperaMenus()
        {
            int idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.recuperaMenus(idEmpresa);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPost("listarMenus")]
        public async Task<IActionResult> listarMenus(PaginacaoTabelaResult<MenuDTO,MenuFiltro> paginacaoTabela)
        {
            paginacaoTabela.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);

            var ret = await _service.ObterTodosMenus(paginacaoTabela);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPost("salvarMenu")]
        public async Task<IActionResult> salvarMenu(MenuDTO menu)
        {
        
            var ret = await _service.CriarMenu(menu);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }

        [HttpPut]
        public async Task<IActionResult> updateMenu(MenuDTO menu)
        {

            var ret = await _service.UpdateMenu(menu);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpGet("carregaCombo")]
        public async Task<IActionResult> carregaComboMenu()
        {
            int idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.carregaComboMenu(idEmpresa);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPost("obterTodosMenus")]
        public async Task<IActionResult> obterTodosMenus(MenuFiltro menu)
        {

            menu.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.obterTodosMenus(menu);

            if(ret.success) {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
    }
}
