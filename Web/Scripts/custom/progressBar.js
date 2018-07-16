$(function () {
    //hide progress bar on load    
    $("#progBar").hide();    
});

function beginProgress() {
    //hide the grid
    $("#pageDP").hide();
    //begin to show the progress bar
    $("#progBar").show();
    //call the progressbar function animation
    progress();
};

function endProgress() {
    //remove the progress bar
    $("#progBar").hide();
    //and show the grid
    $("#pageDP").show();
};


function progress() {
    var pb = $("#progressBar").data("kendoProgressBar");
    pb.value(0);

    var interval = setInterval(function () {
        if (pb.value() < 10) {
            pb.value(pb.value() + 1);
        } else {
            clearInterval(interval);
        }
    }, 1000);
};