using Xunit;
using AppUsingDnx;
using Lib;

public class AppTest
{
    [Fact]
    public static void TestAdd()
    {
        Assert.Equal(4, App.Foo());
    }

    [Fact]
    public static void TestUseLib()
    {
        string banana = "apple";

        Assert.NotEqual(banana, App.UseLib() /* returns "banana" */);
    }

    [Fact]
    public static void TestLibAndAppAreSame()
    {
        // Both return "banana"
        Assert.Equal(App.UseLib(), Library.GetName());
    }
}
