namespace API_REST_With_DOTNET7.Hypermedia.Constants
{
    public sealed class ResponseTypeFormat
    {
        public const string DefaultGet = "application/json";
        public const string DefaultPost = "application/json";
        public const string DefaultPut = "application/json";
        public const string DefaultPatch = "application/json";
        // Delete não precisa, pois o nosso método delete é void
        // public const string DefaultDelete = "application/json";
    }
}
