using API_REST_With_DOTNET7.Business;
using API_REST_With_DOTNET7.Business.Implementations;
using API_REST_With_DOTNET7.Model.Context;
using API_REST_With_DOTNET7.Repository;
using API_REST_With_DOTNET7.Repository.Generic;
using API_REST_With_DOTNET7.Repository.Implementations;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Serilog;

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
builder.Services.AddScoped<ILivroBusiness, LivroBusinessImplementation>();
builder.Services.AddScoped<ILivroRepository, LivroRepositoryImplementation>();
// Injeção de dependencia da interface do Repositorio Generico
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


// Configurando conexão ao MySQL DB (Utilizando o Pomelo)
// https://stackoverflow.com/questions/72670847/using-pomelo-entityframeworkcore-mysql-to-connect-to-mysql-with-net-6-on-mac
var connString = builder.Configuration["MySQLConn:MySQLConnString"];
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connString, serverVersion));

var app = builder.Build();

// Adaptando o IWebHostEnvironment na Program.cs (a partir do .NET6, a classe Startup.cs foi descontinuada)
// https://www.macoratti.net/21/09/aspn6_migra1.htm
// https://www.macoratti.net/21/09/aspn6_migra2.htm
IWebHostEnvironment env = app.Environment;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

if (env.IsDevelopment())
{
	try
	{
		if (connString != null)
			MigrarBaseDeDados(connString);
    }
	catch (Exception ex)
	{
		Log.Error("A string de conexão não pode ser nula: ", ex);
		throw;
	}
}

void MigrarBaseDeDados(string connString)
{
    try
    {
        var envConn = new MySqlConnection(connString);
        var evolve = new Evolve(envConn, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = false,
        };
        evolve.Migrate();
        // https://evolve-db.netlify.app/configuration/naming/
    }
    catch (Exception ex)
    {
        Log.Error("Erro na migração da base de dados: ", ex);
        throw;
    }
}

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