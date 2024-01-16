using API_REST_With_DOTNET7.Hypermedia;
using API_REST_With_DOTNET7.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace API_REST_With_DOTNET7.Data.VO
{
    public class PessoaVO : ISupportsHyperMedia
    {
        public int Id { get; set; }
        //[JsonIgnore] - Se eu ocultar, não consigo trafegar este dado na requisição
        public string Nome { get; set; }
        //[JsonIgnore] - Se eu ocultar, não consigo trafegar este dado na requisição
        public string Sobrenome { get; set; }
        public string? NomeCompleto { get; set; } // Exclusivo do VO
        public string Endereco { get; set; }
        public string Sexo { get; set; }
        public string Idade { get; set; }
        // Configurando HyperMedia no VO
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
