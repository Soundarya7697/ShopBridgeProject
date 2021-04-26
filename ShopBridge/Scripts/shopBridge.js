function init() {
    clearForm();
    getAllProduct();
}
function clearForm() {
    $("#productForm")[0].reset();
}
function addProduct() {
    var data = $("#productForm").serialize();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Product/AddProduct",
        contenttype: 'application/json; charset=utf-8',
        data: data,
        success: function (data) {
            console.log("Success");
            alert("Saved Successfully");
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while saving");
        }
    });
}

function modifyProduct() {
    var data = $("#productForm").serialize();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Product/Modify",
        contenttype: 'application/json; charset=utf-8',
        data: data,
        success: function (data) {
            console.log("Success");
            alert("Saved Successfully");
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while saving");
        }
    });
}

function deleteProduct(productId) {
    var data = 'id=' + productId;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Product/Delete",
        contenttype: 'application/json; charset=utf-8',
        data: data,
        success: function (data) {
            console.log("Success");
            alert("Saved Successfully");
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while saving");
        }
    });
}
function getAllProduct() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Product/GetAllProducts",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            var productList = data;

            var htmlText = "";
            for (var i = 0; i < productList.length; i++) {
                htmlText += "<li class='list-group-item'>" + productList[i].productname +
                    "<span class='float-right text-danger mr-2' onclick='deleteProduct(\"" + productList[i].productid + "\")'>Delete</span>" +
                    "<span class='float-right text-success mr-2' onclick='getProduct(\"" + productList[i].productid + "\")'>Edit</span>" +
                    "</li>";
            }
            $("#productList").html(htmlText);
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while saving");
        }
    });
}
function getProduct(productId) {
    var data = 'id=' + productId;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Product/GetAll",
        contenttype: 'application/json; charset=utf-8',
        data: data,
        success: function (data) {
            console.log("Success");
            alert("Saved Successfully");
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while saving");
        }
    });
}
