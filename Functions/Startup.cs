using Core.Common;
using Core.Ports;
using Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddCore();
            builder.Services.AddSingleton<ITodoRepository, InMemoryRepository>();
        }
    }
}
