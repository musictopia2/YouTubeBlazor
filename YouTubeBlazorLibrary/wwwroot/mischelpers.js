export function getcontainerheight(element) {
    if (element == null) {
        return 0;
    }
    return element.clientHeight;
}
export function getbrowserheight() {
    return window.innerHeight;
}
export function getbrowserwidth() {
    return window.innerWidth;
}