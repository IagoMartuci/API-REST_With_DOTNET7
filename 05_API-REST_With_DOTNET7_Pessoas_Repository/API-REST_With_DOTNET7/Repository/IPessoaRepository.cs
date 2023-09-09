using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Repository
{
    public interface IPessoaRepository
    {
        List<Pessoa> FindAllRepo(); // Get
        Pessoa FindByIdRepo(int id); // Get
        Pessoa CreateRepo(Pessoa pessoa); // Post
        Pessoa UpdateRepo(Pessoa pessoa); // Put
        void DeleteRepo(int id); // Delete
        bool Exists(int id); // Validação no Banco de Dados
    }
}
