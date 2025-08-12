using Microsoft.AspNetCore.Mvc;
using Sellius.API.Services;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api")]
    public class CidadeEstadoController : Controller
    {
        private readonly CidadeEstadoService _service;

        public CidadeEstadoController(CidadeEstadoService service)
        {
            _service = service;
        }

        [HttpGet("recuperaTodosEstados")]
        public async Task<IActionResult> BuscarTodosEstados()
        {
            var response = await _service.buscarEstados();
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("recuperaCidades")]
        public async Task<IActionResult> BuscarCidade(int idEstado)
        {
            var response = await _service.BuscarCidade(idEstado);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
