using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        public IActionResult ObterTodos()
        {
            var objModel = _applicationService.ObterTodosPatios();

            if (objModel is not null)
                return Ok(objModel);

            return NoContent();

        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter pátio por ID",
            Description = "Retorna os dados de um pátio específico, com base no ID fornecido."
        )]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var objModel = _applicationService.ObterPatioPorId(id);

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
        public IActionResult Criar([FromBody] PatioCreateDto novoPatio)
        {
            try
            {
                var objModel = _applicationService.AdicionarPatio(novoPatio);
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
        public IActionResult Login([FromBody] LoginDto login)
        {
            var objModel = _applicationService.ValidarLogin(login);
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
        public IActionResult Atualizar(int id, [FromBody] PatioCreateDto patioAtualizado)
        {
            try
            {
                var objModel = _applicationService.EditarPatio(id, patioAtualizado);

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
        public IActionResult Excluir(int id)
        {
            try
            {
                var objModel = _applicationService.DeletarPatio(id);

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
