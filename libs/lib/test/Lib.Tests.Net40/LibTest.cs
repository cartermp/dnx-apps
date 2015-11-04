using System;

using Xunit;
using Lib;

public class LibTest
{
	[Fact]
	public void GetDotNetCountTest()
	{
		string result = new Library().GetDotNetCount();
		
		Console.WriteLine(result)
		
		Assert.False(string.IsNullOrWhiteSpace(result));
	}
}