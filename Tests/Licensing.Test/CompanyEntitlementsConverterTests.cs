namespace Fossa.Licensing.Test;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using TIKSN.DependencyInjection;
using TIKSN.Licensing;
using Xunit;

public class CompanyEntitlementsConverterTests
{
    private readonly IServiceProvider serviceProvider;

    public CompanyEntitlementsConverterTests()
    {
        var services = new ServiceCollection();
        _ = services.AddLicense();
        _ = services.AddFrameworkCore();

        var fakeTimeProvider = new FakeTimeProvider(
            new DateTimeOffset(2022, 9, 24, 0, 0, 0, TimeSpan.Zero));
        _ = services.AddSingleton(fakeTimeProvider);

        ContainerBuilder containerBuilder = new();
        _ = containerBuilder.RegisterModule<CoreModule>();
        containerBuilder.Populate(services);

        this.serviceProvider = new AutofacServiceProvider(containerBuilder.Build());
    }

    [Fact]
    public void GivenRegisteredServices_WhenCompanyEntitlementsConverterRequested_ThenServiceShouldBeResolved()
    {
        // Arrange

        var companyEntitlementsConverter = this.serviceProvider
            .GetRequiredService<IEntitlementsConverter<CompanyEntitlements, CompanyLicenseEntitlements>>();

        // Act

        // Assert
        Assert.NotNull(companyEntitlementsConverter);
    }
}
