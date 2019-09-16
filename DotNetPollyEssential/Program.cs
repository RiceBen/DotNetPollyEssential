using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using UnStableService;

namespace DotNetPollyEssential
{
    class Program
    {
        static void Main(string[] args)
        {
            var unstableService = new MrUnstable();

            ISyncPolicy redo1 = Policy.Handle<ApplicationException>()
                                      .WaitAndRetry(2, retryDurationProvider =>
                                      
                                          TimeSpan.FromSeconds(Math.Pow(2, retryDurationProvider))
                                      );
                                      
            ISyncPolicy redo2 = Policy.Handle<NullReferenceException>()
                                      .Retry(2,
                                      (exception, count) =>
                                      {
                                          Console.WriteLine($"NullReferenceException, exception:{exception.Message}, count:{count}");
                                      });

            ISyncPolicy redo3 = Policy.Handle<InvalidOperationException>()
                                      .Retry(2,
                                      (exception, count) =>
                                      {
                                          Console.WriteLine($"InvalidOperationException, exception:{exception.Message}, count:{count}");
                                      });

            ISyncPolicy myPolicyWrap =
            Policy.Wrap(redo1,
                        redo2,
                        redo3);
            try
            {
                myPolicyWrap.Execute(() => unstableService.RandomException());
            }
            catch (Exception)
            {
            }
            finally
            {
                Console.WriteLine("DotNetPollyEssential Finished!");
            }

            Console.Read();
        }
    }
}
