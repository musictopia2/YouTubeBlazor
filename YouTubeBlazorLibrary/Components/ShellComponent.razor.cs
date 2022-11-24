namespace YouTubeBlazorLibrary.Components;
public partial class ShellComponent
{
    [Parameter]
    public bool Visible { get; set; } = true;
    private string GetClass
    {
        get
        {
            if (Visible == false)
            {
                return "hide";
            }
            if (UseGrid)
            {
                return "gridcontainer";
            }
            return "";
        }
    }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool UseGrid { get; set; }
}