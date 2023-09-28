namespace Fossa.Licensing.Test;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TIKSN.DependencyInjection;
using TIKSN.Licensing;
using TIKSN.Time;
using Xunit;

public class SystemEntitlementsConverterTest
{
    private readonly IServiceProvider serviceProvider;

    public SystemEntitlementsConverterTest()
    {
        var services = new ServiceCollection();
        _ = services.AddLicense();
        _ = services.AddFrameworkPlatform();

        var fakeTimeProvider = Substitute.For<ITimeProvider>();
        _ = fakeTimeProvider.GetCurrentTime()
            .Returns(new DateTimeOffset(2022, 9, 24, 0, 0, 0, TimeSpan.Zero));
        _ = services.AddSingleton(fakeTimeProvider);

        ContainerBuilder containerBuilder = new();
        _ = containerBuilder.RegisterModule<CoreModule>();
        _ = containerBuilder.RegisterModule<PlatformModule>();
        containerBuilder.Populate(services);

        this.serviceProvider = new AutofacServiceProvider(containerBuilder.Build());
    }

    [Fact]
    public void Given_When_Then()
    {
        // Arrange

        var systemLicenseFactory = this.serviceProvider
            .GetRequiredService<ILicenseFactory<SystemEntitlements, SystemLicenseEntitlements>>();

        // Act

        // Assert

        _ = systemLicenseFactory.Should().NotBeNull();
    }
}
