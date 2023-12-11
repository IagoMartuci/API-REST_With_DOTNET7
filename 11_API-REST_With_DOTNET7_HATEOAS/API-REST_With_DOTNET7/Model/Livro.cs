using API_REST_With_DOTNET7.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_With_DOTNET7.Model
{
    [Table("livros")]
    public class Livro : BaseEntity
    {
        [Column("autor")]
        public string Autor { get; set; }
        [Column("titulo")]
        public string Titulo { get; set; }
        [Column("preco")]
        public decimal? Preco { get; set; }

        // Relacionamento Pessoa x Livro:
        // A ideia era relacionar a pessoa como se fosse o usuario de um sistema, e que ao cadastrar um livro, ela deve informar o seu id de usuário
        // com esse id o sistema recupera o nome e sobrenome da pessoa e preenche automaticamente no nome de usuario.

        [NotMapped] // Mesmo colocando o NotMapped dava esse retorno: "The Pessoa field is required."
        public Pessoa? Pessoa { get; set; } // Solução: deixar como possível null ou comentar
        [Column("id_usuario")] // Id do responsavel pelo cadastro do livro
        public int? IdUsuario { get; set; }
        [Column("nome_usuario")] // Nome do responsavel pelo cadastro do livro
        public string? NomeUsuario { get; set; }
        [Column("data_cadastro")]
        public string? DataCadastro { get; set; }
        //public DateTime DataLancamento { get; set; }
        [Column("id_usuario_alteracao")]
        public int? IdUsuarioAlt { get; set; }
        [Column("nome_usuario_alteracao")]
        public string? NomeUsuarioAlt { get; set; }
        [Column("data_alteracao")]
        public string? DtAlteracao { get; set; }
    }
}