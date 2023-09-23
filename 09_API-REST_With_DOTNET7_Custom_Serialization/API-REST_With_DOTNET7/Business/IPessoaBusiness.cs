using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Business
{
    public interface IPessoaBusiness
    {
        //List<Pessoa> FindAllBusiness(); // Get
        //Pessoa FindByIdBusiness(int id); // Get
        //Pessoa CreateBusiness(Pessoa pessoa); // Post
        //Pessoa UpdateBusiness(Pessoa pessoa); // Put
        //void DeleteBusiness(int id); // Delete

        //// Teste
        //List<Pessoa> FindByIdadeBusiness(int idade);

        // VO
        List<PessoaVO> FindAllBusiness(); // Get
        PessoaVO FindByIdBusiness(int id); // Get
        PessoaVO CreateBusiness(PessoaVO pessoa); // Post
        PessoaVO UpdateBusiness(PessoaVO pessoa); // Put
        void DeleteBusiness(int id); // Delete

        // Teste
        List<PessoaVO> FindByIdadeBusiness(int idade);
    }
}
