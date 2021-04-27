var productDataTable = null;

$(function () {
    init();
});

function init() {
    productDataTable = $('#productTable').DataTable({
        scrollX: true,
        paging: true,
        ordering: true,
        searching: true,
        "columnDefs": [
            { className: "text-right", "targets": [0] },
        ],
    });
    getAllProduct();
}

function getAllProduct() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Product/GetAllProducts",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            var productList = data;
            var productArray = [];
            for (var i = 0; i < productList.length; i++) {
                productArray.push([
                    (i + 1),
                    productList[i].productName,
                    productList[i].productDescription,
                    productList[i].productPrice,
                    productList[i].productCode,
                    productList[i].productAvailableCount,
                ]);
            }
            productDataTable.clear().rows.add(productArray).draw();
            productDataTable.columns.adjust().draw();
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while retrieving");
        }
    });
}



