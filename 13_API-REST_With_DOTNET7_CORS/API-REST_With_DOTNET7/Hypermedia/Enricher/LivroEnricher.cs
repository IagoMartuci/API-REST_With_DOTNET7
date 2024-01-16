using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_REST_With_DOTNET7.Hypermedia.Enricher
{
    public class LivroEnricher : ContentResponseEnricher<LivroVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(LivroVO content, IUrlHelper urlHelper)
        {
            var path = "api/livros";
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
