using dashmottu.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace dashmottu.API.Infrastructure.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<PatioEntity> Patio {get; set;}
        public DbSet<EnderecoEntity> Endereco {get; set;}
        public DbSet<LoginEntity> Login {get; set;}
    }
}
