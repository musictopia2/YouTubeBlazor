namespace YouTubeBlazorLibrary.JavascriptServices;
public static class FullScreenHelpers
{
    public static async Task OpenFullScreen(this IJSRuntime js)
    {
        var task = js.GetLibraryModuleTask("fullscreen");
        await task.InvokeVoidDisposeAsync("openFullscreen");
    }
    public static async Task ExitFullScreen(this IJSRuntime js)
    {
        var task = js.GetLibraryModuleTask("fullscreen");
        await task.InvokeVoidDisposeAsync("exitFullscreen");
    }
}