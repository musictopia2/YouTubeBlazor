var dotnetRef;
var started;
var paused;
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.ENDED) {
        dotnetRef.invokeMethodAsync('ShowVideoEnded');
        started = false;
    }
    if (event.data == YT.PlayerState.PAUSED) {
        paused = true;
    }
    if (event.data == YT.PlayerState.PLAYING) {
        paused = false;
    }
}
//function stopVideo() {
//    player.stopVideo();
//}
function onPlayerReady(event) {
    event.target.playVideo();
}
export function loadVideo(videoid, startat, endat, height, width) {
    if (player) {
        player.destroy();
    }
    if (endat == 0) {
        player = new YT.Player('player', {
            height: height,
            width: width,
            videoId: videoid,
            playerVars: {
                'start': startat,
                'enablejsapi': 1,
                'autoplay': 1,
                'controls': 0,
                'disablekb': 1
            },
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
    }
    else {
        player = new YT.Player('player', {
            height: height,
            width: width,
            videoId: videoid,
            playerVars: {
                'start': startat,
                'end': endat,
                'autoplay': 1,
                'enablejsapi': 1,
                'controls': 0,
                'disablekb': 1
            },
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
        started = true;
    }
}
export function duration() {
    try {
        return privateduration();
    }
    catch (err) {
        console.log(err);
        return -1;
    }
}

export function upto() {

    try {
        time = privatetime();
        return time;
    }
    catch (err) {
        console.log(err);
        return -1;
    }
}
export function seek(seekto, allowseek) {
    privateseek(seekto, allowseek);
}
export function init(start) {
    dotnetRef = start;
}
export function playPause() {
    if (started == false) {
        return;
    }
    if (paused) {
        privateplay();
        return;
    }
    privatepause();
    //not sure if the ui needs to know what to display.
    //if so, then could need a javascript function (?)
}
export function setsize(width, height) {
    privatesetsize(width, height);
}
export function mute() {
    privatemute();
}
export function unmute() {
    privateunmute();
}
export function getVolume() {
    return privategetvolume();
}
export function setVolume(value) {
    return privatesetvolume(value);
}
export function getPlaybackRate() {
    return privategetplaybackrate();
}
export function setPlaybackRate(value) {
    return privatesetplaybackrate(value);
}