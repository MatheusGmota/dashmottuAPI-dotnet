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

        [ForeignKey("Login")]
        [Column("ID_LOGIN")]
        public int IdLogin { get; set; }

        [ForeignKey("Endereco")]
        [Column("ID_ENDERECO")]
        public int IdEndereco { get; set; }

        [Column("URL_IMG_PLANTA")]
        public string UrlImgPlanta { get; set; }
    }
}
