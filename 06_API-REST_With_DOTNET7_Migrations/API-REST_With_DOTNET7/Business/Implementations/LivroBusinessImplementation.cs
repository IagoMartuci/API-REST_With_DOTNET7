using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Repository;
using log4net;

namespace API_REST_With_DOTNET7.Business.Implementations
{
    public class LivroBusinessImplementation : ILivroBusiness
    {
        private ILog _log = LogManager.GetLogger(typeof(LivroBusinessImplementation));
        private readonly ILivroRepository _livroRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public LivroBusinessImplementation(ILivroRepository livroRepository, IPessoaRepository pessoaRepository)
        {
            _livroRepository = livroRepository;
            _pessoaRepository = pessoaRepository;
        }

        public List<Livro> FindAllBusiness()
        {
            try
            {
                return _livroRepository.FindAllRepository();
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Livro FindByIdBusiness(int id)
        {
            try
            {
                return _livroRepository.FindByIdRepository(id);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Livro CreateBusiness(Livro livro)
        {
            try
            {
                if (!ValidarUsuario(livro.IdUsuario))
                    throw new Exception("Erro: favor informar o id do usuário responsável pelo cadastro!");

                var usuario = _pessoaRepository.FindByIdRepository(livro.IdUsuario);
                livro.NomeUsuario = string.Format("{0} {1}", usuario.Nome, usuario.Sobrenome);
                livro.DataLancamento = DateTime.Now.ToString();

                if (!ValidarPreco(livro.Preco))
                    throw new Exception("Erro: Favor informar o preço do livro!");
                else
                    return _livroRepository.CreateRepository(livro);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Livro UpdateBusiness(Livro livro)
        {
            try
            {
                // Verificando se o livro existe diretamente pelo Exists
                //var livroExists = _repository.Exists(livro.Id);

                //if (!livroExists)
                //    throw new Exception("Erro: Id não encontrado! Exists");

                // Se optar por fazer a validação se o livro existe no BD, aproveitando o Exists do método FindById, a programação começa aqui
                if (!ValidarDataLancamento(livro) && !ValidarPreco(livro.Preco))
                {
                    throw new Exception("Erro 1: Não é permitido alterar a data de lançamento do livro! \n" +
                                        "Erro 2: Favor informar o preço do livro!");
                }
                else
                {
                    if (!ValidarDataLancamento(livro))
                    {
                        throw new Exception("Erro: Não é permitido alterar a data de lançamento do livro!");
                    }
                    else
                    {
                        if ((!ValidarPreco(livro.Preco)))
                        {
                            throw new Exception("Erro: Favor informar o preço do livro!");
                        }
                        else
                        {
                            return _livroRepository.UpdateRepository(livro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public void DeleteBusiness(int id)
        {
            try
            {
                _livroRepository.DeleteRepository(id);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        private bool ValidarUsuario(int? idUsuario)
        {
            if (idUsuario == 0)
                idUsuario = null;

            if (idUsuario != null)
                return true;
            else
                return false;
        }

        private bool ValidarPreco(decimal? preco)
        {
            if (preco != null)
                return true;
            else
                return false;
        }

        //private bool ValidarDataLancamento(Livro livro)
        //{
        // Aproveitando a validação do Exists presente no método FindById
        //    var result = _repository.FindByIdRepository(livro.Id);

        //    if (result != null)
        //    {
        //        if (livro.DataLancamento.Equals(result.DataLancamento))
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        return false;            
        //    }
        //}

        private bool ValidarDataLancamento(Livro livro)
        {
            var result = _livroRepository.FindByIdRepository(livro.Id);

            if (livro.DataLancamento.Equals(result.DataLancamento))
                return true;
            else
                return false;
        }

        // Obs.: Para validar se a data de lançamento não foi alterada e o preço não fosse null
        // eu tive que declarar eles como possíveis null no BD e na classe da API, e para não
        // permitir null fiz o tratamento nos métodos de validação, assim como não permitir
        // a alteração na data de lançamento.
    }
}
