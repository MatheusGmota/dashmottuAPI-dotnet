namespace dashmottu.API.Application.DTOs
{
    public record LoginDto(string Usuario, string Senha);
    public record LoginRequestDto(string Usuario, string Senha, string Role);
    public record LoginResponseDto(LoginDto? Login, bool IsValid, int? IdPatio, string? Token);   
}
