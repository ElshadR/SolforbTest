(function ($) {
    "use strict";

    $(document).on('click', '#add-order-item', function () {
        let index = parseInt($("#order-items").data("activeindex"))
        index++
        $("#order-items").data("activeindex", index)
        debugger
        $.ajax({
            url: "/order/AddOrderItemPartial",
            method: "post",
            data: { index: index }
        })
            .done(function (data) {
                debugger
                $("#order-items").append(data)
            })
            .fail(function (msg) {
                console.log(msg);
            })
    })
    $(document).on('click', '#delete-order-item', function () {
        $(this).parent().parent().parent().remove();
        debugger

        var orderItems = $(".order-item");

        for (var i = 0; i < orderItems.length; i++) {
            var unique = $(orderItems[i]).data("unique")
            $(`#order-item-id-${unique}`).attr("name", `OrderItems[${i}].Id`)
            $(`#order-item-name-${unique}`).attr("name", `OrderItems[${i}].Name`)
            $(`#order-item-quantity-${unique}`).attr("name", `OrderItems[${i}].Quantity`)
            $(`#order-item-unit-${unique}`).attr("name", `OrderItems[${i}].Unit`)
        }
        $("#order-items").data("activeindex", orderItems.length - 1)
    })
    $(document).on('click', '#search', function () {

        debugger
        var startdate = $("#search-startdate").val();
        var enddate = $("#search-enddate").val();
        var number = $("#search-number").val();
        var providerid = $("#search-providerid").val();
        var providername = $("#search-providername").val();
        var orderitemname = $("#search-orderitemname").val();
        var orderitemunit = $("#search-orderitemunit").val();

        $.ajax({
            url: "/order/Search",
            method: "post",
            data: {
                StartDate: startdate,
                EndDate: enddate,
                Number: number,
                ProviderId: providerid,
                ProviderName: providername,
                OrderItemName: orderitemname,
                OrderItemUnit: orderitemunit,
            }
        })
            .done(function (data) {
                debugger
                $("#table-data").html(data)
            })
            .fail(function (msg) {
                console.log(msg);
            })
    })
})(jQuery);

//# sourceMappingURL=main.js.map