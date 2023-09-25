using API_REST_With_DOTNET7.Data.Converter.Contract;
using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
                    Titulo = origem.Titulo,
                    Preco = origem.Preco,
                    IdUsuario = origem.IdUsuario,
                    NomeUsuario = origem.NomeUsuario,
                    DataCadastro = origem.DataCadastro,
                    IdUsuarioAlt = origem.IdUsuarioAlt,
                    NomeUsuarioAlt = origem.NomeUsuarioAlt,
                    DtAlteracao = origem.DtAlteracao
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
                    Titulo = origem.Titulo,
                    Preco = origem.Preco,
                    DataCadastro = origem.DataCadastro,
                    IdentificacaoResponsavelCadastro =
                        ($"{origem.IdUsuario} - {origem.NomeUsuario}").ToString(),
                    LogAlteracaoCadastral =
                        ($"Modificado em: {origem.DtAlteracao}, pelo usuario {origem.IdUsuarioAlt} - {origem.NomeUsuarioAlt}").ToString()
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
