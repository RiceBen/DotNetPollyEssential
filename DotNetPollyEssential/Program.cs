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
                                      .Retry(1,
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
