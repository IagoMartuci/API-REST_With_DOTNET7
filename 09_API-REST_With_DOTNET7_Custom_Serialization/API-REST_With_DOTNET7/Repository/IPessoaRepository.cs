using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Repository
{
    public interface IPessoaRepository
    {
        //List<Pessoa> FindAllRepository(); // Get
        //Pessoa FindByIdRepository(int id); // Get
        //Pessoa CreateRepository(Pessoa pessoa); // Post
        //Pessoa UpdateRepository(Pessoa pessoa); // Put
        //void DeleteRepository(int id); // Delete
        //bool Exists(int id); // Validação no Banco de Dados

        // Criado para testar quando precisamos fazer algo com um atributo exclusivo da classe pessoa
        // não faz sentido colocar no repositório genérico, já que o livro não tem o atributo idade
        List<Pessoa> FindByIdadeRepository(int idade); 
    }
}
