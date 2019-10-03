using Polly;
using System;
using System.Threading;
using UnStableService;

namespace PollyCircuitBreaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var unstableService = new MrUnstable();

            ISyncPolicy redo1 = Policy.Handle<ApplicationException>()
                                      .CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(3),
                onBreak: (exception, timespan) =>
                {
                    Console.WriteLine("ApplicationException call onBreak Action delegate");
                },
                onReset: () =>
                {
                    Console.WriteLine("ApplicationException call onReset Action delegate");
                });

            ISyncPolicy redo2 = Policy.Handle<NullReferenceException>()
                                      .CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(3),
                onBreak: (exception, state, timespan, context) =>
                {
                    Console.WriteLine("NullReferenceException call onBreak Action delegate");
                },
                onReset: (ctx) =>
                {
                    Console.WriteLine("NullReferenceException call onReset Action delegate");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("NullReferenceException call onHalfOpen Action delegate");
                }
                );

            ISyncPolicy redo3 = Policy.Handle<InvalidOperationException>()
                                      .AdvancedCircuitBreaker(
                failureThreshold: 0.5,
                samplingDuration: TimeSpan.FromSeconds(10),
                minimumThroughput: 8,
                durationOfBreak: TimeSpan.FromSeconds(10),
                onBreak: (exception, timespan) =>
                {
                    Console.WriteLine("InvalidOperationException call onBreak Action delegate");
                },
                onReset: () =>
                {
                    Console.WriteLine("InvalidOperationException call onReset Action delegate");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("InvalidOperationException call onHalfOpen Action delegate");
                });

            ISyncPolicy myPolicyWrap =
            Policy.Wrap(redo1,
                        redo2,
                        redo3);

            for (var index = 0; index < 100; index++)
            {
                try
                {
                    SpinWait.SpinUntil(() => false, 1000);
                    myPolicyWrap.Execute(
                        () =>
                        unstableService.SpecificException(new NullReferenceException()));
                }
                catch (Exception)
                {
                    Console.WriteLine("Outside catch the exception!");
                }
                finally
                {
                    Console.WriteLine("DotNetPollyEssential Finished!");
                }
            }
            Console.Read();
        }
    }
}