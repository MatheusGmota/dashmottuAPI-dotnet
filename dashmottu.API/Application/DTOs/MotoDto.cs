using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.DTOs
{
    public record MotoRequest(string CodTag, string Modelo, string Placa, string Status);
    public record MotoResponse(int Id, string CodTag, string Modelo, string Placa, string Status) : Recurso;
    public record MotoWithXAndYResponse(int Id, string CodTag, string Modelo, string Placa, string Status, double? PosicaoX, double? PosicaoY) : Recurso;

}
