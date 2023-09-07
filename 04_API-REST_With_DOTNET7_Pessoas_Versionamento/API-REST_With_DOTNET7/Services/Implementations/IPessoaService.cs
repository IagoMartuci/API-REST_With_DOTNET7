using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Services.Implementations
{
    public interface IPessoaService
    {
        List<Pessoa> FindAll(); // Get
        Pessoa FindById(int id); // Get
        Pessoa Create(Pessoa pessoa); // Post
        Pessoa Update(Pessoa pessoa); // Put
        void Delete(int id); // Delete
    }
}
