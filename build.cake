// Usings
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

// Arguments
var target = Argument<string>("target", "Default");
var source = Argument<string>("source", null);
var apiKey = Argument<string>("apikey", null);
var version = Argument<string>("targetversion", "0.1.0-alpha");
var skipClean = Argument<bool>("skipclean", false);
var skipTests = Argument<bool>("skiptests", false);
var nogit = Argument<bool>("nogit", false);

// Variables
var configuration = "Release";
var projectJsonFiles = GetFiles("./src/**/project.json");

// Directories
var output = Directory("build");
var outputBinaries = output + Directory("binaries");
var outputBinariesNet452 = outputBinaries + Directory("net452");
var outputBinariesNetstandard = outputBinaries + Directory("netstandard1.6");
var outputPackages = output + Directory("packages");
var outputNuGet = output + Directory("nuget");

///////////////////////////////////////////////////////////////

Task("Clean")
  .Does(() =>
{
  // Clean artifact directories.
  CleanDirectories(new DirectoryPath[] {
    output, outputBinaries, outputPackages, outputNuGet,
    outputBinariesNet452, outputBinariesNetstandard
  });

  if(!skipClean) {
    // Clean output directories.
    CleanDirectories("./src/**/" + configuration);
    CleanDirectories("./test/**/" + configuration);
    CleanDirectories("./samples/**/" + configuration);
  }
});

Task("Restore-NuGet-Packages")
  .Description("Restores NuGet packages")
  .Does(() =>
{
  var settings = new DotNetCoreRestoreSettings
  {
    Verbose = false,
    Verbosity = DotNetCoreRestoreVerbosity.Warning,
    Sources = new [] {
        "https://www.myget.org/F/xunit/api/v3/index.json",
        "https://dotnet.myget.org/F/dotnet-core/api/v3/index.json",
        "https://dotnet.myget.org/F/cli-deps/api/v3/index.json",
        "https://api.nuget.org/v3/index.json",
    },
  };

  //Restore at root until preview1-002702 bug fixed
  DotNetCoreRestore("./", settings);
});

Task("Compile")
  .Description("Builds the solution")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore-NuGet-Packages")
  .Does(() =>
{
  var projects = GetFiles("./**/*.xproj");

  foreach(var project in projects)
  {
    DotNetCoreBuild(project.GetDirectory().FullPath, new DotNetCoreBuildSettings {
      Configuration = configuration,
      Verbose = false
    });
  }
});

Task("Test")
  .Description("Executes xUnit tests")
  .WithCriteria(!skipTests)
  .IsDependentOn("Compile")
  .Does(() =>
{
  var projects = GetFiles("./test/**/*.xproj");

  foreach(var project in projects)
  {
      DotNetCoreTest(project.GetDirectory().FullPath, new DotNetCoreTestSettings {
        Configuration = configuration
      });
  }
});

Task("Publish")
  .Description("Gathers output files and copies them to the output folder")
  .IsDependentOn("Compile")
  .Does(() =>
{
  // Copy net452 binaries.
  CopyFiles(GetFiles("src/**/bin/" + configuration + "/net452/*.dll")
    + GetFiles("src/**/bin/" + configuration + "/net452/*.xml")
    + GetFiles("src/**/bin/" + configuration + "/net452/*.pdb")
    + GetFiles("src/**/bin/" + configuration + "/net452/*.exe")
    , outputBinariesNet452);

  // Copy netstandard binaries.
  CopyFiles(GetFiles("src/**/bin/" + configuration + "/netstandard1.6/*.dll")
    + GetFiles("src/**/bin/" + configuration + "/netstandard1.6/*.xml")
    + GetFiles("src/**/bin/" + configuration + "/netstandard1.6/*.pdb")
    + GetFiles("src/**/bin/" + configuration + "/netstandard1.6/*.exe")
    , outputBinariesNetstandard);
});

Task("Package")
  .Description("Zips up the built binaries for easy distribution")
  .IsDependentOn("Publish")
  .Does(() =>
{
  var package = outputPackages + File("RabbitShelf-Latest.zip");
  var files = GetFiles(outputBinaries.Path.FullPath + "/**/*");

  Zip(outputBinaries, package, files);
});


Task("Package-NuGet")
  .Description("Generates NuGet packages for each project that contains a nuspec")
  .Does(() =>
{
  var projects = GetFiles("./src/**/*.xproj");
  foreach(var project in projects)
  {
    var settings = new DotNetCorePackSettings {
      Configuration = "Release",
      OutputDirectory = outputNuGet
    };

    DotNetCorePack(project.GetDirectory().FullPath, settings);
  }
});

///////////////////////////////////////////////////////////////

Task("Default")
  .IsDependentOn("Test")
  .IsDependentOn("Package-NuGet")
  .IsDependentOn("Package");



///////////////////////////////////////////////////////////////

RunTarget(target);
