using System.Text;

namespace API_REST_With_DOTNET7.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }

        // Configuração no href para as barras do link serem / mesmo, e não %2f como o .NET tem por padrão.
        private string href;
        public string Href
        {
            get
            {
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set
            {
                href = value;
            }
        }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
