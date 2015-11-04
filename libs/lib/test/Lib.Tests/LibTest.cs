using Xunit;
using Lib;
using System.Threading.Tasks;
using System;

public class LibTest
{	
	[Fact]
	public async Task GetDotNetCountTest()
	{
		string result = await new Library().GetDotNetCountAsync();
		
		Console.WriteLine(result);
		
		Assert.False(string.IsNullOrWhiteSpace(result));
	}
}