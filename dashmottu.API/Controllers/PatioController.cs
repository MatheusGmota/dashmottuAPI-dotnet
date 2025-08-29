using dashmottu.API.Application.Interfaces;
using dashmottu.API.Doc.Samples;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

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
        [SwaggerResponse(200, "Lista de pátios retornada com sucesso.", typeof(IEnumerable<PatioCreateDto>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado.")]
        [SwaggerResponseExample(statusCode:200, typeof(PatioResponseListSample))]
        [EnableRateLimiting("rateLimitPolicy")]
        public async Task<IActionResult> ObterTodos()
        {
            var objModel = await _applicationService.ObterTodosPatios();

            if (objModel is not null)
                return Ok(objModel);

            return NoContent();

        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter pátio por ID",
            Description = "Retorna os dados de um pátio específico, com base no ID fornecido."
        )]
        [SwaggerResponse(200, "Pátio retornado com sucesso", typeof(PatioCreateDto))]
        [SwaggerResponse(204, "Nenhum pátio encontrado.")]
        [SwaggerResponseExample(statusCode:200, typeof(PatioResponseSample))]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var objModel = await _applicationService.ObterPatioPorId(id);

                if (objModel is not null)
                    return Ok(objModel);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = HttpStatusCode.BadRequest,
                });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar um novo pátio",
            Description = "Cadastra um novo pátio com endereço, imagem da planta e informações de login."
        )]
        public async Task<IActionResult > Criar([FromBody] PatioCreateDto novoPatio)
        {
            try
            {
                var objModel = await _applicationService.AdicionarPatio(novoPatio);
                if (objModel is not null)
                    return CreatedAtAction(
                        nameof(ObterPorId),
                        new { id = objModel.Id },
                        objModel
                    );

                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = "Não foi possivel salvar os dados"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = ex.Message
                });
            }
            
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Login do pátio",
            Description = "Realiza o login de um pátio com base nas credenciais fornecidas."
        )]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var objModel = await _applicationService.ValidarLogin(login);
            if (objModel is not null)
                return Ok(objModel);

            return BadRequest(new
            {
                status = HttpStatusCode.BadRequest,
                Error = "Login ou senha inválidos"
            });
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar um pátio",
            Description = "Atualiza os dados de um pátio existente com base no ID fornecido."
        )]
        public async Task<IActionResult> Atualizar(int id, [FromBody] PatioCreateDto patioAtualizado)
        {
            try
            {
                var objModel = await _applicationService.EditarPatio(id, patioAtualizado);

                if (objModel is not null)
                    return Ok(objModel);

                return BadRequest("Não foi possivel salvar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = HttpStatusCode.BadRequest,
                });
            }

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover um pátio",
            Description = "Remove um pátio do sistema com base no ID fornecido."
        )]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var objModel = await _applicationService.DeletarPatio(id);

                if (objModel is not null)
                    return Ok("Objeto deletado com sucesso!");

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = "Não foi possivel deletar o objeto"
                });
            }
        }
    }
}
