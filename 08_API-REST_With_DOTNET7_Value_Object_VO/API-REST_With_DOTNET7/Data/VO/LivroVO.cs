using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Data.VO
{
    public class LivroVO
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string? DataLancamento { get; set; }
        public decimal? Preco { get; set; }
        public string Titulo { get; set; }
        public Pessoa? Pessoa { get; set; } // Solução: deixar como possível null ou comentar
        public int? IdUsuario { get; set; }
        public string? NomeUsuario { get; set; }
        public string? IdentificacaoResponsavelCadastro { get; set; }
    }
}
