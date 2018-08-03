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

    function btnClick() {
        var data = {};
        var grid = $("#ptsGrid").data("kendoGrid");
        var selectedItem = grid.dataItem(grid.select());
        if (selectedItem === null) {
            CustomErrorNotify("Sorry! You have not selected a Patient");
        }
        data = { "selectedPtIds": selectedItem.ID };

        beginProgress();

        $.post("/Main/SendToOPD", data)
            .done(function (result) {
                endProgress();

                if (result[0].resp === "ok") {
                    CustomSuccessNotify(result[0].mesag, "Success !");
                    
                    var agrid = $("#GridOnVisit").data("kendoGrid");
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
    };