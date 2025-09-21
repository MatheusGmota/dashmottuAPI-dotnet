using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
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

        [HttpPost("{idPatio}")]
        [SwaggerOperation(
            Summary = "Cadastro do login do pátio",
            Description = "Realiza o cadastro das credenciais de login de um patio."
        )]
        [SwaggerRequestExample(typeof(LoginDto), typeof(LoginDtoExample))]
        [SwaggerResponse(201, "Login cadastrado com sucesso", typeof(LoginResponseDto))]
        [SwaggerResponseExample(statusCode: 201, typeof(LoginResponseDtoExample))]
        public async Task<IActionResult> Criar(int idPatio, LoginDto login)
        {
            var result = await _applicationService.Adicionar(idPatio, login);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        // Validacao de login das crendencias(logindto)
        [HttpPost]
        public async Task<IActionResult> ValidarLogin(LoginDto login)
        {
            var obj = await _applicationService.ValidarLogin(login);
            
            if (!obj.IsSuccess) return StatusCode(obj.StatusCode, obj.Error);

            return StatusCode(obj.StatusCode, obj.Value);
        }

    }
}
