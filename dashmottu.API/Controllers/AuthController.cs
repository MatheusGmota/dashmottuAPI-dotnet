using dashmottu.API.Application.Interfaces;
using dashmottu.API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace dashmottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthApplicationService _applicationService;
        private readonly IPatioApplicationService _patioService;

        public AuthController(IAuthApplicationService applicationService, IPatioApplicationService patioService)
        {
            _applicationService = applicationService;
            _patioService = patioService;
        }

        [HttpPost("{idPatio}")]
        [SwaggerOperation(
            Summary = "Login do pátio",
            Description = "Realiza o login de um pátio com base nas credenciais fornecidas."
        )]
        public async Task<IActionResult> FazerLogin(int idPatio, LoginDto login)
        {
            try
            {
                var patio = await _patioService.ObterPatioPorId(idPatio);
                if (patio == null)
                    throw new Exception("Pátio não encontrado");

                var obj = await _applicationService.Adicionar(idPatio, login);
                if (!obj.IsValid) 
                    return BadRequest(new
                    {
                        Data = obj,
                        Error = "Usuário já existe",
                        status = System.Net.HttpStatusCode.BadRequest,
                    });
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = System.Net.HttpStatusCode.BadRequest,
                });
            }
        }

        // Validacao de login das crendencias(logindto)
        [HttpPost]
        public async Task<IActionResult> ValidarLogin(LoginDto login)
        {
            try
            {
                var obj = await _applicationService.ValidarLogin(login);
                if (!obj.IsValid)
                    return BadRequest(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = System.Net.HttpStatusCode.BadRequest,
                });
            }
        }

    }
}
