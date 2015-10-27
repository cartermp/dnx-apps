using Xunit;
using AppUsingDnx;

public class AppTest
{
    [Fact]
    public static void TestAdd()
    {
        Assert.Equal(4, App.Foo());
    }
}
