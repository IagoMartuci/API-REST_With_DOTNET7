using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_With_DOTNET7.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; } // Id "generico", serve tanto para a classe Pessoa, quanto para a classe Livro
    }
}
