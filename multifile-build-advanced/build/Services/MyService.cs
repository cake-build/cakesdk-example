public class MyService : IMyService
{
    public string GetVersion()
    {
        InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=6.3.0");
        var version = GitVersion();
        return version.FullSemVer;
    }
}
