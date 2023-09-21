using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_With_DOTNET7.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
