
using BlazorExpenseTracker.Data;
using BlazorExpenseTracker.Data.Repositories;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlazorExpenseTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            builder.Services.AddSwaggerGen();
           
            
            
            var sqlCon = new SqlConfiguration("sqlConnection");
            builder.Services.AddSingleton(sqlCon);
           
            var sqlConnectionConfiguration = builder.Configuration.GetConnectionString("sqlConnection");
            builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(sqlConnectionConfiguration));


            //inyectando Category Repository
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}