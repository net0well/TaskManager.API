using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Integração;
using SistemaDeTarefas.Integração.Interfaces;
using SistemaDeTarefas.Integração.Refit;
using SistemaDeTarefas.Repositorio;
using SistemaDeTarefas.Repositorio.Interfaces;

namespace SistemaDeTarefas;

public class Program
{
    
    
    public static void Main(string[] args)
    {
        string chaveScreta = "6b2d34a0-04ae-4858-9b8d-5b19664a9745";
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo{Title = "Sistem de Tarefas - API", Version = "v1"});

            var securitySchema = new OpenApiSecurityScheme()
            {
                Name = "JWT Authenticação",
                Description = "Entre com o JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { securitySchema, new string[] {} }
            });

        });

        builder.Services.AddEntityFrameworkSqlServer()
            .AddDbContext<SistemaDeTarefasDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );
        builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
        builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();

        builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://viacep.com.br");
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer  = "task",
                ValidAudience = "app",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveScreta))
            };

        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
       
        

        app.MapControllers();

        app.Run();
    }
}