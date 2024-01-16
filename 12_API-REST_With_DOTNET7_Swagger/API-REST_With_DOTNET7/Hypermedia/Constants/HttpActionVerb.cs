namespace API_REST_With_DOTNET7.Hypermedia.Constants
{
    // Quando aplicado a uma classe, o modificador sealed impede que outras classes herdem dela
    // https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/sealed
    public sealed class HttpActionVerb
    {
        public const string GET = "GET";
        public const string POST = "POST";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
        public const string PATCH = "PACTH";
    }
}
