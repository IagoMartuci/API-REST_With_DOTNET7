using log4net;
using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Repository.Generic;
using API_REST_With_DOTNET7.Repository;
using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Data.Converter.Implementations;

namespace API_REST_With_DOTNET7.Business.Implementations
{
    public class PessoaBusinessImplementation : IPessoaBusiness
    {
        private ILog _log = LogManager.GetLogger("Pessoa Business");
        private readonly IRepository<Pessoa> _repository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly PessoaConverter _converter;
        public PessoaBusinessImplementation(IRepository<Pessoa> repository, IPessoaRepository pessoaRepository)
        {
            _repository = repository;
            _pessoaRepository = pessoaRepository;
            _converter = new PessoaConverter();
        }

        // Teste
        //public List<Pessoa> FindByIdadeBusiness(int idade)
        //{
        //    try
        //    {
        //        return _pessoaRepository.FindByIdadeRepository(idade);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Info(ex);
        //        throw;
        //    }
        //}

        public List<PessoaVO> FindByIdadeBusiness(int idade)
        {
            try
            {
                return _converter.Parse(_pessoaRepository.FindByIdadeRepository(idade));
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        //public List<Pessoa> FindAllBusiness()
        //{
        //    try
        //    {
        //        return _repository.FindAllRepository();
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Info(ex);
        //        throw;
        //    }
        //}

        public List<PessoaVO> FindAllBusiness()
        {
            try
            {
                return _converter.Parse(_repository.FindAllRepository());
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        //public Pessoa FindByIdBusiness(int id)
        //{
        //    try
        //    {
        //        return _repository.FindByIdRepository(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Info(ex);
        //        throw;
        //    }
        //}

        public PessoaVO FindByIdBusiness(int id)
        {
            try
            {
                return _converter.Parse(_repository.FindByIdRepository(id));
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        //public Pessoa CreateBusiness(Pessoa pessoa)
        //{
        //    try
        //    {
        //        if (!ValidarSexo(pessoa) && !ValidarIdade(pessoa))
        //        {
        //            throw new Exception("Erro: Sexo e idade inválidos!");
        //        }
        //        else
        //        {
        //            if (!ValidarSexo(pessoa))
        //            {
        //                throw new Exception("Erro: Sexo inválido!");
        //            }
        //            else
        //            {
        //                if (ValidarIdade(pessoa))
        //                {
        //                    return _repository.CreateRepository(pessoa);
        //                }
        //                else
        //                {
        //                    throw new Exception("Erro: Idade inválida!");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Info(ex);
        //        throw;
        //    }
        //}

        public PessoaVO CreateBusiness(PessoaVO pessoa)
        {
            try
            {
                if (!ValidarSexo(pessoa) && !ValidarIdade(pessoa))
                {
                    throw new Exception("Erro: Sexo e idade inválidos!");
                }
                else
                {
                    if (!ValidarSexo(pessoa))
                    {
                        throw new Exception("Erro: Sexo inválido!");
                    }
                    else
                    {
                        if (ValidarIdade(pessoa))
                        {
                            // Objeto chega como VO, então não podemos persistir ele no BD
                            // Por isso temos que "parsear" ele para entidade Pessoa
                            // E no retorno convertemos a entidade Pessoa para VO
                            var pessoaEntity = _converter.Parse(pessoa);
                            pessoaEntity = _repository.CreateRepository(pessoaEntity);
                            return _converter.Parse(pessoaEntity);
                        }
                        else
                        {
                            throw new Exception("Erro: Idade inválida!");
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

        //public Pessoa UpdateBusiness(Pessoa pessoa)
        //{
        //    try
        //    {
        //        if (!ValidarSexo(pessoa) && !ValidarIdade(pessoa))
        //        {
        //            throw new Exception("Erro: Sexo e idade inválidos!");
        //        }
        //        else
        //        {

        //            if (!ValidarSexo(pessoa))
        //            {
        //                throw new Exception("Erro: Sexo inválido!");
        //            }
        //            else
        //            {
        //                if (ValidarIdade(pessoa))
        //                {
        //                    return _repository.UpdateRepository(pessoa);
        //                }
        //                else
        //                {
        //                    throw new Exception("Erro: Idade inválida!");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Info(ex);
        //        throw;
        //    }
        //}

        public PessoaVO UpdateBusiness(PessoaVO pessoa)
        {
            try
            {
                if (!ValidarSexo(pessoa) && !ValidarIdade(pessoa))
                {
                    throw new Exception("Erro: Sexo e idade inválidos!");
                }
                else
                {

                    if (!ValidarSexo(pessoa))
                    {
                        throw new Exception("Erro: Sexo inválido!");
                    }
                    else
                    {
                        if (ValidarIdade(pessoa))
                        {
                            var pessoaEntity = _converter.Parse(pessoa);
                            pessoaEntity = _repository.UpdateRepository(pessoaEntity);
                            return _converter.Parse(pessoaEntity);
                        }
                        else
                        {
                            throw new Exception("Erro: Idade inválida!");
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
                _repository.DeleteRepository(id);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        //private bool ValidarSexo(Pessoa pessoa)
        //{
        //    if (pessoa.Sexo.Equals("Feminino") || pessoa.Sexo.Equals("Masculino"))
        //        return true;
        //    else
        //        return false;
        //}

        private bool ValidarSexo(PessoaVO pessoa)
        {
            if (pessoa.Sexo.Equals("Feminino") || pessoa.Sexo.Equals("Masculino"))
                return true;
            else
                return false;
        }

        //private bool ValidarIdade(Pessoa pessoa)
        //{
        //    // https://www.techiedelight.com/pt/check-if-a-string-is-a-number-in-csharp/
        //    uint num;
        //    bool isNum = uint.TryParse(pessoa.Idade, out num);

        //    if (isNum)
        //    {
        //        if (pessoa.Idade.Length <= 3 && num <= 100)
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private bool ValidarIdade(PessoaVO pessoa)
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
