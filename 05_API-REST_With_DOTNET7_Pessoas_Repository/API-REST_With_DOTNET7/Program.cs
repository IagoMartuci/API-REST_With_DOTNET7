using API_REST_With_DOTNET7.Business;
using API_REST_With_DOTNET7.Business.Implementations;
using API_REST_With_DOTNET7.Model.Context;
using API_REST_With_DOTNET7.Repository;
using API_REST_With_DOTNET7.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicionando o versionamento da API: https://github.com/dotnet/aspnet-api-versioning
builder.Services.AddApiVersioning();
// Injeção de dependencia da interface
builder.Services.AddScoped<IPessoaBusiness, PessoaBusinessImplementation>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepositoryImplementation>();

// Configurando conexão ao MySQL DB (Utilizando o Pomelo)
// https://stackoverflow.com/questions/72670847/using-pomelo-entityframeworkcore-mysql-to-connect-to-mysql-with-net-6-on-mac
var connString = builder.Configuration["MySQLConn:MySQLConnString"];
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connString, serverVersion));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
