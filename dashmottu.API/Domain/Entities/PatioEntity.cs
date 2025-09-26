using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dashmottu.API.Domain.Entities
{
    [Table("PATIO")]
    public class PatioEntity
    {
        [Key]
        [Column("ID_PATIO")]
        public int Id { get; set; }

        [Required]
        [Column("URL_IMAGEM")]
        public string UrlImgPlanta { get; set; } = string.Empty;
        
        public LoginEntity Login { get; set; }

        public EnderecoEntity Endereco { get; set; }

        public List<MotoEntity> Motos { get; set; }
    }
}
