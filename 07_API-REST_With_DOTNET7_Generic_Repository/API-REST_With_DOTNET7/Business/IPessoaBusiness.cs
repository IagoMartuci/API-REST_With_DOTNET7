using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Business
{
    public interface IPessoaBusiness
    {
        List<Pessoa> FindAllBusiness(); // Get
        Pessoa FindByIdBusiness(int id); // Get
        Pessoa CreateBusiness(Pessoa pessoa); // Post
        Pessoa UpdateBusiness(Pessoa pessoa); // Put
        void DeleteBusiness(int id); // Delete

        // Teste
        List<Pessoa> FindByIdadeBusiness(int idade);
    }
}
