namespace dashmottu.API.Domain.DTOs
{
    public record LoginDto(string Usuario, string Senha);
    public record LoginResponseDto(bool IsValid, int? IdPatio, string? Token);
}
