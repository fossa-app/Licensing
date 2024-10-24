namespace Fossa.Licensing;

/// <summary>
/// Company Entitlements.
/// </summary>
/// <param name="SystemId">System ID.</param>
/// <param name="CompanyId">Company ID.</param>
/// <param name="MaximumBranchCount">Maximum Branch Count.</param>
/// <param name="MaximumEmployeeCount">Maximum Employee Count.</param>
[CLSCompliant(false)]
public record CompanyEntitlements(
    Ulid SystemId,
    long CompanyId,
    int MaximumBranchCount,
    int MaximumEmployeeCount);