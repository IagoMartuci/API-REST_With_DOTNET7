using API_REST_With_DOTNET7.Model;
using Microsoft.AspNetCore.Identity;

namespace API_REST_With_DOTNET7.Services.Implementations
{
    public class PessoaServiceImplementation : IPessoaService
    {
        // Variável para mockar um id incrementado
        private volatile int count;

        public Pessoa Create(Pessoa pessoa)
        {
            return pessoa;
        }

        public void Delete(int id)
        {
            
        }

        public List<Pessoa> FindAll()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            for(int i = 1; i < 8; i ++)
            {
                Pessoa pessoa = MockPerson(i);
                pessoas.Add(pessoa);
            }
            return pessoas;
        }

        public Pessoa FindByID(int id)
        {
            return new Pessoa { Id = IncrementAndGet(), Nome = "Iago", Sobrenome = "Martuci", 
                Endereco = "São Paulo - SP / Brasil", Sexo = "Masculino" };
        }

        public Pessoa Update(Pessoa pessoa)
        {
            return pessoa;
        }

        private Pessoa MockPerson(int i)
        {
            return new Pessoa { Id = IncrementAndGet(), Nome = "Nome" + i, 
                Sobrenome = "Sobrenome" + i, Endereco = "Endereco" + i,
                Sexo = DefinirSexo(i) };
        }

        private string DefinirSexo(int i)
        {
            if (i % 2 == 0) // Par
                return "Masculino";
            else // Impar
                return "Feminino";
        }

        private int IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
