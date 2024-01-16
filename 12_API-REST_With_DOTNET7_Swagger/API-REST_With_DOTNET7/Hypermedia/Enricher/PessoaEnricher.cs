using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_REST_With_DOTNET7.Hypermedia.Enricher
{
    // Para todas classes retornadas e VO's temos que fazer essa configuração
    public class PessoaEnricher : ContentResponseEnricher<PessoaVO>
    {
        // Como será processado de forma paralela, temos que fazer o lock para garantir o funcionando correto
        private readonly object _lock = new object();
        protected override Task EnrichModel(PessoaVO content, IUrlHelper urlHelper)
        {
            var path = "api/pessoas";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });

            return Task.CompletedTask;
            // return null;
        }

        private string GetLink(int id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new
                {
                    controller = path,
                    id = id
                };

                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
