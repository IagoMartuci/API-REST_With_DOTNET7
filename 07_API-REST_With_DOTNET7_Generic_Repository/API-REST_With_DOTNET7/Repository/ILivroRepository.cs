using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Repository
{
    public interface ILivroRepository
    {
        List<Livro> FindAllRepository();
        Livro FindByIdRepository(int id);
        Livro CreateRepository(Livro livro);
        Livro UpdateRepository(Livro livro);
        void DeleteRepository(int id);
        bool Exists(int id);
    }
}
