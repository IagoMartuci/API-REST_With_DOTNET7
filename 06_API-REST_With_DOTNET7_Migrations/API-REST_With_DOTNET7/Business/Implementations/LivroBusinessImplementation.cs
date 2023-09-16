﻿using API_REST_With_DOTNET7.Model;
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
                livro.DataLancamento = DateTime.Now.ToString();

                if (!ValidarPreco(livro.Preco))
                    throw new Exception("Erro: Favor informar o preço do livro!");
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
                // Verificando se o livro existe diretamente pelo Exists
                //var livroExists = _repository.Exists(livro.Id);

                //if (!livroExists)
                //    throw new Exception("Erro: Id não encontrado! Exists");

                // Se fizer a validação se o livro existe proveitando o Exists do método FindById, a programação começa aqui
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
                            return _repository.UpdateRepository(livro);
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

        private bool ValidarPreco(decimal? preco)
        {
            if (preco != null)
                return true;
            else
                return false;
        }

        //private bool ValidarDataLancamento(Livro livro)
        //{
              // Aproveitando a validação do exists do método FindById
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
            var result = _repository.FindByIdRepository(livro.Id);

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