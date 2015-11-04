using System;

#if NET40
using System.Net;
#else
// Generic collections are just a part of
using System.Net.Http;
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
        public string Whee()
        {
            string url = "http://www.dotnetfoundation.org/";
            
#if NET40
            var uri = new Uri(url);
            var result = _client.DownloadString(uri);
#else
            var result = _client.GetStringAsync(url).Result;
#endif
            
            int dotNetCount = Regex.Matches(result, ".NET").Count;
            
            return $"Dotnet Foundation mentions .NET {dotNetCount} times!";
        }
    }
}
