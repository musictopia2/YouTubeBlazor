namespace YouTubeBlazorLibrary.Components;
public abstract class YouTubeComponentBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public string VideoID { get; set; } = "";
    [Parameter]
    public BasicList<SkipSceneClass> SkipList { get; set; } = new();
    [Parameter]
    public int StartAt { get; set; }
    [Parameter]
    public int EndAt { get; set; }
    [Parameter]
    public bool Visible { get; set; } = true;
    [Parameter]
    public bool StartMute { get; set; } = false; //sometimes you want to always be mute.
    [Parameter]
    public EventCallback VideoEnded { get; set; }

    [Parameter]
    public EventCallback<ProgressModel> ProgressAction { get; set; }
    [Parameter]
    public bool Start { get; set; }
    [Inject]
    protected IJSRuntime? JS { get; set; }
    private YouTubeService? _youtubeService { get; set; }
    protected override void OnInitialized()
    {
        _youtubeService = new(JS!);
        _youtubeService.VideoEnd = () =>
        {
            if (VideoEnded.HasDelegate)
            {
                VideoEnded.InvokeAsync();
            }
        };
        _youtubeService.ProgressAction = async x =>
        {
            await InvokeAsync(async () =>
            {
                if (ProgressAction.HasDelegate)
                {
                    await ProgressAction.InvokeAsync(x);
                }
            });
        };
        //this means overrided versions simply do the initial plus extras
        base.OnInitialized();
    }
    protected virtual Task InitOtherAsync() => Task.CompletedTask;
    protected virtual int InitialHeight => 390;
    protected virtual int InitialWidth => 640;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _youtubeService!.LoadVideoAsync(VideoID, StartAt, EndAt, SkipList, InitialHeight, InitialWidth); //doing pause is not helping to allow to load automatically.
            if (StartMute)
            {
                await _youtubeService.MuteAsync();
            }
            await InitOtherAsync();
        }
    }
    public async Task PlayPauseAsync()
    {
        await _youtubeService!.PlayPauseAsync();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="howMuch">set positive to fast forward or negative to rewind.  time in seconds.</param>
    /// <returns></returns>
    public async Task SeekIntoAsync(int howMuch, bool allowSeek)
    {
        int upto = await _youtubeService!.UpToAsync();
        upto += howMuch;
        await _youtubeService.SeekAsync(upto, allowSeek);
    }
    public async Task ResizePlayerAsync(int width, int height)
    {
        await _youtubeService!.ResizePlayerAsync(width, height);
    }
    public async Task MuteAsync()
    {
        await _youtubeService!.MuteAsync();
    }
    public async Task UnmuteAsync()
    {
        await _youtubeService!.UnmuteAsync();
    }
    public async Task<int> GetVolumeAsync()
    {
        return await _youtubeService!.GetVolumeAsync();
    }
    public async Task SetVolumeAsync(int value)
    {
        await _youtubeService!.SetVolumeAsync(value);
    }
    public async Task<float> GetPlaybackRateAsync()
    {
        return await _youtubeService!.GetPlaybackRateAsync();
    }
    /// <summary>
    /// will attempt to set the playback rate.  some videos don't even support this.
    /// </summary>
    /// <param name="suggestValue">supported values are 0.25, 0.5, 1, 1.5, 2</param>
    /// <returns></returns>
    public async Task SetPlaybackRateAsync(float suggestValue)
    {
        await _youtubeService!.SetPlaybackRateAsync(suggestValue);
    }
}