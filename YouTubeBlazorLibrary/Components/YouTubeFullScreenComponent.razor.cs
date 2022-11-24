namespace YouTubeBlazorLibrary.Components;
public partial class YouTubeFullScreenComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    private ResizeService? _resizeService;
    private ElementReference? _bottomElement;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _resizeService = new(JS!);
        _resizeService.OnResized = DoResizeAsync;
    }
    private async Task DoResizeAsync()
    {
        int bottomheight = await JS!.GetContainerHeight(_bottomElement!);
        int parentheight = await JS!.GetBrowserHeight();
        int parentwidth = await JS!.GetBrowserWidth();
        int realHeight = parentheight - bottomheight - 5; //should have some margins
        await ResizePlayerAsync(parentwidth, realHeight);
        await InvokeAsync(StateHasChanged);
    }
    protected override async Task InitOtherAsync()
    {
        await _resizeService!.InitAsync();
        mm1.ProcessFullScreen?.Invoke(); //anybody can do what they want depending on platform for full screen.
    }
}