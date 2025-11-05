using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.DTOs
{
    public record PatioRequest(string UrlImgPlanta, EnderecoDto Endereco);
    public record PatioResponse(int Id, string UrlImgPlanta, EnderecoDto Endereco) : Recurso;
    public record PatioComMotosResponse(int Id, IEnumerable<MotoWithXAndYResponse?> Motos) : Recurso;
}
