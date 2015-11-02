# Targeting with .NET Core Libraries

blah blah some intro

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

And that's it!  Run `dotnet restore` and `dotnet build` from the command line, and your library can now be built!

**NOTE:** This assumes your code will compile across *both* .NET Core and .NET Framework.  Read the section on cross-compiling with `#ifdef`s on how to compile the same file differently for each target if you are using features which are unavailable in some of your targets.

## How do I target a PCL?

Targeting a PCL profile is a bit trickier.  For starters, [reference this](http://embed.plnkr.co/03ck2dCtnJogBKHJ9EjY) list of PCL profiles to ensure you have the correct target.  Hover over the Name of each entry for the framework identifier, which you will need.

Unfortunately, when you will need to list the dependencies for each target when you wish to additionally target a PCL with your library.  Something like this:

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

Additionally, you will need to list the libraries you choose to reference inside each PCL target profile in your `project.json` file.

Here is an example targeting PCL Profile 344 and .NET Core, using only `System.Runtime` as a dependency.  Note that you need to have .NET Framework already installed on your machine.

```
{
    "frameworks":{
        "dotnet":{
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

This will allow you to target .NET Core, .NET Framework 4.5, Windows 8, Windows Phone 8.1, and Silverlight 5.0.

## How do I cross-compile to use newer features for newer versions of .NET?

some stuff about how to do `#ifdef`s
