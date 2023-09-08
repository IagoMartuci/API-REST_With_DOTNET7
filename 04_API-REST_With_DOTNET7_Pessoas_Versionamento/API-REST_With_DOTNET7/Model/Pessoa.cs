using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_With_DOTNET7.Model
{
    [Table("pessoas")]
    public class Pessoa
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("sobrenome")]
        public string Sobrenome { get; set; }
        [Column("endereco")]
        public string Endereco { get; set; }
        [Column("sexo")]
        public string Sexo { get; set; }
        [Column("idade")]
        public string Idade { get; set; }
    }
}
