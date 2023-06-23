
function requestMedia() {

    // Create request options
    let options = {
        audio: false,
        video: true
    };


    // Warning: Below commented code causes "Illegal invocation error"
    //// Set up proper function
    //navigator.mediaDevices.getUserMedia =
    //    navigator.getUserMedia ||
    //    navigator.webkitGetUserMedia ||
    //    navigator.mozGetUserMedia ||
    //    navigator.msGetUserMedia;

    try {
        // Request user media
        navigator.mediaDevices
            .getUserMedia(options)
            .then(gotLocalStream)
            .catch(logError);
    }
    catch (e) {
        logError(e);
    }

}

// Receives local stream data 
function gotLocalStream(stream) {

    // Get 'video' html control
    var videoLocalPanel = document.getElementById("LOCAL_VIDEO_ID");

    if ("srcObject" in videoLocalPanel) {
        videoLocalPanel.srcObject = stream;
    } else {
        videoLocalPanel.src = window.URL.createObjectURL(stream);
    }

}

function logError(e) {

    // Get 'label' html control
    var label = document.getElementById("LABEL_ID");

    label.value = e;

}