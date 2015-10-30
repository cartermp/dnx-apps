using Xunit;
using Lib;
using SomeDependencyLib;

public class LibTest
{
    [Fact]
    public static void TestAdd()
    {
        Assert.Equal(4, Library.Foo());
    }

    [Fact]
    public static void TestUseLib()
    {
        string banana = "apple";

        Assert.NotEqual(banana, Library.Bar() /* returns "banana" */);
    }

    [Fact]
    public static void TestLibAndAppAreSame()
    {
        // Both return "banana"
        Assert.Equal(Library.Bar(), TheDependencyLib.GetName());
    }
}
