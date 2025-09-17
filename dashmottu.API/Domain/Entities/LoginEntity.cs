using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace dashmottu.API.Domain.Entities
{
    [Table("LOGIN")]
    [Index(nameof(Usuario), IsUnique = true)]
    public class LoginEntity
    {
        [Key]
        [Column("ID_LOGIN")]
        public int Id { get; set; }

        [Required]
        [Column("USUARIO")]
        public string Usuario { get; set; }

        [Required]
        [Column("SENHA")]
        public string Senha { get; set; }

        [Column("ID_PATIO")]
        public int PatioId { get; set; }

        [JsonIgnore]
        public PatioEntity Patio { get; set; }
    }
}
