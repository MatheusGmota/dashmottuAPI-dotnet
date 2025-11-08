using dashmottu.API.Application.DTOs;
using dashmottu.API.Application.Interfaces;
using dashmottu.API.Doc.LoginSamples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace dashmottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthApplicationService _applicationService;

        public AuthController(IAuthApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("{idPatio}")]
        [SwaggerOperation(
            Summary = "Obter login do pátio por ID",
            Description = "Retorna os dados de um login específico, com base no ID do pátio fornecido."
        )]
        [SwaggerResponse(200, "Login retornado com sucesso", typeof(LoginResponseDto))]
        [SwaggerResponse(204, "Nenhum login encontrado.")]
        [SwaggerResponseExample(statusCode: 200, typeof(LoginResponseSample))]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(int idPatio)
        {
            var resultado = await _applicationService.ObterLoginPorId(idPatio);
            if (!resultado.IsSuccess) return StatusCode(resultado.StatusCode, resultado.Error);
            return StatusCode(resultado.StatusCode, resultado.Value);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastro do login do pátio",
            Description = "Realiza o cadastro das credenciais de login de um patio."
        )]
        [SwaggerRequestExample(typeof(LoginRequestDto), typeof(LoginRequestCreateSample))]
        [SwaggerResponse(201, "Login cadastrado com sucesso", typeof(LoginResponseDto))]
        [SwaggerResponseExample(statusCode: 201, typeof(LoginResponseSample))]
        public async Task<IActionResult> Criar([FromQuery] int? idPatio,[FromBody] LoginRequestDto login)
        {
            var result = await _applicationService.Adicionar(idPatio, login);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPost("/login")]
        [SwaggerOperation(
            Summary = "Validação de login",
            Description = "Valida as credenciais de login fornecidas e retorna um token se as credenciais forem válidas."
        )]
        [SwaggerRequestExample(typeof(LoginDto), typeof(LoginRequestSample))]
        [SwaggerResponse(200, "Login validado com sucesso", typeof(LoginResponseDto))]
        public async Task<IActionResult> ValidarLogin([FromBody] LoginDto login)
        {
            var obj = await _applicationService.ValidarLogin(login);

            if (!obj.IsSuccess) return StatusCode(obj.StatusCode, obj.Error);

            return StatusCode(obj.StatusCode, obj.Value);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Edição do login do pátio",
            Description = "Realiza a edição das credenciais de login de um patio."
        )]
        [SwaggerRequestExample(typeof(LoginDto), typeof(LoginRequestSample))]
        [SwaggerResponse(200, "Login editado com sucesso", typeof(LoginResponseDto))]
        public async Task<IActionResult> Editar([FromQuery]int idPatio, [FromBody] LoginDto login)
        {
            var result = await _applicationService.EditarLogin(idPatio, login);
            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Deletar o login do pátio",
            Description = "Realiza a exclusão das credenciais de login de um patio."
        )]
        public async Task<IActionResult> Deletar([FromQuery]int idPatio)
        {
            var result = await _applicationService.Deletar(idPatio);
            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);
            return StatusCode(result.StatusCode, "Deletado com sucesso");
        }
    }
}
