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


function DataSaveSuccess() {    
    toastr.success("Your change(s) have been committed to the server.", "Successful Save", opts);
};

function DataSaveError() {   
    toastr.error("Sorry, we could not get your recent changes to the server right now. Please try again.", "Failed Save", opts);
};

function ServiceError() {
    toastr.error("Sorry, something went wrong... We could not complete your requested task at the moment, please try again!", "Service Unreachable", opts);    
};

function TeacherClassNotFoundError() {
    toastr.warning("Sorry, either you do not teach any Subject for the class selected or the server could not be reached, please verify your selection and try again!", "Subject Not Found", opts);
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
