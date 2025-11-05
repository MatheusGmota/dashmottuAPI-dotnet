using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Interfaces;
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


        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _applicationService.ObterPorId(id);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] int Deslocamento = 0, [FromQuery] int Limite = 10)
        {
            var resultado = await _applicationService.ObterTodos(Deslocamento, Limite);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            var hateoas = resultado.Value.Data.Select(obj =>
            {
                GerarLinks(obj);
                return obj;
            });
            return StatusCode(resultado.StatusCode, hateoas);
        }

        [HttpPost("{idPatio}")]
        [SwaggerOperation(
            Summary = "Adicionar uma nova moto a um pátio",
            Description = "Adiciona uma nova moto a um pátio existente com base no ID do pátio fornecido."
        )]
        //[SwaggerRequestExample(typeof(MotoRequest), typeof(MotoRequestSample))]
        [SwaggerResponse(201, "Moto adicionada com sucesso.", typeof(PatioComMotosResponse))]
        //[SwaggerResponseExample(statusCode: 201, typeof(PatioComMotosResponseSample))]
        public async Task<IActionResult> AdicionarMoto(int idPatio, [FromBody] MotoRequest novaMoto)
        {
            var resultado = await _applicationService.AdicionarMotoNoPatio(idPatio, novaMoto);

            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }


        [HttpPost]
        [SwaggerOperation(Summary= "Adicionar uma nova moto", Description = "Adiciona uma nova moto ao sistema.")]
        public async Task<IActionResult> Adicionar([FromBody] MotoRequest moto)
        {
            var resultado = await _applicationService.Adicionar(moto);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] MotoRequest moto)
        {
            var resultado = await _applicationService.Atualizar(id, moto);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpDelete("{id}")]
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
