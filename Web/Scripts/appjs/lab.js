﻿function error_handler(e) {
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
}

function csonClick() {
    if (confirm('NOTE: This process cannot be reversed ! \n \n Is it Ok to return Patient to OPD?'))
        sendToOPDfromLab();
    else
        CustomErrorNotify("Return to OPD aborted.", "Abort !");
};


function sendToOPDfromLab() {
    var grid = $("#GridLab").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());

    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient", "Error !");
    }
    var data = {
        "attdID": selectedItem.AttdID
    };

    beginProgress();

    $.post("/Main/ReturnToOpdFromLab", data)
        .done(function (result) {
            endProgress();

            if (result[0].resp === "ok") {
                CustomSuccessNotify(result[0].mesag, "Success !");

                var agrid = $("#GridLab").data("kendoGrid");
                agrid.dataSource.read();
                //clear bio data grid
                //var abgrid = $("#GridBV").data("kendoGrid");
                //abgrid.dataSource.data([]);

            }
            else {
                CustomErrorNotify(result[0].mesag, "Error !");
            }
        }
        ).error(function (xhr, ajaxOptions, thrownError) {
            endProgress();
            ServiceError();
        }
        );
};

$(function () {
    // Declare a proxy to reference the hub.
    var notifications = $.connection.flowQueueHub;

    // Create a function that the hub can call to broadcast messages.
    notifications.client.updateLabMsgs = function () {

        var clsRegGrid = $("#GridLab").data("kendoGrid");
        clsRegGrid.dataSource.read();

        CustomQueueNotify("New Patient arrives LAB", "Out Patient Alert !");
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        //alert("connection started")
    }).fail(function (e) {
        //alert(e);
    });
});

function onChange(arg) {
    var labgrid = $("#labReqGrid").data("kendoGrid");
    labgrid.dataSource.read();
}

function onFilterChange() {
    $("#scoreList").hide();
    $("#scoreTop").hide();
}