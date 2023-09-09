using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Repository
{
    public interface IPessoaRepository
    {
        List<Pessoa> FindAllRepository(); // Get
        Pessoa FindByIdRepository(int id); // Get
        Pessoa CreateRepository(Pessoa pessoa); // Post
        Pessoa UpdateRepository(Pessoa pessoa); // Put
        void DeleteRepository(int id); // Delete
        bool Exists(int id); // Validação no Banco de Dados
    }
}
