namespace Fossa.Licensing;

using System.Globalization;
using LanguageExt;
using TIKSN.Deployment;

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
    Seq<RegionInfo> Countries);
