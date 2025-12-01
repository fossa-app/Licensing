# Fossa Licensing

[![Licensing NuGet Package](https://img.shields.io/nuget/v/Fossa.Licensing.svg)](https://www.nuget.org/packages/Fossa.Licensing/) [![Licensing NuGet Package Downloads](https://img.shields.io/nuget/dt/Fossa.Licensing)](https://www.nuget.org/packages/Fossa.Licensing) [![GitHub Actions Status](https://github.com/fossa-app/Licensing/workflows/Build/badge.svg?branch=main)](https://github.com/fossa-app/Licensing/actions)
[![StandWithUkraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://github.com/vshymanskyy/StandWithUkraine/blob/main/docs/README.md)

## Purpose

The Fossa Licensing project provides a robust and extensible .NET library for managing software licenses and entitlements. It is designed to support both system-level and company-level licensing, offering a flexible framework for defining, converting, and validating license data. This library leverages Protocol Buffers for efficient data serialization and gRPC for potential future integration with remote licensing services.

## Technical Details

### Architecture

The core architecture revolves around three main interfaces from the `TIKSN.Licensing` framework:

* `ILicenseDescriptor<TEntitlements>`: Defines metadata for a specific license type, including a unique discriminator (GUID) and a human-readable name.
* `IEntitlementsConverter<TEntitlements, TEntitlementsData>`: Handles the conversion between the domain-specific `TEntitlements` object and its Protocol Buffer data model (`TEntitlementsData`), including comprehensive validation rules.
* `ILicenseFactory<TEntitlements, TEntitlementsData>`: Provides methods for creating and validating licenses based on the defined entitlements and their data representations.

### Data Model

The project defines two primary entitlement types, serialized using Protocol Buffers (`schema.proto`):

#### `SystemLicenseEntitlements`

Represents system-wide licensing parameters.

* `SystemId` (bytes): A unique identifier for the system (ULID).
* `EnvironmentName` (string): The deployment environment name.
* `MaximumCompanyCount` (int32): The maximum number of companies allowed for the system.
* `CountryCodes` (repeated string): A list of two-letter ISO region names representing supported countries.

The corresponding C# domain model is `SystemEntitlements`.

#### `CompanyLicenseEntitlements`

Represents licensing parameters specific to a company within a system.

* `SystemId` (bytes): The unique identifier of the parent system (ULID).
* `CompanyId` (int64): A unique identifier for the company.
* `MaximumBranchCount` (int32): The maximum number of branches allowed for the company.
*   `MaximumEmployeeCount` (int32): The maximum number of employees allowed for the company.
*   `MaximumDepartmentCount` (int32): The maximum number of departments allowed for the company.

The corresponding C# domain model is `CompanyEntitlements`.

### Dependencies

*   `Grpc.Tools`: Provides tooling for Protocol Buffers and gRPC.
*   `TIKSN-Framework`: A foundational framework providing common utilities and abstractions, including the licensing interfaces used here.

## Usage Instructions

To integrate Fossa Licensing into your .NET application, you can register the necessary services using the `AddLicense` extension method on `IServiceCollection`.

### Service Registration

```csharp
using Fossa.Licensing;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLicense();
        // ... other service registrations
    }
}
```

### Creating and Validating Licenses

After registering the services, you can inject `ILicenseFactory` and `IEntitlementsConverter` to create and validate licenses.

#### Example: System License

```csharp
using Fossa.Licensing;
using TIKSN.Licensing;
using LanguageExt;
using System.Globalization;

public class SystemLicenseService
{
    private readonly ILicenseFactory<SystemEntitlements, SystemLicenseEntitlements> _systemLicenseFactory;
    private readonly IEntitlementsConverter<SystemEntitlements, SystemLicenseEntitlements> _systemEntitlementsConverter;

    public SystemLicenseService(
        ILicenseFactory<SystemEntitlements, SystemLicenseEntitlements> systemLicenseFactory,
        IEntitlementsConverter<SystemEntitlements, SystemLicenseEntitlements> systemEntitlementsConverter)
    {
        _systemLicenseFactory = systemLicenseFactory;
        _systemEntitlementsConverter = systemEntitlementsConverter;
    }

    public void WorkWithSystemLicenses()
    {
        // 1. Define System Entitlements
        var systemEntitlements = new SystemEntitlements(
            SystemId: Ulid.NewUlid(),
            EnvironmentName: new TIKSN.Deployment.EnvironmentName("Production"),
            MaximumCompanyCount: 100,
            Countries: LanguageExt.Seq.create(new RegionInfo("US"), new RegionInfo("CA")));

        // 2. Convert Domain Model to Data Model (Protobuf)
        var systemLicenseEntitlementsValidation = _systemEntitlementsConverter.Convert(systemEntitlements);

        systemLicenseEntitlementsValidation.Match(
            Success: systemLicenseEntitlementsData =>
            {
                // 3. Create a License
                var license = _systemLicenseFactory.Create(systemLicenseEntitlementsData);

                // 4. Validate the License
                var validationResult = _systemLicenseFactory.Validate(license);

                validationResult.Match(
                    Success: validEntitlements =>
                    {
                        Console.WriteLine("System License is valid!");
                        Console.WriteLine($"Max Company Count: {validEntitlements.MaximumCompanyCount}");
                    },
                    Fail: errors =>
                    {
                        Console.WriteLine("System License validation failed:");
                        errors.ToList().ForEach(e => Console.WriteLine($"- {e.Message}"));
                    });
            },
            Fail: errors =>
            {
                Console.WriteLine("System Entitlements conversion failed:");
                errors.ToList().ForEach(e => Console.WriteLine($"- {e.Message}"));
            });
    }
}
```

#### Example: Company License

```csharp
using Fossa.Licensing;
using TIKSN.Licensing;
using LanguageExt;

public class CompanyLicenseService
{
    private readonly ILicenseFactory<CompanyEntitlements, CompanyLicenseEntitlements> _companyLicenseFactory;
    private readonly IEntitlementsConverter<CompanyEntitlements, CompanyLicenseEntitlements> _companyEntitlementsConverter;

    public CompanyLicenseService(
        ILicenseFactory<CompanyEntitlements, CompanyLicenseEntitlements> companyLicenseFactory,
        IEntitlementsConverter<CompanyEntitlements, CompanyLicenseEntitlements> companyEntitlementsConverter)
    {
        _companyLicenseFactory = companyLicenseFactory;
        _companyEntitlementsConverter = companyEntitlementsConverter;
    }

    public void WorkWithCompanyLicenses()
    {
        // 1. Define Company Entitlements
        var companyEntitlements = new CompanyEntitlements(
            SystemId: Ulid.NewUlid(), // Should match a valid system ID
            CompanyId: 123,
            MaximumBranchCount: 5,
            MaximumEmployeeCount: 50,
            MaximumDepartmentCount: 10);

        // 2. Convert Domain Model to Data Model (Protobuf)
        var companyLicenseEntitlementsValidation = _companyEntitlementsConverter.Convert(companyEntitlements);

        companyLicenseEntitlementsValidation.Match(
            Success: companyLicenseEntitlementsData =>
            {
                // 3. Create a License
                var license = _companyLicenseFactory.Create(companyLicenseEntitlementsData);

                // 4. Validate the License
                var validationResult = _companyLicenseFactory.Validate(license);

                validationResult.Match(
                    Success: validEntitlements =>
                    {
                        Console.WriteLine("Company License is valid!");
                        Console.WriteLine($"Max Employee Count: {validEntitlements.MaximumEmployeeCount}");
                    },
                    Fail: errors =>
                    {
                        Console.WriteLine("Company License validation failed:");
                        errors.ToList().ForEach(e => Console.WriteLine($"- {e.Message}"));
                    });
            },
            Fail: errors =>
            {
                Console.WriteLine("Company Entitlements conversion failed:");
                errors.ToList().ForEach(e => Console.WriteLine($"- {e.Message}"));
            });
    }
}
```
