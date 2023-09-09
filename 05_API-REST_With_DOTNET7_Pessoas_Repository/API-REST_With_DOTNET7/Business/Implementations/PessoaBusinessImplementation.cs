using log4net;
using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Repository;

namespace API_REST_With_DOTNET7.Business.Implementations
{
    public class PessoaBusinessImplementation : IPessoaBusiness
    {
        private ILog _log = LogManager.GetLogger("Pessoa Business");
        private readonly IPessoaRepository _repository;
        public PessoaBusinessImplementation(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public List<Pessoa> FindAll()
        {
            try
            {
                return _repository.FindAllRepo();
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
                return _repository.FindByIdRepo(id);
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
                if (!ValidarSexo(pessoa) && !ValidarIdade(pessoa))
                {
                    throw new Exception("Sexo e idade inválidos!");
                }
                else
                {
                    if (!ValidarSexo(pessoa))
                    {
                        throw new Exception("Sexo inválido!");
                    }
                    else
                    {
                        if (ValidarIdade(pessoa))
                        {
                            return _repository.CreateRepo(pessoa);
                        }
                        else
                        {
                            throw new Exception("Idade inválida!");
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

        public Pessoa Update(Pessoa pessoa)
        {
            try
            {
                if (!ValidarSexo(pessoa) && !ValidarIdade(pessoa))
                {
                    throw new Exception("Sexo e idade inválidos!");
                }
                else
                {

                    if (!ValidarSexo(pessoa))
                    {
                        throw new Exception("Sexo inválido!");
                    }
                    else
                    {
                        if (ValidarIdade(pessoa))
                        {
                            return _repository.UpdateRepo(pessoa);
                        }
                        else
                        {
                            throw new Exception("Idade inválida!");
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

        public void Delete(int id)
        {
            try
            {
                _repository.DeleteRepo(id);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
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
            uint num;
            bool isNum = uint.TryParse(pessoa.Idade, out num);

            if (isNum)
            {
                if (pessoa.Idade.Length <= 3 && num <= 100)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
