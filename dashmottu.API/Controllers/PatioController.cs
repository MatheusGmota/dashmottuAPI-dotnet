using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter pátio por ID")]
        public IActionResult ObterPorId(int id)
        {
            return Ok();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criar novo pátio")]
        public IActionResult Criar([FromBody] PatioCreateDto novoPatio)
        {
            var criado = _applicationService.AdicionarPatio(novoPatio);
            return Ok(criado);
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
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir pátio por ID")]
        public IActionResult Excluir(int id)
        {
            return Ok();
        }
    }
}
