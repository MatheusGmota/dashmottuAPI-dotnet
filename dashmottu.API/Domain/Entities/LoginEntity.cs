using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dashmottu.API.Domain.Entities
{
    [Table("LOGIN")]
    public class LoginEntity
    {
        [Key]
        [Column("ID_LOGIN")]
        public int Id { get; set; }

        [Column("USUARIO")]
        public string Usuario { get; set; }

        [Column("SENHA")]
        public string Senha { get; set; } // Deve ser armazenada como HASH (ex: BCrypt)
    }
}
