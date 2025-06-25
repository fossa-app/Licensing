namespace Fossa.Licensing;

using Google.Protobuf;
using LanguageExt;
using LanguageExt.Common;
using TIKSN.Licensing;
using static LanguageExt.Prelude;

/// <summary>
/// Company Entitlements Converter.
/// </summary>
public class CompanyEntitlementsConverter : IEntitlementsConverter<CompanyEntitlements, CompanyLicenseEntitlements>
{
    private static readonly Seq<long> InvalidCompanyIds =
        Seq(long.MinValue, long.MaxValue);

    private static readonly Seq<Ulid> InvalidSystemIds =
        Seq(Ulid.Empty, Ulid.MinValue, Ulid.MaxValue);

    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyEntitlementsConverter"/> class.
    /// </summary>
    public CompanyEntitlementsConverter()
    {
    }

    /// <summary>
    /// Convert from Domain Model to Data Model.
    /// </summary>
    /// <param name="entitlements">Domain Model.</param>
    /// <returns>Validation of Data Model.</returns>
    public Validation<Error, CompanyLicenseEntitlements> Convert(
        CompanyEntitlements entitlements)
    {
        var errors = new List<Error>();
        var result = new CompanyLicenseEntitlements();

        if (entitlements == null)
        {
            errors.Add(Error.New(1272977306, "Value must not be NULL"));
            return errors.ToSeq();
        }

        if (InvalidSystemIds.Contains(entitlements.SystemId))
        {
            errors.Add(Error.New(1366359375, "Invalid System ID"));
        }

        result.SystemId = ByteString.CopyFrom(entitlements.SystemId.ToByteArray());

        if (InvalidCompanyIds.Contains(entitlements.CompanyId) || entitlements.CompanyId <= 0)
        {
            errors.Add(Error.New(23274617, "Invalid Company ID"));
        }

        result.CompanyId = entitlements.CompanyId;

        if (entitlements.MaximumBranchCount <= 0)
        {
            errors.Add(Error.New(23275468, "Maximum Branch Count is invalid"));
        }
        else
        {
            result.MaximumBranchCount = entitlements.MaximumBranchCount;
        }

        if (entitlements.MaximumBranchCount <= 0)
        {
            errors.Add(Error.New(23275468, "Maximum Branch Count is invalid"));
        }
        else
        {
            result.MaximumBranchCount = entitlements.MaximumBranchCount;
        }

        if (entitlements.MaximumEmployeeCount <= 0)
        {
            errors.Add(Error.New(23275628, "Maximum Employee Count is invalid"));
        }
        else
        {
            result.MaximumEmployeeCount = entitlements.MaximumEmployeeCount;
        }

        if (entitlements.MaximumDepartmentCount <= 0)
        {
            errors.Add(Error.New(41537942, "Maximum Department Count is invalid"));
        }
        else
        {
            result.MaximumDepartmentCount = entitlements.MaximumDepartmentCount;
        }

        if (errors.Count > 0)
        {
            return errors.ToSeq();
        }

        return result;
    }

    /// <summary>
    /// Convert from Data Model to Domain Model.
    /// </summary>
    /// <param name="entitlementsData">Data Model.</param>
    /// <returns>Validation of Domain Model.</returns>
    public Validation<Error, CompanyEntitlements> Convert(
        CompanyLicenseEntitlements entitlementsData)
    {
        var errors = new List<Error>();

        if (entitlementsData is null)
        {
            errors.Add(Error.New(253116444, "Value must not be NULL"));

            return errors.ToSeq();
        }

        var systemId = new Ulid(entitlementsData.SystemId.ToByteArray());

        if (InvalidSystemIds.Contains(systemId))
        {
            errors.Add(Error.New(670425534, "Invalid System ID"));
        }

        if (InvalidCompanyIds.Contains(entitlementsData.CompanyId) || entitlementsData.CompanyId <= 0)
        {
            errors.Add(Error.New(23274617, "Invalid Company ID"));
        }

        if (entitlementsData.MaximumBranchCount <= 0)
        {
            errors.Add(Error.New(23275468, "Maximum Branch Count is invalid"));
        }

        if (entitlementsData.MaximumEmployeeCount <= 0)
        {
            errors.Add(Error.New(23275628, "Maximum Employee Count is invalid"));
        }

        if (entitlementsData.MaximumDepartmentCount <= 0)
        {
            errors.Add(Error.New(41537900, "Maximum Department Count is invalid"));
        }

        if (errors.Count > 0)
        {
            return errors.ToSeq();
        }

        return new CompanyEntitlements(
            systemId,
            entitlementsData.CompanyId,
            entitlementsData.MaximumBranchCount,
            entitlementsData.MaximumEmployeeCount,
            entitlementsData.MaximumDepartmentCount);
    }
}
