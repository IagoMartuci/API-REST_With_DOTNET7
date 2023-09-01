using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Services.Implementations
{
    public interface IPessoaService
    {
        Pessoa Create(Pessoa pessoa); // Post
        Pessoa FindByID(int id); // Get
        Pessoa Update(Pessoa pessoa); // Put
        void Delete(int id); // Delete
        List<Pessoa> FindAll(); // Get
    }
}
