using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CustomPolicyBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = StartUpService();

            var result = FetchDataFromOtherService(serviceProvider);


            Console.WriteLine(JsonSerializer.Serialize(result));
        }

        private static async Task<Classes> FetchDataFromOtherService(IServiceProvider serviceProvider)
        {
            var helper = serviceProvider.GetService<ThirdPartyHelper>();
            return await helper.FetchOpenCityData();
        }

        private static IServiceProvider StartUpService()
        {
            // basic service injection.
            var service = new ServiceCollection();

            service.AddSingleton<ThirdPartyHelper>();
            service.AddHttpClient("ThirdPartyHelper", client =>
             {
             }).AddPolicyHandler(GetRetryPolicy());

            var serviceProvider = service.BuildServiceProvider();

            return serviceProvider;
        }
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var customRetryPolicy =
            Policy.HandleResult<HttpResponseMessage>(result =>
            {
                // custom retry logic
                var rawData = result.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<Classes>(rawData);
                return string.IsNullOrEmpty(data.content[0].name) == false;
            }).WaitAndRetryAsync(
                2,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)),
                (result,timespan,context) => 
                {
                    Console.WriteLine("retry...");
                });

            return customRetryPolicy;
        }
    }
}

