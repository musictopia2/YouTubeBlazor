namespace YouTubeBlazorLibrary.Components;
public partial class YouTubeFlexibleScreenComponent
{
    [Parameter]
    [EditorRequired]
    public int VideoHeight { get; set; }
    [Parameter]
    [EditorRequired]
    public int VideoWidth { get; set; }
    protected override int InitialHeight => VideoHeight;
    protected override int InitialWidth => VideoWidth;
}