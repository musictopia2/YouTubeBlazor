var tag = document.createElement('script');
var player;
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
function onYouTubeIframeAPIReady() {

}
function privatetime() {
    time = player.getCurrentTime();
    return time;
}
function privateduration() {
    duration = player.getDuration();
    return duration;
}
function privateseek(seekto, allowseekahead) {
    player.seekTo(seekto, allowseekahead)
}
function privateinit(start) {
    dotnetRef = start; //this is used so parts later can do what is needed.
}
function privateplay() {
    player.playVideo();
}
function privatepause() {
    player.pauseVideo();
}
function privatesetsize(width, height) {
    player.setSize(width, height);
}
function privatemute() {
    player.mute();
}
function privateunmute() {
    player.unMute();
}
function privategetvolume() {
    volume = player.getVolume();
    return volume;
}
function privatesetvolume(value) {
    player.setVolume(value);
}
function privategetplaybackrate() {
    value = player.getPlaybackRate();
    return value;
}
function privatesetplaybackrate(value) {
    player.setPlaybackRate(value);
}