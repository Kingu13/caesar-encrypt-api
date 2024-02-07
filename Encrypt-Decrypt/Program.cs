using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Caesar;

    public static class CaesarMachine
    {
        public static readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();

        public static string EncryptCaesar(string text, int shift)
        {
            string encryptedText = "";
            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    char baseChar = char.IsUpper(character) ? 'A' : 'a';
                    int index = Array.IndexOf(alphabet, char.ToUpper(character));
                    int shiftedIndex = (index + shift) % 29;
                    char shiftedChar = char.IsUpper(character) ? alphabet[shiftedIndex] : char.ToLower(alphabet[shiftedIndex]);
                    encryptedText += shiftedChar;
                }
                else
                {
                    encryptedText += character;
                }
            }
            return encryptedText;
        }

        public static string DecryptCaesar(string text, int shift)
        {
            return EncryptCaesar(text, 29 - shift);
        }
    }

    public class StartUpSite
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Caesar", async context =>
                {
                    await context.Response.WriteAsync("Welcome To The Caesar Cihper Encrypt And Decrypt!\n");
                    await context.Response.WriteAsync("Use /Encrypt?text=(What to encrypt)\n");
                    await context.Response.WriteAsync("Use /Decrypt?text=(What to decrypt)\n");
                });
                endpoints.MapGet("/Encrypt", async context =>
                {
                    string text = context.Request.Query["text"];

                    string encryptedText = CaesarMachine.EncryptCaesar(text, 3);

                    await context.Response.WriteAsync(encryptedText);
                });

                endpoints.MapGet("/Decrypt", async context =>
                {
                    string text = context.Request.Query["text"];

                    string decryptedText = CaesarMachine.DecryptCaesar(text, 3);

                    await context.Response.WriteAsync(decryptedText);
                });
            });
        }
    }

/* Om man vill använda sig av html koden:
 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.SendFileAsync("../wwwroot/Index.html");
                });
                endpoints.MapGet("/Caesar", async context =>
                {
                    await context.Response.SendFileAsync("../wwwroot/Caesar.html");
                });
                endpoints.MapPost("/Caesar/encrypt", async context =>
                {
                    var text = await context.Request.ReadFromJsonAsync<string>();
                    var encryptedText = CaesarMachine.EncryptCaesar(text, 3);
                    await context.Response.WriteAsync(encryptedText);
                });
                endpoints.MapPost("/Caesar/decrypt", async context =>
                {
                    var text = await context.Request.ReadFromJsonAsync<string>();
                    var decryptedText = CaesarMachine.DecryptCaesar(text, 3);
                    await context.Response.WriteAsync(decryptedText);
                });
            });
        }
    }
*/
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUpSite>();
                });
    }
