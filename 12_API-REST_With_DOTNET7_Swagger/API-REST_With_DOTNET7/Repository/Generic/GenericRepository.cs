using API_REST_With_DOTNET7.Model.Base;
using API_REST_With_DOTNET7.Model.Context;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace API_REST_With_DOTNET7.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private ILog _log = LogManager.GetLogger("Generic Repository");
        private MySQLContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            // "Seta" o DbSet na classe MySQLContext e já "pega" objeto, tudo de forma dinamica
            // passa dinamicamente o dataset para cada classe em tempo de execução.
            dataset = _context.Set<T>();
        }

        public List<T> FindAllRepository()
        {
            try
            {
                int count = dataset.ToArray().Count(); // Não é necessário, só usei para saber o tamanho da lista
                return dataset.ToList();
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public T FindByIdRepository(int? id)
        {
            try
            {
                var result = dataset.SingleOrDefault(x => x.Id == id);

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

        public T CreateRepository(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                throw;
            }
        }

        public T UpdateRepository(T item)
        {

            if (!Exists(item.Id))
                throw new Exception("Erro: Id não encontrado! Update");

            var result = dataset.SingleOrDefault(x => x.Id == item.Id);

            if (result != null)
            {
                try
                {
                    dataset.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    _log.Info(ex);
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public void DeleteRepository(int id)
        {
            if (!Exists(id))
                throw new Exception("Erro: Id não encontrado! Delete");

            var result = dataset.SingleOrDefault(x => x.Id == id);

            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
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
            return dataset.Any(x => x.Id == id);
        }
    }
}
