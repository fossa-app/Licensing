namespace Fossa.Licensing;

using LanguageExt;
using TIKSN.Deployment;
using TIKSN.Globalization;

/// <summary>
/// System Entitlements.
/// </summary>
/// <param name="SystemId">System ID.</param>
/// <param name="EnvironmentName">Deployment environment name.</param>
/// <param name="MaximumCompanyCount">Maximum Company Count.</param>
/// <param name="Countries">Supported Countries.</param>
public record SystemEntitlements(
    Ulid SystemId,
    EnvironmentName EnvironmentName,
    int MaximumCompanyCount,
    Seq<CountryInfo> Countries);
