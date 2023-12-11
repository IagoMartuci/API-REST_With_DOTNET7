using API_REST_With_DOTNET7.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Concurrent;

namespace API_REST_With_DOTNET7.Hypermedia
{
    // O modificador abstract indica que o item que está sendo modificado tem uma implementação ausente ou incompleta.
    // O modificador abstrato pode ser usado com classes, métodos, propriedades, indexadores e eventos.
    // Use o modificador abstract em uma declaração de classe para indicar que uma classe se destina somente a ser uma classe base de outras classes, não instanciada por conta própria.
    // Membros marcados como abstratos precisam ser implementados por classes não abstratas que derivam da classe abstrata.
    // https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/abstract

    // Vai implementar a interface IResponseEnricher, desde que o T também implemente ISupportHyperMedia
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {
        public ContentResponseEnricher()
        {

        }

        // Type Representa as declarações de tipo: tipos de classe, tipos de interface, tipos de matriz, tipos de valor,
        // tipos de enumeração, parâmetros de tipo, definições de tipo genérico e tipos genéricos construídos abertos ou fechados.
        // https://learn.microsoft.com/pt-br/dotnet/api/system.type?view=net-8.0
        
        // Vai poder aplicar HATEOAS desde que seja um tipo T ou uma lista do tipo T, caso contrário retornará false
        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
                return CanEnrich(okObjectResult.Value.GetType());
            
            return false;
        }
        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }
            await Task.FromResult<object>(null);
        }
    }
}
