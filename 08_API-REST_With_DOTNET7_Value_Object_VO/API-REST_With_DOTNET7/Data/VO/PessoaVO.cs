namespace API_REST_With_DOTNET7.Data.VO
{
    public class PessoaVO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string? NomeCompleto { get; set; }
        public string Endereco { get; set; }
        public string Sexo { get; set; }
        public string Idade { get; set; }
    }
}
