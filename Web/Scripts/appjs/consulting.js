function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
};

$(function () {
    // Declare a proxy to reference the hub.
    var notifications = $.connection.flowQueueHub;

    // Create a function that the hub can call to broadcast messages.
    notifications.client.updateConsultingMsgs = function (data) {

        //check that update will be sent to the only doctor in the selected consulting room
        var consultRmID = $("#consrDrpdwn").data("kendoDropDownList").value();
        if (consultRmID > 0 & consultRmID === data) {
            var grid = $("#consPtsGrid").data("kendoGrid");
            grid.dataSource.read();
            CustomQueueNotify("New Patient is queued at your consulting room", "Out Patient Alert !");
        }
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        //alert("connection started")
        //getAllMessages();
    }).fail(function (e) {
        //alert(e);
    });
});

var consultRmID = $("#consrDrpdwn").data("kendoDropDownList").value();
if (consultRmID < 0) {
    CustomDocConsWarningNotify("Sorry! You have to choose your Consulting Room before you can attend to Patients", "Warning !");  
};

//call the save function here so users dont have to save manually
function tinyMceChange(ed) {
    //console.log('Editor contents was modified. Contents: ' + ed.getContent());
    var data = {};
    var cntnt = ed.getContent();
    var grid = $("#consPtsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    
    data = { "attdID": selectedItem.AttdID, "note": cntnt };
    //console.log(data);
    $.ajax({
        cache: false,
        type: "POST",
        //contentType: "application/json; charset=utf-8",
        //dataType: 'json',
        url: "/Consultation/UpdateDocReportForAttendance",
        data: data,
        success: function (result) {
            if (result[0].resp === "ok") {
                CustomSuccessNotify(result[0].mesag, "Success !");
            }
            else {
                //CustomErrorNotify(result[0].mesag, "Error !");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //do nothing
        }
    });
};

//called when doctor select a new patient in consultation queue
function onChange(arg) {
    //when called, get doctor's report of selected patient if exists, or create new
    var data = {};
    var grid = $("#consPtsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    if (selectedItem !== null) {
        data = { "attdId": selectedItem.AttdID };
        $.ajax({
            cache: false,
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            //dataType: 'json',
            url: "/Consultation/GetDocReportForAttendance",
            data: data,
            success: function (result) {
                if (result.length > 2) {
                    tinyMCE.get('texteditor').setContent(result);
                }
                else {
                    $.get("/Scripts/tinymce/tinytemplates/docreport1.html", function (d) {
                        tinyMCE.get('texteditor').setContent(d);
                    });
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
               //do nothing
            }
        });
    };

    //when called, get the bioviatl of selected patient
    var bvgrid = $("#GridBV").data("kendoGrid");
    bvgrid.dataSource.read();
    //$("#biov").show();

    //when called, get the drug prescription of selected patient
    var prgrid = $("#PrescGrid").data("kendoGrid");
    prgrid.dataSource.read();
};

function drpdwnOpen() {
    //$("#biov").hide();

    //clear grid
    var grid = $("#consPtsGrid").data("kendoGrid");
    grid.dataSource.data([]);

    //reset doc report
    tinyMCE.get('texteditor').setContent("Data not yet set");
};

function drpdwnClose() {
    $("#biov").show();
};

function drpdwnChange() {
    var consultnnRmID = $("#consrDrpdwn").data("kendoDropDownList").value();
    var grid = $("#consPtsGrid").data("kendoGrid");
    if (consultnnRmID > 0) {
        grid.dataSource.read();       
    } else {
        grid.dataSource.data([]);
    }
};

function btnPharmClick() {
    var data = {};
    var grid = $("#consPtsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient");
    }

    data = { "attdID": selectedItem.AttdID };

    if (confirm('NOTE: This process cannot be reversed ! \n \n Is it Ok to send Patient to Pharmacy?')) {
        beginProgress();

        $.post("/Main/SendToPharmacy", data)
            .done(function (result) {
                endProgress();

                if (result[0].resp === "ok") {
                    CustomSuccessNotify(result[0].mesag, "Success !");
                    var agrid = $("#consPtsGrid").data("kendoGrid");
                    agrid.dataSource.read();
                }
                else {
                    CustomErrorNotify(result[0].mesag, "Error !");
                }
            }
            ).error(function (xhr, ajaxOptions, thrownError) {
                endProgress();
                ServiceError();
            });
    } else {
        CustomErrorNotify("Send to Pharmacy aborted.", "Abort !");
    }
};

function btnLabClick() {
    var data = {};
    var grid = $("#consPtsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient");
    }

    data = { "attdID": selectedItem.AttdID };

    if (confirm('NOTE: This process cannot be reversed ! \n \n Is it Ok to send Patient to Lab?')) {
        beginProgress();

        $.post("/Main/SendToLab", data)
            .done(function (result) {
                endProgress();

                if (result[0].resp === "ok") {
                    CustomSuccessNotify(result[0].mesag, "Success !");
                    var agrid = $("#consPtsGrid").data("kendoGrid");
                    agrid.dataSource.read();
                }
                else {
                    CustomErrorNotify(result[0].mesag, "Error !");
                }
            }
            ).error(function (xhr, ajaxOptions, thrownError) {
                endProgress();
                ServiceError();
            });
    } else {
        CustomErrorNotify("Send to Lab aborted.", "Abort !");
    }
};

function btnAdmitpClick() {
    var data = {};
    var grid = $("#consPtsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient");
    }

    data = { "attdID": selectedItem.AttdID };

    if (confirm('NOTE: This process cannot be reversed ! \n \n Is it Ok to send Patient to Admission?')) {
        beginProgress();

        $.post("/Main/SendToAdmission", data)
            .done(function (result) {
                endProgress();

                if (result[0].resp === "ok") {
                    CustomSuccessNotify(result[0].mesag, "Success !");
                    var agrid = $("#consPtsGrid").data("kendoGrid");
                    agrid.dataSource.read();
                }
                else {
                    CustomErrorNotify(result[0].mesag, "Error !");
                }
            }
            ).error(function (xhr, ajaxOptions, thrownError) {
                endProgress();
                ServiceError();
            });
    } else {
        CustomErrorNotify("Send to Admission aborted.", "Abort !");
    }
};