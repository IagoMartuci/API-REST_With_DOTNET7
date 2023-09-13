using Microsoft.EntityFrameworkCore;

namespace API_REST_With_DOTNET7.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() 
        {

        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Livro> Livros { get; set; }
    }
}
