
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

function csonClick() {
    if (confirm('NOTE: This process cannot be reversed ! \n \n Is it Ok to send Patient to Accounts?'))
        sendToAccounts();
    else
        CustomErrorNotify("Send to Accounts aborted.", "Abort !");
};


function sendToAccounts() {
    //console.log(consulRmID);
    var grid = $("#GridPharm").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    //console.log(selectedItem);
    if (selectedItem === null) {
        return CustomErrorNotify("Sorry! You have not selected a Patient", "Error !");
    }
    var data = {
    "attdID": selectedItem.AttdID
    };

    beginProgress();

    $.post("/Main/SendToAccounts", data)
        .done(function (result) {
    endProgress();

    if (result[0].resp === "ok") {
        CustomSuccessNotify(result[0].mesag, "Success !");

        var agrid = $("#GridPharm").data("kendoGrid");
                        agrid.dataSource.read();
                        //clear bio data grid
                        //var abgrid = $("#GridBV").data("kendoGrid");
                        //abgrid.dataSource.data([]);

                    }
    else {
        CustomErrorNotify(result[0].mesag, "Error !");
    }
    }).error(function (xhr, ajaxOptions, thrownError) {
        endProgress();
        ServiceError();
    });
};

$(function () {
    // Declare a proxy to reference the hub.
    var notifications = $.connection.flowQueueHub;

    // Create a function that the hub can call to broadcast messages.
    notifications.client.updatePharmMsgs = function () {

        var clsRegGrid = $("#GridPharm").data("kendoGrid");
        clsRegGrid.dataSource.read();

        CustomQueueNotify("New Patient arrives Pharmacy", "Out Patient Alert !");
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
    var grid = $("#GridBV").data("kendoGrid");
    grid.dataSource.read();
    $("#scoreList").show();
    $("#scoreTop").show();
}

function onFilterChange() {
    $("#scoreList").hide();
    $("#scoreTop").hide();
}