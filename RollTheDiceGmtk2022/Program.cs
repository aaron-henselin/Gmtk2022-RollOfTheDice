using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RollTheDiceGmtk2022.Game;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RollTheDiceGmtk2022
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddTransient<BlazorTimer>();

            builder.Services.AddScoped(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://gmtk2022-rollofthedice-functions.azurewebsites.net"),
                };
                client.DefaultRequestHeaders.Add("x-functions-key", "p9RXQa5NrV9VFGig-38NILM6Gt6sCGDwL5aHdKaMVqq4AzFu5NFtPQ==");
                return client;
            });



            await builder.Build().RunAsync();
        }
    }
}
