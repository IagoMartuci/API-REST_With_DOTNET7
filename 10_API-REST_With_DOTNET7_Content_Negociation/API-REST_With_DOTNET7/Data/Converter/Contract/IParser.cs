namespace API_REST_With_DOTNET7.Data.Converter.Contract
{
    // Interface recebe 2 objetos do tipo generico: Origem e Destino
    public interface IParser<O, D>
    {
        D Parse(O origem); // Recebe o objeto Origem, converte ele, e retorna o obejto Destino
        List<D> Parse(List<O> origem); // Recebe uma lista do objeto Origem, converte ela, e retorna uma lista do obejto Destino
    }
}
