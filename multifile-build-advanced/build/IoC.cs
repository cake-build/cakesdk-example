public static partial class Program
{
    static partial void RegisterServices(IServiceCollection services)
    {
        // Register MyService
        services.AddSingleton<IMyService, MyService>();
        
        // Injects IOC-Task as an dependency of Build task
        services.AddSingleton(new Action<IScriptHost>(
                        host => host.Task("IOC-Task")
                                    .IsDependeeOf("Build")
                                    .Does(() => Information("Hello from IOC-Task"))));
    }
}
