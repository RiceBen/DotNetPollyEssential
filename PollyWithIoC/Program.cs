using Autofac;
using System;
using System.Collections.Generic;
using UnStableService;

namespace PollyWithIoC
{
    internal class Program
    {
        private static void Main(string[] args)
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
                    var context = new Dictionary<string, object>()
                    {
                        { "Name", "benjamin fan" },
                        { "Age", 30 }
                    };

                    policy.ExecuteWithPolicy(() =>
                    {
                        unstableService.RandomException();
                    }, context);
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