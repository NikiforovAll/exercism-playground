var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

var source = "./**/*.csproj";

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    var projects = GetFiles("./**/*.csproj");
    foreach(var project in projects)
    {
        CleanDirectory($"{project.GetDirectory().FullPath}/bin/{configuration}");
    }
    
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var projects = GetFiles("./**/*.csproj");
    foreach(var project in projects)
    {
        DotNetCoreBuild(project.GetDirectory().FullPath,
            new DotNetCoreBuildSettings() { Configuration = configuration });
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Test execution is not implemented");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
