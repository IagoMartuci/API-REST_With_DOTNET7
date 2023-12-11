using API_REST_With_DOTNET7.Model.Base;

namespace API_REST_With_DOTNET7.Repository.Generic
{
    // T: Tipo genérico (que muda em tempo de execução), o objeto genérico recebe a herança da classe BaseEntity
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> FindAllRepository();
        // Possível null por conta dos dois métodos da LivroBusiness que pegam os atributo "int? IdUsuarioAlt" e int? IdUsuario
        // e passam para o médoto FindByIdRepository para resgatar quem é o usuário que está cadastrando o livro ou alterando.
        T FindByIdRepository(int? id);
        T CreateRepository(T item);
        T UpdateRepository(T item);
        void DeleteRepository(int id);
        bool Exists(int id);
    }
}
