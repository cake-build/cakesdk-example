public static partial class Program
{
    static void Main_PreClean()
    {
        Task("PreClean")
            .IsDependeeOf("Clean")
            .WithCriteria<BuildData>(c => c.Rebuild, nameof(BuildData.Rebuild))
            .Does(() => Information("Preparing for clean operation"));
    }
}
