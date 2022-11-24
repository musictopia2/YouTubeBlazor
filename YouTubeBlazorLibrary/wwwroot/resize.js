var throttleResizeHandlerId = -1;
var dotnet;
export function listenForResize(dotnetRef) {
    dotnet = dotnetRef;
    window.addEventListener("resize", throttleResizeHandler);
    resizeHandler();
}
function throttleResizeHandler() {
    clearTimeout(throttleResizeHandlerId);
    throttleResizeHandlerId = window.setTimeout(resizeHandler, 300);
}
function cancelListener() {
    window.removeEventListener("resize", throttleResizeHandler);
}
function resizeHandler() {
    dotnet.invokeMethodAsync('RaiseOnResized');
}