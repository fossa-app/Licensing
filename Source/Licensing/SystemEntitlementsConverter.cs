namespace Fossa.Licensing;

using System.Globalization;
using LanguageExt;
using LanguageExt.Common;
using TIKSN.Deployment;
using TIKSN.Licensing;
using static LanguageExt.Prelude;

/// <summary>
/// System Entitlements Converter.
/// </summary>
public class SystemEntitlementsConverter : IEntitlementsConverter<SystemEntitlements, SystemLicenseEntitlements>
{
    private static readonly Seq<Ulid> InvalidSystemIds =
        Seq(Ulid.Empty, Ulid.MinValue, Ulid.MaxValue);

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
            return errors.ToSeq();
        }

        if (InvalidSystemIds.Contains(entitlements.SystemId))
        {
            errors.Add(Error.New(1366359375, "Invalid System ID"));
        }

        result.SystemId = new ArraySegment<byte>(entitlements.SystemId.ToByteArray());

        if (string.IsNullOrWhiteSpace(entitlements.EnvironmentName.ToString()))
        {
            errors.Add(Error.New(1410594292, "Environment Name is missing"));
        }
        else
        {
            result.EnvironmentName = entitlements.EnvironmentName.ToString();
        }

        if (entitlements.MaximumCompanyCount <= 0)
        {
            errors.Add(Error.New(1390645275, "Maximum Company Count is invalid"));
        }
        else
        {
            result.MaximumCompanyCount = entitlements.MaximumCompanyCount;
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

        var systemId = new Ulid(entitlementsData.SystemId);

        if (InvalidSystemIds.Contains(systemId))
        {
            errors.Add(Error.New(670425534, "Invalid System ID"));
        }

        if (string.IsNullOrWhiteSpace(entitlementsData.EnvironmentName))
        {
            errors.Add(Error.New(429642749, "Environment Name is missing"));
        }

        if (entitlementsData.MaximumCompanyCount <= 0)
        {
            errors.Add(Error.New(391605441, "Maximum Company Count is invalid"));
        }

        var environmentName =
            EnvironmentName.Parse(entitlementsData.EnvironmentName, asciiOnly: true, CultureInfo.InvariantCulture);

        if (environmentName.IsNone)
        {
            errors.Add(Error.New(1804391968, "Environment Name is invalid"));
        }

        var environmentNameValue = environmentName.Match(s => s, () => throw new InvalidOperationException());

        if (errors.Count > 0)
        {
            return errors.ToSeq();
        }

        return new SystemEntitlements(
            systemId,
            environmentNameValue,
            entitlementsData.MaximumCompanyCount);
    }
}
