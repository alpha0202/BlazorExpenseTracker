using BlazorExpenseTracker.UI.Interfaces;
using BlazorExpenseTracker.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace BlazorExpenseTracker.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();       //ver potenciales errores
            builder.Services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; }) ;
            builder.Services.AddRadzenComponents();
            builder.Services.AddHttpClient<ICategoryService, CategoryService>(client => { client.BaseAddress = new Uri("https://localhost:7111"); });
            builder.Services.AddHttpClient<IExpenseService, ExpenseService>(client => { client.BaseAddress = new Uri("https://localhost:7111"); });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}