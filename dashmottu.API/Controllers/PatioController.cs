using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Interfaces;
using dashmottu.API.Doc.Samples;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioController : Controller
    {
        private readonly IPatioApplicationService _applicationService;

        public PatioController(IPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obter todos os pátios",
            Description = "Retorna uma lista com todos os registros de pátios cadastrados no sistema."
        )]
        [SwaggerResponse(200, "Lista de pátios retornada com sucesso.", typeof(IEnumerable<PatioResponse>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado.")]
        [SwaggerResponseExample(statusCode: 200, typeof(PatioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> ObterTodos(int Deslocamento = 0, int Limite = 3)
        {
            var resultado = await _applicationService.ObterTodosPatios(Deslocamento, Limite);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            var hateoas = resultado.Value.Data.Select(obj =>
            {
                GerarLinks(obj);
                return obj;
            });
            return StatusCode(resultado.StatusCode, hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter pátio por ID",
            Description = "Retorna os dados de um pátio específico, com base no ID fornecido."
        )]
        [SwaggerResponse(200, "Pátio retornado com sucesso", typeof(PatioResponse))]
        [SwaggerResponse(204, "Nenhum pátio encontrado.")]
        [SwaggerResponseExample(statusCode: 200, typeof(PatioResponseSample))]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _applicationService.ObterPatioPorId(id);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar um novo pátio",
            Description = "Cadastra um novo pátio com endereço, imagem da planta e informações de login."
        )]
        [SwaggerRequestExample(typeof(PatioRequest),typeof(PatioRequestSample))]
        [SwaggerResponse(201, "Pátio criado com sucesso.", typeof(PatioResponse))]
        [SwaggerResponseExample(statusCode: 201, typeof(PatioResponseSample))]
        public async Task<IActionResult> Criar([FromBody] PatioRequest novoPatio)
        {
            var resultado = await _applicationService.AdicionarPatio(novoPatio);
            
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            GerarLinks(resultado.Value);

            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar um pátio",
            Description = "Atualiza os dados de um pátio existente com base no ID fornecido."
        )]
        public async Task<IActionResult> Atualizar(int id, PatioRequest patioAtualizado)
        {
            var resultado = await _applicationService.EditarPatio(id, patioAtualizado);
            
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);
            
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover um pátio",
            Description = "Remove um pátio do sistema com base no ID fornecido."
        )]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _applicationService.DeletarPatio(id);

            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);

            return StatusCode(resultado.StatusCode, "Deletado com sucesso");
        }

        private void GerarLinks(PatioResponse obj)
        {
            obj.Links = new LinkDto(
                Url.Action(nameof(ObterPorId), "Patio", new { id = obj.Id }, Request.Scheme),
                Url.Action(nameof(Atualizar), "Patio", new { id = obj.Id }, Request.Scheme),
                Url.Action(nameof(Excluir), "Patio", new { id = obj.Id }, Request.Scheme));
        }

    }
}   
