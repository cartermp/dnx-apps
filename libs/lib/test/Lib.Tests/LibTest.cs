using Xunit;
using Lib;

public class LibTest
{
	/*
	[Fact]
	public static void Net40Test()
	{
		string result = new Library().GetDotNetCount();
		
		Assert.False(string.IsNullOrEmpty(result));
	}
	*/
	
	[Fact]
	public async Task PostNet40Test()
	{
		string result = await new Library().GetDotNetCountAsync();
		
		Assert.False(string.IsNullOrWhiteSpace(result));
	}
}