namespace dashmottu.API.Domain.DTOs
{
    public record LoginDto(string Usuario, string Senha);
    public record LoginResponseDto(LoginDto? login, bool IsValid, int? IdPatio, string? Token);   
}
