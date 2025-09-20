using dashmottu.API.Domain.DTOs;
using dashmottu.API.Domain.Entities;

namespace dashmottu.API.Application.Mappers
{
    public static class LoginMapper
    {
        public static LoginEntity ToEntity(this LoginDto obj)
        {
            return new LoginEntity
            {
                Usuario = obj.Usuario,
                Senha = obj.Senha
            };
        }

        public static LoginDto ToDto(this LoginEntity entity)
        {
            return new LoginDto(
                entity.Usuario,
                entity.Senha
            );
        }
    }
}
