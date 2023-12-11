using API_REST_With_DOTNET7.Hypermedia.Abstract;

namespace API_REST_With_DOTNET7.Hypermedia.Filters
{
    // Classe responsavel para interceptar a response e adiconar o links
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
