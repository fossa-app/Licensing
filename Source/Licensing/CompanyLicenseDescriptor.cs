namespace Fossa.Licensing;

using System;
using TIKSN.Licensing;

/// <summary>
/// Company License Descriptor.
/// </summary>
public class CompanyLicenseDescriptor : ILicenseDescriptor<CompanyEntitlements>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyLicenseDescriptor"/> class.
    /// </summary>
    public CompanyLicenseDescriptor()
    {
        this.Discriminator = Guid.Parse("6619279b-50da-4138-924c-a971c02a9c5f");
        this.Name = "Fossa/Company";
    }

    /// <summary>
    /// Gets Company License Discriminator.
    /// </summary>
    public Guid Discriminator { get; }

    /// <summary>
    /// Gets Company License Name.
    /// </summary>
    public string Name { get; }
}
