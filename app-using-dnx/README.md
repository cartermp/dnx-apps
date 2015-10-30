This is a demonstrative library built via the command line and a text editor only (no Visaul Studio!).

It demonstrates how to build a library which can target the following:

* DNX Core
* .NET Core
* .NET Framework 4.5
* PCL Profile 344 (.NET Framework 4.5, Windows 8, Windows Phone 8.1, Windows Phone Silverlight 8, Silverlight 5)

It also demonstrates how to build an XUnit test which can be run without Visual Studio.

**Prerequisites:**

* Windows (because of the PCL and .NET Framework dependencies)
* DNX is installed on your machine and running the latest with coreclr as the runtime.

Open a command prompt and enter the following:

```
$ @powershell -NoProfile -ExecutionPolicy unrestricted -Command "&{$Branch='dev';iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.ps1'))}"
$ dnvm upgrade
$ dnvm install -r coreclr latest -u
```

**To Build:**

(assuming you are in /app-using-dnx)

```
$ dnu restore
$ cd src/SomeDependencyLib
$ dnu build
$ cd ../Lib
$ dnu build
```

**To Test:**

(assuming you are in /app-using-dnx)

```
$ dnu restore
$ cd test/Lib.Tests
$ dnx test
```

**To build as a NuGet package:**

(assuming you are in /app-using-dnx **and** have built)

```
$ cd src/Lib
$ dnu pack
```
