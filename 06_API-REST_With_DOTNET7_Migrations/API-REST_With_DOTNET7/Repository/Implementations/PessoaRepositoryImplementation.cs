using log4net;
using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Model.Context;

namespace API_REST_With_DOTNET7.Repository.Implementations
{
    public class PessoaRepositoryImplementation : IPessoaRepository
    {
        private ILog _log = LogManager.GetLogger("Pessoa Repository");
        private MySQLContext _context;
        public PessoaRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Pessoa> FindAllRepository()
        {
            try
            {
                int count = _context.Pessoas.ToList().Count(); // Não é necessário, só usei para saber o tamanho da lista
                return _context.Pessoas.ToList();
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Pessoa FindByIdRepository(int id)
        {
            try
            {
                // Quando o id da pessoa recebido no parametro da requisição for igual ao id que tem no banco de dados, ele vai armazenar em result.
                var result = _context.Pessoas.SingleOrDefault(p => p.Id == id);

                if (result != null)
                    return result;
                else
                    throw new Exception("Id não econtrado!");
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Pessoa CreateRepository(Pessoa pessoa)
        {
            try
            {
                _context.Add(pessoa);
                _context.SaveChanges();
                return pessoa;
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Pessoa UpdateRepository(Pessoa pessoa)
        {
            if (!Exists(pessoa.Id))
                throw new Exception("Id não encontrado!");

            var result = _context.Pessoas.SingleOrDefault(p => p.Id == pessoa.Id);

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(pessoa);
                    _context.SaveChanges();
                    //return pessoa;
                }
                catch (Exception ex)
                {
                    _log.Info(ex);
                    throw;
                }
            }
            return pessoa;
        }

        public void DeleteRepository(int id)
        {
            if (!Exists(id))
                throw new Exception("Id não encontrado!");

            var result = _context.Pessoas.SingleOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Pessoas.Remove(result);
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
            return _context.Pessoas.Any(p => p.Id.Equals(id));
        }
    }
}
