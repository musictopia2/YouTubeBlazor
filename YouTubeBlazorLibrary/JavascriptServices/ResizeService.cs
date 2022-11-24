using Microsoft.JSInterop;
namespace YouTubeBlazorLibrary.JavascriptServices;
internal class ResizeService : BaseStandardJavascriptClass
{
    public ResizeService(IJSRuntime js) : base(js)
    {
    }
    public Func<Task>? OnResized { get; set; }
    protected override string JavascriptFileName => "resize";
    public async Task InitAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("listenForResize", DotNetObjectReference.Create(this));
    }
    [JSInvokable]
    public void RaiseOnResized()
    {
        if (OnResized is null)
        {
            return;
        }
        OnResized?.Invoke(); //hopefully this simple (?)
    }
}