using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_With_DOTNET7.Model
{
    [Table("livros")]
    public class Livro
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("autor")]
        public string Autor { get; set; }
        [Column("data_lancamento")]
        public string? DataLancamento { get; set; }
        //public DateTime DataLancamento { get; set; }
        [Column("preco")]
        public decimal? Preco { get; set; }
        [Column("titulo")]
        public string Titulo { get; set; }
    }
}