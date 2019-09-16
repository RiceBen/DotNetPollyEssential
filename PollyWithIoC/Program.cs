using System;
using Autofac;
using UnStableService;

namespace PollyWithIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PolicyModule>();
            var container = builder.Build();

            var unstableService = new MrUnstable();

            using (var lifetimeScope = container.BeginLifetimeScope())
            {
                var policy = lifetimeScope.ResolveNamed<IBenPolicy>("MyPolicyOne");

                try
                {
                    policy.GetPolicy().Execute(() => unstableService.RandomException());
                }
                catch (Exception)
                {
                }
                finally
                {
                    Console.WriteLine("PollyWithIoC Finished!");
                }
            }

            Console.Read();
        }
    }
}
