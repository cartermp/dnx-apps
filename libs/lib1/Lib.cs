using System;

#if NET40
using System.Net;
#else
// Generic collections are just a part of
using System.Net.Http;
//using System.Threading.Tasks;
#endif

using System.Text.RegularExpressions;

namespace Lib
{
    public class Foo
    {
#if NET40
        private readonly WebClient _client = new WebClient();
#else
        private readonly HttpClient _client = new HttpClient();
#endif

#if NET40
        public string GetDotNetCount()
        {
            string url = "http://www.dotnetfoundation.org/";
          
            var uri = new Uri(url);
            var result = _client.DownloadString(uri);
            
            int dotNetCount = Regex.Matches(result, ".NET").Count;
            
            return $"Dotnet Foundation mentions .NET {dotNetCount} times!";
        }
#else
        public string GetDotNetCountAsync()
        {
            string url = "http://www.dotnetfoundation.org/";
            
            var result = await _client.GetStringAsync(url);
            
            int dotNetCount = Regex.Matches(result, ".NET").Count;
            
            return $"Dotnet Foundation mentions .NET {dotNetCount} times!";
        }
#endif
    }
}
