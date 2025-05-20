namespace dashmottu.API.Domain.DTOs
{
    public class LoginDto
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }

    public class LoginResponseDto
    {
        public bool IsValid { get; set; }
        public int? IdPatio { get; set; }
        public string? Token { get; set; }
    }
}
