using Polly;
using System;
using UnStableService;

namespace PollyFallback
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var unstableService = new MrUnstable();

            ISyncPolicy redo1 = Policy.Handle<ApplicationException>()
                                      .Retry(1,
                                      (exception, count) =>
                                      {
                                          Console.WriteLine($"ApplicationException, exception:{exception.Message}, count:{count}");
                                      });

            ISyncPolicy benFallback = Policy.Handle<ApplicationException>()
                                            .Fallback(() =>
                                            {
                                                Console.WriteLine("Execute Fallback action");
                                            }, ex =>
                                            {
                                                Console.WriteLine($"on fallback, exception message:{ex.Message.ToString()}");
                                            });
            benFallback.Execute(() =>
            {
                redo1.Execute(() =>
                {
                    unstableService.SpecificException(new ApplicationException());
                });
            });

            Console.Read();
        }
    }
}