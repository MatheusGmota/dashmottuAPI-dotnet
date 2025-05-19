namespace dashmottu.API.Domain.DTOs
{
    public class LoginDto
    {
        public string Usuario { get; set; }
        public string Senha { get; set; } // Será hasheada antes de salvar
    }
}
