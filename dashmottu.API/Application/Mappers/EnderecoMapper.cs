using dashmottu.API.Application.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Mappers
{
    public static class EnderecoMapper
    {
        public static EnderecoEntity ToEntity(this EnderecoDto obj)
        {
            return new EnderecoEntity
            {
                Cep = obj.Cep,
                Logradouro = obj.Logradouro,
                Numero = obj.Numero,
                Bairro = obj.Bairro,
                Cidade = obj.Cidade,
                Estado = obj.Estado
            };
        }
        public static EnderecoDto ToDto(this EnderecoEntity entity)
        {
                return new EnderecoDto(
                entity.Cep,
                entity.Logradouro,
                entity.Numero,
                entity.Bairro,
                entity.Cidade,
                entity.Estado
            );
        }
    }
}
