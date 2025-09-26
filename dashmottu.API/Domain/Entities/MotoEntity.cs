using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dashmottu.API.Domain.Entities
{
    [Table("MOTOS")]
    public class MotoEntity
    {
        [Key]
        [Column("ID_MOTO")]
        public int Id { get; set; }

        [Required]
        [Column("COD_TAG")]
        public string CodTag { get; set; }

        [Required]
        [Column("MODELO")]
        public string Modelo { get; set; }

        [Required]
        [Column("PLACA")]
        public string Placa { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("POSICAO_X")]
        public double? PosicaoX { get; set; }

        [Column("POSICAO_Y")]
        public double? PosicaoY { get; set; }

        [Column("ID_PATIO")]
        public int? PatioId { get; set; }

        [JsonIgnore]
        public PatioEntity Patio { get; set; }
    }
}
