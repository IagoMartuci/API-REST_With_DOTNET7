using log4net;
using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Model.Context;
using System.Text.RegularExpressions;

namespace API_REST_With_DOTNET7.Services.Implementations
{
    public class PessoaServiceImplementation : IPessoaService
    {
        private ILog _log = LogManager.GetLogger("Pessoa Service");
        private MySQLContext _context;
        public PessoaServiceImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Pessoa> FindAll()
        {
            try
            {
                return _context.Pessoas.ToList();
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Pessoa FindById(int id)
        {
            try
            {
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

        public Pessoa Create(Pessoa pessoa)
        {
            try
            {
                if (ValidarSexo(pessoa) && ValidarIdade(pessoa))
                {
                    _context.Add(pessoa);
                    _context.SaveChanges();
                    return pessoa;
                }
                else
                {
                    throw new Exception("Sexo ou idade inválidos!");
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public Pessoa Update(Pessoa pessoa)
        {
            if (!Exists(pessoa.Id))
                throw new Exception("Id não encontrado!");

            // Quando o id da pessoa recebido no parametro da requisição for igual ao id que tem no banco de dados, ele vai armazenar em result.
            var result = _context.Pessoas.SingleOrDefault(p => p.Id == pessoa.Id);

            if (result != null)
            {
                try
                {
                    if (ValidarSexo(pessoa) && ValidarIdade(pessoa))
                    {
                        _context.Entry(result).CurrentValues.SetValues(pessoa);
                        _context.SaveChanges();
                        return pessoa;
                    }
                    else
                    {
                        throw new Exception("Sexo ou idade inválidos!");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info(ex);
                    throw;
                }
            }
            return null;
        }

        public void Delete(int id)
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

        private bool Exists(int id)
        {
            return _context.Pessoas.Any(p => p.Id.Equals(id));
        }

        private bool ValidarSexo(Pessoa pessoa)
        {
            if (pessoa.Sexo.Equals("Feminino") || pessoa.Sexo.Equals("Masculino"))
                return true;
            else
                return false;
        }

        private bool ValidarIdade(Pessoa pessoa)
        {
            // https://www.techiedelight.com/pt/check-if-a-string-is-a-number-in-csharp/
            int num;
            bool isNum = int.TryParse(pessoa.Idade, out num);

            if (isNum)
                return true;
            else
                return false;
        }
    }
}
