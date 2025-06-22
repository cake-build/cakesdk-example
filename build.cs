#:sdk Cake.Sdk
#:package Cake.BuildSystems.Module@7.1.0

var target = Argument("target", "Pack");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(context =>
{
    var configuration = Argument("configuration", "Release");

    InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=6.3.0");
    var version = GitVersion();

    Information(
        "Building Version: {0}, Configuration: {1}",
        version.FullSemVer,
        configuration);

    return new BuildData(
        Version: version.FullSemVer,
        Configuration: configuration,
        SolutionFile: "./src/Example.sln",
        ArtifactsDirectory: "./artifacts",
        MSBuildSettings: new DotNetMSBuildSettings()
                            .SetVersion(version.FullSemVer)
                            .SetConfiguration(configuration)
                            .WithProperty("WarnAsError", "true"));
});

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
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


//////////////////////////////////////////////////////////////////////
// Models
//////////////////////////////////////////////////////////////////////

public record BuildData(
    string Version,
    string Configuration,
    FilePath SolutionFile,
    DirectoryPath ArtifactsDirectory,
    DotNetMSBuildSettings MSBuildSettings
)
{
    public DirectoryPath[] DirectoriesToClean { get; init; } =
        [
            $"./src/Example/bin/{Configuration}",
            $"./src/Example/obj/{Configuration}",
            ArtifactsDirectory
        ];
}