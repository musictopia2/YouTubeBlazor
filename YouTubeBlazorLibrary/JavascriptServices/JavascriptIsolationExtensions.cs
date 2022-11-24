namespace YouTubeBlazorLibrary.JavascriptServices;
internal static class JavascriptIsolationExtensions
{
    public static async Task InvokeVoidDisposeAsync(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[] args)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(identifier, args);
        await module.DisposeAsync();
    }
    public static async Task<T> InvokeDisposeAsync<T>(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[] args)
    {
        var module = await moduleTask.Value;
        var output = await module.InvokeAsync<T>(identifier, args);
        await module.DisposeAsync();
        return output;
    }
    public static async Task InvokeVoidFromClassAsync(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[] args)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(identifier, args);
    }
    public static async Task<T> InvokeFromClassAsync<T>(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[] args)
    {
        var module = await moduleTask.Value;
        var output = await module.InvokeAsync<T>(identifier, args);
        return output;
    }
    internal static Lazy<Task<IJSObjectReference>> GetLibraryModuleTask(this IJSRuntime js, string javascriptfile)
    {
        return js.GetModuleTask($"./_content/YouTubeBlazorLibrary/{GetJsName(javascriptfile)}");
    }
    private static Lazy<Task<IJSObjectReference>> GetModuleTask(this IJSRuntime js, string fullPath)
    {
        Lazy<Task<IJSObjectReference>> output = new(() => js.InvokeAsync<IJSObjectReference>(
         "import", fullPath).AsTask());
        return output;
    }
    private static string GetJsName(string name)
    {
        string jsName = name;
        if (jsName.EndsWith("js") == false)
        {
            jsName = $"{jsName}.js";
        }
        return jsName;
    }
}
