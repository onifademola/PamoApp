

function RegServiceAlert(msg, infoTmp) {
    var popupNotification = $("#popupNotification").data("kendoNotification");
    popupNotification.show({
        title: "Registration Alert...",
        message: msg
    }, infoTmp);
};

function RegProgressAlert() {
    var popupNotification = $("#progressNotification").data("kendoNotification");
    popupNotification.show({
        title: "Registration In Progress...",
        message: "Your registration will complete in a moment!"
    }, "upload-success");
};

//function RegServiceFailure(msg, infoTmp) {
//    var popupNotification = $("#popupNotification").data("kendoNotification");
//    popupNotification.show({
//        title: "Registration Invalid...",
//        message: msg
//    }, "error");
//};
