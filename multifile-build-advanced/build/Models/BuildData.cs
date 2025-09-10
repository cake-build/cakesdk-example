public record BuildData(
    string Version,
    string Configuration,
    FilePath SolutionFile,
    DirectoryPath ArtifactsDirectory,
    DotNetMSBuildSettings MSBuildSettings,
    bool Rebuild
)
{
    public DirectoryPath[] DirectoriesToClean { get; init; } =
        [
            $"./src/Example/bin/{Configuration}",
            $"./src/Example/obj/{Configuration}",
            ArtifactsDirectory
        ];
}
