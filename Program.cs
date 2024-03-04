using ASPLAB_1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace ASPLAB_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var productList = new List<Product>();
            var app = builder.Build();

            app.Map("/postuser", appBuilder =>
            {
                appBuilder.Run(PostRequst);
            });

            app.MapWhen(context => context.Request.Path == "/aboutShop.html", appBuilder =>
            {
                appBuilder.Run(HomeRequst);
            });

            app.Run(HandleRequst);
            app.Run();
            
            async Task HandleRequst(HttpContext context)
            {

                var path = context.Request.Path;
                var fullPath = $"wwwroot/html/{path}";
                var response = context.Response;

                response.ContentType = "text/html; charset=utf-8";
                if (File.Exists(fullPath))
                {
                    await response.SendFileAsync(fullPath);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsync("<h2>Not Found</h2>");
                }
            }

            async Task HomeRequst(HttpContext context)
            {
                var path = context.Request.Path;
                var fullPath = $"wwwroot/html/{path}";
                var response = context.Response;

                response.ContentType = "text/html; charset=utf-8";
                await response.SendFileAsync(fullPath);
            }

            async Task PostRequst(HttpContext context)
            {
                var path = context.Request.Path;
                var fullPath = $"wwwroot/html/{path}";
                var response = context.Response;
                var form = context.Request.Form;
                productList.Add(new Product(form["name"], form["description"], form["category"], int.Parse(form["price"])));
                foreach (var product in productList)
                {
                    await context.Response.WriteAsync($"<div><p>ID: {product.getID()}</p><p>Name: {product.Name}</p><p>Description: {product.Description}</p><p>Type: {product.Type}</p><p>Prise: {product.Prise}</p></div><p>----------------------</p></div>");
                }

                await context.Response.WriteAsJsonAsync(productList);
            }
        }
    }
}