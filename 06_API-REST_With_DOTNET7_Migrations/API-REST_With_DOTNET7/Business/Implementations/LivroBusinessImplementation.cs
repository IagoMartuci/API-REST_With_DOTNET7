using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Repository;
using log4net;

namespace API_REST_With_DOTNET7.Business.Implementations
{
    public class LivroBusinessImplementation : ILivroBusiness
    {
        private ILog _log = LogManager.GetLogger(typeof(LivroBusinessImplementation));
        private readonly ILivroRepository _repository;

        public LivroBusinessImplementation(ILivroRepository repository)
        {
            _repository = repository;
        }

        public List<Livro> FindAllBusiness()
        {
            try
            {
                return _repository.FindAllRepository();
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
                return _repository.FindByIdRepository(id);
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
                livro.DataLancamento = DateTime.Now;

                if (!VerificarPreco(livro.Preco))
                    throw new Exception("Favor informar o preço do livro!");
                else
                    return _repository.CreateRepository(livro);
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
                if (!VerificarPreco(livro.Preco))
                    throw new Exception("Favor informar o preço do livro!");
                else
                    return _repository.UpdateRepository(livro);
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
                _repository.DeleteRepository(id);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        private bool VerificarPreco(decimal? preco)
        {
            if (preco == null)
                return false;
            else
                return true;
        }
    }
}
