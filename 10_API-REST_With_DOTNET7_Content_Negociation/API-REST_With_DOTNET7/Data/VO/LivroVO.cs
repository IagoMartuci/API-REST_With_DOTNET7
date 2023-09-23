using API_REST_With_DOTNET7.Model;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace API_REST_With_DOTNET7.Data.VO
{
    public class LivroVO
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string? DataLancamento { get; set; }
        public decimal? Preco { get; set; }
        public string Titulo { get; set; }
        [JsonIgnore]
        public Pessoa? Pessoa { get; set; } // Solução: deixar como possível null ou comentar
        //[JsonIgnore] - Se eu ocultar, não consigo trafegar este dado na requisição
        public int? IdUsuario { get; set; }
        [JsonIgnore]
        public string? NomeUsuario { get; set; }
        [JsonPropertyName("responsavelPeloCadastro")] // Alterando nome no JSON
        [XmlElement(ElementName = "ResponsavelPeloCadastro")] // Alterando nome no XML
        public string? IdentificacaoResponsavelCadastro { get; set; }
    }
}
