using Carterinha.Aplication.Dto;
using Carterinha.Aplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carterinha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarterinhaController : ControllerBase
    {
        private readonly CarterinhaService _service;

        public CarterinhaController(CarterinhaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CadastraCarteira([FromBody] CreateCarterinhaDTO dto)
        {
            var result = await _service.CadastraCarteia(dto);
            if (result.Sucesso)
            {
                return Ok(result);
            }
            return BadRequest(result.Erros);
        }
    }
}
