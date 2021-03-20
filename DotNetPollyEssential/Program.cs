using System;
using Polly;
using UnStableService;

namespace DotNetPollyEssential
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

            ISyncPolicy redo2 = Policy.Handle<NullReferenceException>()
                                      .Retry(1,
                                      (exception, count) =>
                                      {
                                          Console.WriteLine($"NullReferenceException, exception:{exception.Message}, count:{count}");
                                      });

            ISyncPolicy redo3 = Policy.Handle<InvalidOperationException>()
                                      .WaitAndRetry(2,
                                      (retryCounter, context) =>
                                      {
                                          Console.WriteLine($"InvalidOperationException, retryCounter:{retryCounter}, Context:{context}");
                                          return TimeSpan.FromSeconds(Math.Pow(2, retryCounter));
                                      });

            ISyncPolicy myPolicyWrap =
            Policy.Wrap(redo1,
                        redo2,
                        redo3);
            try
            {
                myPolicyWrap.Execute(() => unstableService.SpecificException(new InvalidOperationException()));
            }
            catch (Exception)
            {
                Console.WriteLine("DotNetPollyEssential outsight try catch!");
            }
            finally
            {
                Console.WriteLine("DotNetPollyEssential Finished!");
            }

            Console.Read();
        }
    }
}