namespace YouTubeBlazorLibrary.JavascriptServices;
internal static class BasicJavascriptExtensions
{
    private static Lazy<Task<IJSObjectReference>> GetModuleTask(this IJSRuntime js)
    {
        return js.GetLibraryModuleTask("mischelpers");
    }
    
    public static async Task<int> GetContainerHeight(this IJSRuntime js, ElementReference? element)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getcontainerheight", element);
    }
    public static async Task<int> GetBrowserHeight(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getbrowserheight");
    }
    public static async Task<int> GetBrowserWidth(this IJSRuntime js)
    {
        var moduleTask = js.GetModuleTask();
        return await moduleTask.InvokeDisposeAsync<int>("getbrowserwidth");
    }
}