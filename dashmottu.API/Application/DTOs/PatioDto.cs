namespace dashmottu.API.Domain.DTOs
{
    public class PatioCreateDto
    {
        public int Id { get; set; }
        public string UrlImgPlanta { get; set; }
        public EnderecoDto Endereco { get; set; }
        public LoginDto Login { get; set; }
    }
}
