# Targeting with .NET Core Libraries

blah blah some intro

## What do I do if I want to target .NET Core?

The first step is to decide on the *version* of .NET Core you need to target.  [This document](https://github.com/davidfowl/aspnetvnextwebapiapp/blob/master/Generations.md) outlines everything you need to know, in-depth, about what each .NET Core target means.

The distilled version is this:

1. Pick any one of `dotnet51`, `dotnet52`, `dotnet53`, `dotnet54`, or `dotnet55` if you don't care about sharing code which can also run on .NET Framework.

// TODO add more

## How do I target .NET Core and .NET Framework?

To target specific versions of .NET Framework, add the relevant Target Framework Moniker, such as `net45` to your `project.json` file.  Below is a sample `project.json` file which targets .NET 4.0, .NET 4.5, and .NET Core.

```
{
    "dependencies":{
        "System.Runtime":"4.0.0-rc1-*"
    },
    "frameworks":{
        "net40":{},
        "net45":{},
        "dotnet":{}
    }
}
```

And that's it!  Run `dnu restore` and `dnu build` from the command line, and your library can now be built!  You will now notice three new entries in your `/bin` folder:

```
/bin
   /Debug
      /net40
      /net45
      /dotnet
```

Finally, running `dnu pack` will build a NuGet package, and your `/bin/Debug` folder will look like this:

```
/bin
   /Debug
      /net40
      /net45
      /dotnet
      Lib.1.0.0.nupkg
      Lib.1.0.0.symbols.nupkg
```

And now you have the necessary files to publish a NuGet package!

**NOTE:** This assumes your code will compile across *both* .NET Core and .NET Framework.  Read the section on cross-compiling with `#if`s on how to compile the same file differently for each target if you are using features which are unavailable in some of your targets.

## How do I target a PCL?

Targeting a PCL profile is a bit trickier.  For starters, [reference this list of PCL profiles](http://embed.plnkr.co/03ck2dCtnJogBKHJ9EjY) to ensure you have the correct target.  Hover over the Name of each entry for the framework identifier, which you will need.

Unfortunately, you will need to list the dependencies for each target when you wish to additionally target a PCL with your library.  Something like this:

```
{
    "dependencies":{
        "System.Runtime":"4.0.0-rc1-*"
    },
    "frameworks":{
        "dotnet":{},
        "the-pcl-profile-i'm-targeting":{}
    }
}
```

**Will not work!**

Additionally, you will need to list the framework assemblies you choose to reference inside each PCL target profile in your `project.json` file.  This will likely require, at a minimum, `mscorlib`, `System`, and `System.Core`.

Here is an example targeting PCL Profile 344 and .NET Core, using only `System.Runtime` as a dependency.

```
{
    "frameworks":{
        "dotnet51":{
           "dependencies":{
                "System.Runtime":"4.0.0-rc1-*"
            }
        },
        "dotnet55":{
           "dependencies":{
                "System.Runtime":"4.0.0-rc1-*"
            }
        },
        ".NETPortable,Version=v4.0,Profile=Profile344":{
            "frameworkAssemblies":{
                "mscorlib":"",
                "System":"",
                "System.Core":""
            }
        }
    }
}
```

This will allow you to target .NET Core 5.1, .NET Core 5.5, .NET Framework 4.5, Windows 8, Windows Phone 8.1, and Silverlight 5.0.

Run `dnu restore` and `dnu build` from the command line, and your library can now be built!  You will now notice three new entries in your `/bin/Debug` folder:

```
/bin
   /Debug
      /dotnet51
      /dotnet55
      /portable-net45+sl50+netcore45+wpa81+wp8
```

Finally, run `dnu pack` to build a NuGet pakage, and your `/bin/Debug` folder should look like this:

```
/bin
   /Debug
      /dotnet51
      /dotnet55
      /portable-net45+sl50+netcore45+wpa81+wp8
      Lib.1.0.0.nupkg
      Lib.1.0.0.symbols.nupkg
```

And now you can publish a NuGet package!

**NOTE:** This assumes your code will compile across *both* .NET Core and .NET Framework.  Read the section on cross-compiling with `#if`s on how to compile the same file differently for each target if you are using features which are unavailable in some of your targets.

## How do I cross-compile to use newer features for newer versions of .NET?

Newer versions of .NET introduce improved APIs that you'll want to take advantage of.  However, if you're targeting an older version of .NET, you need to also support older APIs!  The solution for this is to cross-compile with `#if` guards.

For example, in .NET Core you can use `HttpClient` in the `System.Net.Http` API to perform network operations over Http.  However, the .NET 4.0 Framework does not include `HttpClient`!  Instead, you may need to use `WebClient` from the `System.Net` assembly.

First, the `project.json` file should look something like this:

```
{
    "frameworks":{
        "net40":{
            "frameworkAssemblies": {
                "System.Net":"",
                "System.Text.RegularExpressions":""
            }
            
        },
        "dotnet":{
            "dependencies": {
                "System.Runtime":"4.0.0-rc1-*",
                "System.Net.Http": "4.0.1-beta-23409",
                "System.Text.RegularExpressions": "4.0.11-beta-23409"
            }
        }
    }
}

```

Note that framework assemblies being used are explicitly referenced in the `net40` target, and NuGet references are also explictly listed in the `dotnet` target.

Next, your `using`s in your source file can be adjusted like this:

```csharp
#ifdef NET40
// This only compiles for non .NET 4.0 targets
using System.Net;
#else
// This compiles for all other targets
using System.Net.Http;
#endif
```

And further down in the source, you can use guards to use those libraries conditionally:

```csharp
    public class Library
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
        public async Task<string> GetDotNetCountAsync()
        {
            string url = "http://www.dotnetfoundation.org/";
            
            var result = await _client.GetStringAsync(url);
            
            int dotNetCount = Regex.Matches(result, ".NET").Count;
            
            return $"dotnetfoundation.orgmentions .NET {dotNetCount} times in its HTML!";
        }
#endif
    }
```

And that's it!

## But What about Portable Libraries?

//TODO -- replace with actual libraries

PCLs add *one* more thing to do before you can use `#if`s to conditionally compile different targets: **you need to add a moniker in your** `project.json` **file!**.

For example, if you wanted to targeting PCL profile 344, you may want to refer it to as "PORTABLE344" when cross-compiling.  Simply add it to the `project.json` file as a `compilationOptions` attribute:

```
{
    "frameworks":{
        "dotnet51":{
           "dependencies":{
                "System.Runtime":"4.0.0-rc1-*"
            }
        },
        "dotnet55":{
           "dependencies":{
                "System.Runtime":"4.0.0-rc1-*"
            }
        },
        ".NETPortable,Version=v4.0,Profile=Profile344":{
            "compilationOptions": {
                "define": [ "PORTABLE344" ]
            },
            "frameworkAssemblies":{
                "mscorlib":"",
                "System":"",
                "System.Core":""
            }
        }
    }
}

```

Now you can conditionally compile against that target:

```csharp
#if PORTABLE344
using System.Foo.Portable344CompatibleLibrary;
#endif
```

And that's it! `PORTABLE344` is now recognized by the compiler, and the `.dll` generated for that target will now contain `System.Foo.Portable344CompatibleLibrary`, but your other target `.dll`s will not.
