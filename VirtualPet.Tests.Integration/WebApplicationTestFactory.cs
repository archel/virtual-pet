using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using VirtualPet.Domain.Pet;
using VirtualPet.Tests.Integration.Mocks;

namespace VirtualPet.Tests.Integration;

public class WebApplicationTestFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            builder.UseEnvironment("Testing");
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<FakeGuidGenerator>();
            services.AddTransient<IGuidGenerator>(sp => sp.GetRequiredService<FakeGuidGenerator>());
        });
    }
}
