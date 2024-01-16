using API_REST_With_DOTNET7.Business;
using API_REST_With_DOTNET7.Business.Implementations;
using API_REST_With_DOTNET7.Hypermedia.Enricher;
using API_REST_With_DOTNET7.Hypermedia.Filters;
using API_REST_With_DOTNET7.Model.Context;
using API_REST_With_DOTNET7.Repository;
using API_REST_With_DOTNET7.Repository.Generic;
using API_REST_With_DOTNET7.Repository.Implementations;
using EvolveDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Serilog;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var appName = "API-REST_With_DOTNET7";
var appVersion = "v1";
var appDescription = $"API RESTful developed in course \"{appName}\"";

// Configurando para os endpoints no Swagger ficarem todos em minúsculo
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configurando o Swagger "manualmente" para efetuar personalizações
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(appVersion,
        new OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description = appDescription,
            Contact = new OpenApiContact
            {
                Name = "Iago Martuci",
                Url = new Uri("https://github.com/IagoMartuci")
            }
        });
});

// Adicionando HyperMedia (HATEOAS)
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PessoaEnricher());
filterOptions.ContentResponseEnricherList.Add(new LivroEnricher());
builder.Services.AddSingleton(filterOptions);
// Adicionando o versionamento da API: https://github.com/dotnet/aspnet-api-versioning
builder.Services.AddApiVersioning();
// Injeção de dependencia da interface
builder.Services.AddScoped<IPessoaBusiness, PessoaBusinessImplementation>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepositoryImplementation>();
builder.Services.AddScoped<ILivroBusiness, LivroBusinessImplementation>();
//builder.Services.AddScoped<ILivroRepository, LivroRepositoryImplementation>();
// Injeção de dependencia da interface do Repositorio Generico
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Configurando conexão ao MySQL DB (Utilizando o Pomelo)
// https://stackoverflow.com/questions/72670847/using-pomelo-entityframeworkcore-mysql-to-connect-to-mysql-with-net-6-on-mac
var connString = builder.Configuration["MySQLConn:MySQLConnString"];
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connString, serverVersion));

// Configurando para trafegar dados em XML também (Content Negociation)
/*
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true; // Para ele aceitar o formato de dado setado no header da requisição

    // options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
    // options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
    // OU
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");

}).AddXmlSerializerFormatters();
*/

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
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Configuração manual do Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{appName} - {appVersion}");
});

// Configuração manual do Swagger
var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

// Configuração para o HATEOAS
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app.Run();
