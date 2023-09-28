namespace Fossa.Licensing;

using TIKSN.Deployment;

/// <summary>
/// System Entitlements.
/// </summary>
/// <param name="SystemId">System ID.</param>
/// <param name="EnvironmentName">Deployment environment name.</param>
/// <param name="MaximumCompanyCount">Maximum Company Count.</param>
[CLSCompliant(false)]
public record SystemEntitlements(
    Ulid SystemId,
    EnvironmentName EnvironmentName,
    int MaximumCompanyCount);
