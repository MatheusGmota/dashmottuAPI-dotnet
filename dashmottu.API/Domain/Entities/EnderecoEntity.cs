using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dashmottu.API.Domain.Entities
{
    [Table("ENDERECO")]
    public class EnderecoEntity
    {
        [Key]
        [Column("ID_ENDERECO")]
        public int Id { get; set; }

        [Required]
        [Column("CEP")]
        public string Cep { get; set; }

        [Column("LOGRADOURO")]
        public string Logradouro { get; set; }

        [Column("NUMERO")]
        public int Numero { get; set; }

        [Column("BAIRRO")]
        public string Bairro { get; set; }

        [Column("CIDADE")]
        public string Cidade { get; set; }

        [Column("ESTADO")]
        public string Estado { get; set; }

        [Column("ID_PATIO")]
        public int PatioId { get; set; }

        [JsonIgnore]
        public PatioEntity Patio { get; set; }
    }
}
