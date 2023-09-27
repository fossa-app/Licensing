namespace Fossa.Licensing;

/// <summary>
/// System Entitlements.
/// </summary>
/// <param name="EnvironmentName">Deployment environment name.</param>
/// <param name="MaximumCompanyCount">Maximum Company Count.</param>
public record SystemEntitlements(
    string EnvironmentName,
    int MaximumCompanyCount);
