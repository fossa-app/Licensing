namespace Fossa.Licensing;

using LanguageExt;
using LanguageExt.Common;
using TIKSN.Licensing;

/// <summary>
/// System Entitlements Converter.
/// </summary>
public class SystemEntitlementsConverter : IEntitlementsConverter<SystemEntitlements, SystemLicenseEntitlements>
{
    /// <summary>
    /// Convert from Domain Model to Data Model.
    /// </summary>
    /// <param name="entitlements">Domain Model.</param>
    /// <returns>Validation of Data Model.</returns>
    [CLSCompliant(false)]
    public Validation<Error, SystemLicenseEntitlements> Convert(
        SystemEntitlements entitlements)
    {
        var errors = new List<Error>();
        var result = new SystemLicenseEntitlements();

        if (entitlements == null)
        {
            errors.Add(Error.New(1272977306, "Value must not be NULL"));
        }
        else
        {
            if (string.IsNullOrWhiteSpace(entitlements.EnvironmentName))
            {
                errors.Add(Error.New(1410594292, "Environment Name is missing"));
            }
            else
            {
                result.EnvironmentName = entitlements.EnvironmentName;
            }

            if (entitlements.MaximumCompanyCount <= 0)
            {
                errors.Add(Error.New(1390645275, "Maximum Company Count is invalid"));
            }
            else
            {
                result.MaximumCompanyCount = entitlements.MaximumCompanyCount;
            }
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
    [CLSCompliant(false)]
    public Validation<Error, SystemEntitlements> Convert(
        SystemLicenseEntitlements entitlementsData)
    {
        var errors = new List<Error>();

        if (entitlementsData == null)
        {
            errors.Add(Error.New(253116444, "Value must not be NULL"));

            return errors.ToSeq();
        }

        if (string.IsNullOrWhiteSpace(entitlementsData.EnvironmentName))
        {
            errors.Add(Error.New(429642749, "Environment Name is missing"));
        }

        if (entitlementsData.MaximumCompanyCount <= 0)
        {
            errors.Add(Error.New(391605441, "Maximum Company Count is invalid"));
        }

        if (errors.Count > 0)
        {
            return errors.ToSeq();
        }

        return new SystemEntitlements(
            entitlementsData.EnvironmentName,
            entitlementsData.MaximumCompanyCount);
    }
}
