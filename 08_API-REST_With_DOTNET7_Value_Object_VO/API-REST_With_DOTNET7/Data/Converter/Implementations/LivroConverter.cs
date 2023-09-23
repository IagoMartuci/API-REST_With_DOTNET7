using API_REST_With_DOTNET7.Data.Converter.Contract;
using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Data.Converter.Implementations
{
    public class LivroConverter : IParser<LivroVO, Livro>, IParser<Livro, LivroVO>
    {
        public Livro Parse(LivroVO origem)
        {
            if (origem == null)
                return null;
            else
                return new Livro
                {
                    Id = origem.Id,
                    Autor = origem.Autor,
                    DataLancamento = origem.DataLancamento,
                    Preco = origem.Preco,
                    Titulo = origem.Titulo,
                    IdUsuario = origem.IdUsuario,
                    NomeUsuario = origem.NomeUsuario
                };
        }

        public List<Livro> Parse(List<LivroVO> origem)
        {
            if (origem == null)
                return null;
            else
                return origem.Select(item => Parse(item)).ToList();
        }

        public LivroVO Parse(Livro origem)
        {
            if (origem == null)
                return null;
            else
                return new LivroVO
                {
                    Id = origem.Id,
                    Autor = origem.Autor,
                    DataLancamento = origem.DataLancamento,
                    Preco = origem.Preco,
                    Titulo = origem.Titulo,
                    IdentificacaoResponsavelCadastro =
                        ($"{origem.IdUsuario} - {origem.NomeUsuario}").ToString(),
                };
        }

        public List<LivroVO> Parse(List<Livro> origem)
        {
            if (origem == null)
                return null;
            else
                return origem.Select(item => Parse(item)).ToList();
        }
    }
}
