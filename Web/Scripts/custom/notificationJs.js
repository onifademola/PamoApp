var opts = {
    "closeButton": true,
    "debug": false,
    "positionClass": "toast-top-full-width",
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var qopts = {
    "closeButton": true,
    "debug": false,
    "positionClass": "toast-bottom-right",
    "onclick": null,
    "showDuration": "0",
    "hideDuration": "1000",
    "timeOut": "0",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};


function DataSaveSuccess() {    
    toastr.success("Your change(s) have been committed to the server.", "Successful Save !", opts);
};

function DataSaveError() {   
    toastr.error("Sorry, we could not get your recent changes to the server right now. Please try again.", "Failed Save !", opts);
};

function ServiceError() {
    toastr.error("Sorry, something went wrong... We could not complete your requested task at the moment, please try again!", "Internal Error !", opts);    
};


// CUSTOM NOTIFICATIONS //
function CustomSuccessNotify(message, title) {
    toastr.success(message, title, opts);
};

function CustomInfoNotify(message, title) {
    toastr.info(message, title, opts);
};

function CustomWarningNotify(message, title) {
    toastr.warning(message, title, opts);
};

function CustomErrorNotify(message, title) {
    toastr.error(message, title, opts);    
};

function CustomQueueNotify(message, title) {
    toastr.info(message, title, qopts);
};