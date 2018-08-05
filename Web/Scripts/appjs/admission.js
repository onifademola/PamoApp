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
}

function csonClick() {
    if (confirm('NOTE: This process cannot be reversed ! \n \n Is it Ok to discharge and send Patient to Pharmacy?'))
        sendToPharmacy();
    else
        CustomErrorNotify("Discharge & send to Pharmacy aborted.", "Abort !");
};


function sendToPharmacy() {
    //console.log(consulRmID);
    var grid = $("#GridAdmsn").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    //console.log(selectedItem);
    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient", "Error !");
    }
    var data = {
        "attdID": selectedItem.AttdID
    };

    beginProgress();

    $.post("/Main/DischargeAndSendToPharmacy", data)
        .done(function (result) {
            endProgress();

            if (result[0].resp === "ok") {
                CustomSuccessNotify(result[0].mesag, "Success !");

                var agrid = $("#GridAdmsn").data("kendoGrid");
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

function admClick() {
    var grid = $("#GridAdmsn").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    var wdID = $("#admDrpdwn").data("kendoDropDownList").value();
    var remark = $("#txtbox").val();
    //console.log(selectedItem);
    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient", "Error !");
    }
    var data = {
        "attdID": selectedItem.AttdID,
        "wardID": wdID,
        "rmk": remark
    };

    beginProgress();

    $.post("/Main/AdmitPatient", data)
        .done(function (result) {
            endProgress();

            if (result[0].resp === "ok") {
                CustomSuccessNotify(result[0].mesag, "Success !");
                $("#admDrpdwn").val("").data("kendoDropDownList").text("");
                $("#txtbox").val("");
                var agrid = $("#GridADM").data("kendoGrid");
                agrid.dataSource.read();

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
    notifications.client.updateAdmsMsgs = function () {

        var clsRegGrid = $("#GridAdmsn").data("kendoGrid");
        clsRegGrid.dataSource.read();

        CustomQueueNotify("New Patient have been placed on Admission", "Out Patient Alert !");
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        //alert("connection started")
    }).fail(function (e) {
        //alert(e);
    });
});

function onChange(arg) {
    //kendoConsole.log("The selected product ids are: [" + this.selectedKeyNames().join(", ") + "]");
    var grid = $("#GridADM").data("kendoGrid");
    grid.dataSource.read();
}
