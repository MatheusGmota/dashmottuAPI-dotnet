using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
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
        [SwaggerOperation(Summary = "Obter todos os pátios")]
        public IActionResult ObterTodos()
        {
            var objModel = _applicationService.ObterTodosPatios();

            if (objModel is not null)
                return Ok(objModel);

            return BadRequest("Não foi possivel obter os dados");

        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter pátio por ID")]
        public IActionResult ObterPorId(int id)
        {
            var objModel = _applicationService.ObterPatioPorId(id);

            if (objModel is not null)
                return Ok(objModel);

            return BadRequest("Não foi possivel obter os dados");

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criar novo pátio", Description = "Endpoint que recebe dados para cadastrar novo patio")]
        public IActionResult Criar([FromBody] PatioCreateDto novoPatio)
        {
            try
            {
                var criado = _applicationService.AdicionarPatio(novoPatio);
                return Created();
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
        [SwaggerOperation(Summary = "Login de pátio")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar pátio existente")]
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
        [SwaggerOperation(Summary = "Excluir pátio por ID")]
        public IActionResult Excluir(int id)
        {
            return Ok();
        }
    }
}
