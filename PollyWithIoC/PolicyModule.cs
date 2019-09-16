using Autofac;
using System.Reflection;

namespace PollyWithIoC
{
    public class PolicyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceAssembly = Assembly.Load("PollyWithIoC");

            builder.RegisterType(typeof(BenPolicy)).Named<IBenPolicy>("MyPolicyOne");

            base.Load(builder);
        }
    }
}
