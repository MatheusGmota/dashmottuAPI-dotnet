using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Mappers
{
    public static class MotoMapper
    {
        public static MotoResponse ToDto(this MotoEntity entity)
        {
            return new MotoResponse(entity.Id, entity.CodTag, entity.Modelo, entity.Placa, entity.Status);
        }
        public static MotoEntity ToEntity(this MotoRequest dto)
        {
            return new MotoEntity
            {
                CodTag = dto.CodTag,
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                Status = dto.Status
            };
        }

        public static MotoWithXAndYResponse ToResponse(this MotoEntity entity)
        {
            return new MotoWithXAndYResponse(entity.Id, entity.CodTag, entity.Modelo, entity.Placa, entity.Status, entity.PosicaoX, entity.PosicaoY);
        }
    }
}
