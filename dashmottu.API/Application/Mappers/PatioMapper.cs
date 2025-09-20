using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Mappers
{
    public static class PatioMapper
    {
        public static PatioEntity ToEntity (this PatioResponse dto)
        {
            return new PatioEntity
            {
                Id = dto.Id,
                UrlImgPlanta = dto.UrlImgPlanta,
                Endereco = new EnderecoEntity
                {
                    Cep = dto.Endereco.Cep,
                    Cidade = dto.Endereco.Cidade,
                    Estado = dto.Endereco.Estado,
                    Logradouro = dto.Endereco.Logradouro,
                    Numero = dto.Endereco.Numero,
                    Bairro = dto.Endereco.Bairro
                },
            };
        }

        public static PatioResponse ToResponse(this PatioEntity entity)
        {
            return new PatioResponse(
                entity.Id,
                entity.UrlImgPlanta,
                new EnderecoDto(
                    entity.Endereco.Cep,
                    entity.Endereco.Logradouro,
                    entity.Endereco.Numero,
                    entity.Endereco.Bairro,
                    entity.Endereco.Cidade,
                    entity.Endereco.Estado
                )
            );
        }
    }
}
