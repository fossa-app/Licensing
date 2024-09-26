namespace Fossa.Licensing;

using Microsoft.Extensions.DependencyInjection;
using TIKSN.Licensing;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add License.
    /// </summary>
    /// <param name="services">Input Service Collection.</param>
    /// <returns>Populated Service Collection.</returns>
    public static IServiceCollection AddLicense(this IServiceCollection services)
    {
        _ = services
            .AddSingleton<
                ILicenseFactory<SystemEntitlements, SystemLicenseEntitlements>,
                LicenseFactory<SystemEntitlements, SystemLicenseEntitlements>>();

        _ = services
            .AddSingleton<
                IEntitlementsConverter<SystemEntitlements, SystemLicenseEntitlements>,
                SystemEntitlementsConverter>();

        _ = services
            .AddSingleton<
                ILicenseDescriptor<SystemEntitlements>,
                SystemLicenseDescriptor>();

        _ = services
            .AddSingleton<
                ILicenseFactory<CompanyEntitlements, CompanyLicenseEntitlements>,
                LicenseFactory<CompanyEntitlements, CompanyLicenseEntitlements>>();

        _ = services
            .AddSingleton<
                IEntitlementsConverter<CompanyEntitlements, CompanyLicenseEntitlements>,
                CompanyEntitlementsConverter>();

        _ = services
            .AddSingleton<
                ILicenseDescriptor<CompanyEntitlements>,
                CompanyLicenseDescriptor>();

        return services;
    }
}
