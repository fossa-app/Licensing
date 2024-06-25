namespace Fossa.Licensing;

using System;
using TIKSN.Licensing;

/// <summary>
/// System License Descriptor.
/// </summary>
public class SystemLicenseDescriptor : ILicenseDescriptor<SystemEntitlements>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SystemLicenseDescriptor"/> class.
    /// </summary>
    public SystemLicenseDescriptor()
    {
        this.Discriminator = Guid.Parse("09e60634-5eac-4b7a-8ffd-8f900cde7a8e");
        this.Name = "Fossa/System";
    }

    /// <summary>
    /// Gets System License Discriminator.
    /// </summary>
    public Guid Discriminator { get; }

    /// <summary>
    /// Gets System License Name.
    /// </summary>
    public string Name { get; }
}
