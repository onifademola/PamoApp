
$("#btnSubmitFDPE").click(function () {
    urlAction = '/backendrepair/FixDoctorPhyExamTB';
    $.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPR").click(function () {
    urlAction = '/backendrepair/FixPatientRoundsTB';
$.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPPr").click(function () {
    urlAction = '/backendrepair/FixPatientProcedurTB';
$.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPP").click(function () {
    urlAction = '/backendrepair/FixPatientPhyExamTB';
$.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPT").click(function () {
    urlAction = '/backendrepair/FixDoctorProcedurTB';
$.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPA").click(function () {
    urlAction = '/backendrepair/FixPatientAttendanceTB';
    $.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitGSU").click(function () {
    urlAction = '/backendrepair/GenerateUserUniqueId';
    $.ajax({
    cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction,
        success: function (result) {
    console.log(result);
    endProgress();
            if (result[0].status == "OK") {
    $('#smsAlert').addClass("alert alert-success");
    $('#smsNotifyHeader').html('Success !!');
                $('#smsNotifyBody').html("Operation successful");
                $('#smsNotify').show();
                $("#btnSubmit").attr('disabled', 'disabled');
            }
            else {
    CustomErrorNotify("See error message on page.", result[0].status + " !!");
    $('#smsAlert').addClass("alert alert-danger");
                $('#smsNotifyHeader').html(result[0].status + " !!");
                $('#smsNotifyBody').html("Error code: " + result[0].error + " - Reason: " + result[0].errorcode);
                $('#smsNotifyMessage').html("Please, verify that you have a valid subscription, otherwise contact your Administrator.");
                $('#smsNotify').show();
                $("#btnSubmit").attr('disabled', 'disabled');
            }
        }
    });
});

$("#btnSubmitFDRT").click(function () {
    urlAction = '/backendrepair/FixDoctorRoundsTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPPC").click(function () {
    urlAction = '/backendrepair/FixPatientPComplainTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPB").click(function () {
    urlAction = '/backendrepair/FixPatientBiovitalsTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPDT").click(function () {
    urlAction = '/backendrepair/FixPatientDeliveryTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPDTB").click(function () {
    urlAction = '/backendrepair/FixPatientDiagnosisTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPIT").click(function () {
    urlAction = '/backendrepair/FixPatientInvestigationTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPNN").click(function () {
    urlAction = '/backendrepair/FixPatientNursenoteTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPSR").click(function () {
    urlAction = '/backendrepair/FixPatientScanReportTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPST").click(function () {
    urlAction = '/backendrepair/FixPatientSurgeryTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPTR").click(function () {
    urlAction = '/backendrepair/FixPatientTblRecTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPWT").click(function () {
    urlAction = '/backendrepair/FixPatientWardTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFPAT").click(function () {
    urlAction = '/backendrepair/FixPatientAppointmentTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFDAT").click(function () {
    urlAction = '/backendrepair/FixDoctorAppointmentTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFNNNT").click(function () {
    urlAction = '/backendrepair/FixNurseNursenoteTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFCUT").click(function () {
    urlAction = '/backendrepair/FixComplainUserTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFACR").click(function () {
    urlAction = '/backendrepair/FixAttendanceConsultingRoomTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFDDB").click(function () {
    urlAction = '/backendrepair/FixDiagnosisDiagnosedBy';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});

$("#btnSubmitFDATT").click(function () {
    urlAction = '/backendrepair/FixDoctorAttendanceTB';
    $.ajax({
        cache: false,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: urlAction
    });
});