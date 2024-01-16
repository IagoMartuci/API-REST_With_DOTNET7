using Microsoft.AspNetCore.Mvc.Filters;

namespace API_REST_With_DOTNET7.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
