using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Interfaces;
using dashmottu.API.Doc.Samples;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
namespace dashmottu.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MotoController : Controller
    {
        private readonly IMotoApplicationService _applicationService;
        public MotoController(IMotoApplicationService service)
        {
            _applicationService = service;
        }


        [HttpPost]
        [SwaggerOperation(Summary= "Adicionar uma nova moto", Description = "Adiciona uma nova moto ao sistema.")]
        [SwaggerRequestExample(typeof(MotoRequest), typeof(MotoRequestSample))]
        [SwaggerResponse(201, "Pátio criado com sucesso.", typeof(MotoResponse))]
        [SwaggerResponseExample(statusCode: 201, typeof(MotoResponseSample))]
        public async Task<IActionResult> Adicionar([FromBody] MotoRequest moto)
        {
            var resultado = await _applicationService.ObterPorId(id);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar uma moto", Description = "Atualiza os detalhes de uma moto existente.")]
        [SwaggerResponse(200, "Moto atualizada com sucesso.", typeof(MotoResponse))]
        [SwaggerResponse(404, "Moto não encontrada.")]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseSample))]
        [SwaggerRequestExample(typeof(MotoRequest), typeof(MotoRequestSample))]
        public async Task<IActionResult> Atualizar(int id, [FromBody] MotoRequest moto)
        {
            var resultado = await _applicationService.AdicionarMotoNoPatio(idPatio, novaMoto);

            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter moto por ID", Description = "Obtém os detalhes de uma moto específica pelo seu ID.")]
        [SwaggerResponse(200, "Moto encontrada com sucesso.", typeof(MotoResponse))]
        [SwaggerResponse(404, "Moto não encontrada.")]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseSample))]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _applicationService.Adicionar(moto);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obter todas as motos", Description = "Obtém uma lista paginada de todas as motos.")]
        [SwaggerResponse(200, "Lista de motos obtida com sucesso.", typeof(IEnumerable<MotoResponse>))]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseListSample))]
        [SwaggerRequestExample(typeof(MotoRequest), typeof(MotoRequestSample))]
        public async Task<IActionResult> ObterTodos([FromQuery] int Deslocamento = 0, [FromQuery] int Limite = 10)
        {
            var resultado = await _applicationService.Atualizar(id, moto);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletar uma moto", Description = "Remove uma moto do sistema pelo seu ID.")]
        [SwaggerResponse(200, "Moto deletada com sucesso.", typeof(MotoResponse))]
        [SwaggerResponse(404, "Moto não encontrada.")]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseSample))]
        public async Task<IActionResult> Deletar(int id)
        {
            var resultado = await _applicationService.Deletar(id);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        private void GerarLinks(MotoResponse? obj)
        {
            obj.Links = new LinkDto(
                Url.Action(nameof(ObterPorId), "Moto", new { id = obj.Id }, Request.Scheme),
                Url.Action(nameof(Atualizar), "Moto", new { id = obj.Id }, Request.Scheme),
                Url.Action(nameof(Deletar), "Moto", new { id = obj.Id }, Request.Scheme));
        }
    }
}
