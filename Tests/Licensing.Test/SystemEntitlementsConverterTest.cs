namespace Fossa.Licensing.Test;

using Fossa.Licensing;
using Xunit;

public class SystemEntitlementsConverterTest
{
    [Fact]
    public void Given_When_Then()
    {
        var sut = new SystemEntitlementsConverter();

        Assert.NotNull(sut);
    }
}
