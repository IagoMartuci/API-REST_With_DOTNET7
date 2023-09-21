using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Model.Context;
using log4net;

namespace API_REST_With_DOTNET7.Repository.Implementations
{
    public class LivroRepositoryImplementation : ILivroRepository
    {
        private ILog _log = LogManager.GetLogger(typeof(LivroRepositoryImplementation));
        private MySQLContext _context;

        public LivroRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Livro> FindAllRepository()
        {
            try
            {
                int count = _context.Livros.ToList().Count(); // Não é necessário, só usei para saber o tamanho da lista
                return _context.Livros.ToList();
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Livro FindByIdRepository(int id)
        {
            try
            {
                var result = _context.Livros.SingleOrDefault(x => x.Id == id);

                if (result != null)
                    return result;
                else
                    throw new Exception("Erro: Id não encontrado! FindById");
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Livro CreateRepository(Livro livro)
        {
            try
            {
                _context.Add(livro);
                _context.SaveChanges();
                return livro;
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Livro UpdateRepository(Livro livro)
        {
             if (!Exists(livro.Id))
                throw new Exception("Erro: Id não encontrado! Update");

            var result = _context.Livros.SingleOrDefault(x => x.Id == livro.Id);

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(livro);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _log.Info(ex);
                    throw;
                }
            }
            return livro;
        }

        public void DeleteRepository(int id)
        {
            if (!Exists(id))
                throw new Exception("Erro: Id não encontrado! Delete");

            var result = _context.Livros.SingleOrDefault(x => x.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Livros.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _log.Info(ex);
                    throw;
                }
            }
        }

        public bool Exists(int id)
        {
            return _context.Livros.Any(x => x.Id == id);
        }
    }
}
