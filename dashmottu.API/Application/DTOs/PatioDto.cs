namespace dashmottu.API.Domain.DTOs
{
    public record PatioRequest(string UrlImgPlanta, EnderecoDto Endereco);
    public record PatioResponse(int Id,string UrlImgPlanta, EnderecoDto Endereco);
}
