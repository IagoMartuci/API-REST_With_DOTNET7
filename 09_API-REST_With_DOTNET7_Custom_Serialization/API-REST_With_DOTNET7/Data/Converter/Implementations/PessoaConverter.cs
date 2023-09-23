using API_REST_With_DOTNET7.Data.Converter.Contract;
using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Data.Converter.Implementations
{
    // <Recebe PessoaVO e retorna Pessoa>, <Recebe Pessoa e retorna PessoaVO>
    public class PessoaConverter : IParser<PessoaVO, Pessoa>, IParser<Pessoa, PessoaVO>
    {
        public Pessoa Parse(PessoaVO origem)
        {
            if (origem == null)
                return null;
            else
                return new Pessoa
                {
                    Id = origem.Id,
                    Nome = origem.Nome,
                    Sobrenome = origem.Sobrenome,
                    Endereco = origem.Endereco,
                    Sexo = origem.Sexo,
                    Idade = origem.Idade
                };
        }

        public List<Pessoa> Parse(List<PessoaVO> origem)
        {
            if (origem == null)
                return null;
            else
                return origem.Select(item => Parse(item)).ToList(); // Faz tipo um foreach retornando uma lista do objeto
        }

        public PessoaVO Parse(Pessoa origem)
        {
            if (origem == null)
                return null;
            else
                return new PessoaVO
                {
                    Id = origem.Id,
                    NomeCompleto = origem.Nome + ' ' + origem.Sobrenome,
                    Endereco = origem.Endereco,
                    Sexo = origem.Sexo,
                    Idade = origem.Idade
                };
        }

        public List<PessoaVO> Parse(List<Pessoa> origem)
        {
            if (origem == null)
                return null;
            else
                return origem.Select(item => Parse(item)).ToList(); // Faz tipo um foreach retornando uma lista do objeto
        }
    }
}
