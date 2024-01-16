namespace YouTubeBlazorLibrary.JavascriptServices;
internal class YouTubeService : BaseStandardJavascriptClass
{
    public YouTubeService(IJSRuntime js) : base(js)
    {
    }
    protected override string JavascriptFileName => "youtubemain";
    public Action? VideoEnd { get; set; }
    public Func<ProgressModel, Task>? ProgressAction { get; set; }
    private BasicList<SkipSceneClass> _skips = new(); //has to be sorted already.  decided to go ahead and require the basiclist (custom afterall).  since i have specialized methods.
    Timer? _time;
    private bool _didInit;
    public async Task LoadVideoAsync(string videoId, int startat, int endat, BasicList<SkipSceneClass> skips, int height = 390, int width = 640)
    {
        if (_didInit == false)
        {
            await ModuleTask.InvokeVoidFromClassAsync("init", DotNetObjectReference.Create(this));
            _didInit = true;
        }
        _skips = skips;
        _skips.KeepConditionalItems(x => x.StartTime > startat);
        await ModuleTask.InvokeVoidFromClassAsync("loadVideo", videoId, startat, endat, height, width);
        if (_time is not null)
        {
            _time.Stop();
        }
        else
        {
            _time = new()
            {
                Interval = 500,
                AutoReset = false
            };
        }
        _time.Elapsed += Time_Elapsed;
        _time.Start();
    }
    public async Task PlayPauseAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("playPause");
    }
    private async void Time_Elapsed(object? sender, ElapsedEventArgs e)
    {
        _time!.Stop();
        int upto = await UpToAsync();
        int duration = await DurationAsync();
        if (ProgressAction is not null)
        {
            await ProgressAction.Invoke(new ProgressModel(upto, duration));
        }
        if (_skips.Count > 0)
        {
            if (upto > _skips.First().StartTime)
            {
                var item = _skips.First();
                await SeekAsync(item.EndTime, true);
                _skips.RemoveFirstItem();
            }
        }
        _time.Start();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="upto"></param>
    /// <param name="allowseekahead">this is whether a new request is made if pasted the buffered area.  its suggested to set to false if the user is doing mouse moves.</param>
    /// <returns></returns>
    public async Task SeekAsync(int upto, bool allowseekahead)
    {
        await ModuleTask.InvokeVoidFromClassAsync("seek", upto, allowseekahead);
    }
    public async Task<int> DurationAsync()
    {
        float x = await ModuleTask.InvokeFromClassAsync<float>("duration");
        return (int)Math.Floor(x);
    }
    public async Task<int> UpToAsync()
    {
        float x = await ModuleTask.InvokeFromClassAsync<float>("upto");
        return (int)Math.Floor(x);
    }
    [JSInvokable]
    public void ShowVideoEnded()
    {
        VideoEnd?.Invoke();
    }
    public async Task ResizePlayerAsync(int width, int height)
    {
        await ModuleTask.InvokeVoidFromClassAsync("setsize", width, height);
    }
    public async Task MuteAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("mute");
    }
    public async Task UnmuteAsync()
    {
        await ModuleTask.InvokeVoidFromClassAsync("unmute");
    }
    public async Task<int> GetVolumeAsync()
    {
        return await ModuleTask.InvokeFromClassAsync<int>("getvolume");
    }
    public async Task SetVolumeAsync(int value)
    {
        await ModuleTask.InvokeVoidFromClassAsync("setvolume", value);
    }
    public async Task<float> GetPlaybackRateAsync()
    {
        return await ModuleTask.InvokeFromClassAsync<float>("getPlaybackRate");
    }
    public async Task SetPlaybackRateAsync(float value)
    {
        await ModuleTask.InvokeVoidDisposeAsync("setPlaybackRate", value);
    }
}