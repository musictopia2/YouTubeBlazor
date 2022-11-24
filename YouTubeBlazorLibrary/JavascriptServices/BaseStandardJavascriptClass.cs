using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeBlazorLibrary.JavascriptServices;
internal abstract class BaseStandardJavascriptClass : IAsyncDisposable
{
    protected Lazy<Task<IJSObjectReference>> ModuleTask;
    protected abstract string JavascriptFileName { get; }
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (ModuleTask.IsValueCreated)
        {
            var module = await ModuleTask.Value;
            await module.DisposeAsync();
        }
    }
    public BaseStandardJavascriptClass(IJSRuntime js)
    {
        string jsName = JavascriptFileName;
        if (jsName.EndsWith("js") == false)
        {
            jsName = $"{jsName}.js";
        }
        Assembly? aa = Assembly.GetAssembly(GetType());
        if (aa == null)
        {
            throw new CustomBasicException("You need an assmebly for this.  Otherwise, rethink");
        }
        string firsts = aa.FullName!;
        int index = firsts.IndexOf(", ");
        string ns = firsts.Substring(0, index);
        ModuleTask = new(() => js.InvokeAsync<IJSObjectReference>(
        "import", $"./_content/{ns}/{jsName}").AsTask());
    }
}