#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs
#:property RunWorkingDirectory=$(MSBuildProjectDirectory)/..
#:package Cake.BuildSystems.Module@8.0.0

var target = Argument("target", "Pack");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(context =>
{
    var configuration = Argument("configuration", "Release");

    var myService = ServiceProvider.GetRequiredService<IMyService>();
    var version = myService.GetVersion();

    Information(
        "Building Version: {0}, Configuration: {1}",
        version,
        configuration);

    return new BuildData(
        Version: version,
        Configuration: configuration,
        SolutionFile: MakeAbsolute(File("./src/Example.sln")),
        ArtifactsDirectory: MakeAbsolute(Directory("./artifacts")),
        MSBuildSettings: new DotNetMSBuildSettings()
                            .SetVersion(version)
                            .SetConfiguration(configuration)
                            .WithProperty("TreatWarningsAsErrors", "true"),
        Rebuild: HasArgument("rebuild"));
});

Task("Clean")
    .WithCriteria<BuildData>(c => c.Rebuild, nameof(BuildData.Rebuild))
    .Does<BuildData>((ctx, data) =>
{
    CleanDirectories(data.DirectoriesToClean);
});

Task("Build")
    .IsDependentOn("Clean")
    .Does<BuildData>((ctx, data) =>
{
    DotNetBuild(
        data.SolutionFile.FullPath,
        new DotNetBuildSettings
        {
            MSBuildSettings = data.MSBuildSettings
        });
});

Task("Test")
    .IsDependentOn("Build")
    .Does<BuildData>((ctx, data) =>
{
    DotNetTest(
        data.SolutionFile.FullPath,
        new DotNetTestSettings
        {
            MSBuildSettings = data.MSBuildSettings,
            NoRestore = true,
            NoBuild = true
        });
});

Task("Pack")
    .IsDependentOn("Test")
    .Does<BuildData>((ctx, data) =>
{
    DotNetPack(
        data.SolutionFile.FullPath,
        new DotNetPackSettings
        {
            MSBuildSettings = data.MSBuildSettings,
            NoRestore = true,
            NoBuild = true,
            OutputDirectory = data.ArtifactsDirectory
        });
});

Task("UploadArtifacts")
    .IsDependentOn("Pack")
    .Does<BuildData>((ctx, data) =>
        GitHubActions.Commands.UploadArtifact(
            data.ArtifactsDirectory,
            "ExampleArtifacts"));

Task("GitHubActions")
    .IsDependentOn("UploadArtifacts");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
