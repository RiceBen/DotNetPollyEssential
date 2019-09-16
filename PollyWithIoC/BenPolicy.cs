using Polly;
using System;

namespace PollyWithIoC
{
    public class BenPolicy : IBenPolicy
    {
        public ISyncPolicy GetPolicy()
        {
            ISyncPolicy redo1 = Policy.Handle<ApplicationException>()
                                      .Retry(2, 
                                      (exception, count) =>
                                      {
                                          Console.WriteLine($"ApplicationException, exception:{exception.Message}, count:{count}");
                                      });

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

            return myPolicyWrap;
        }
    }
}
