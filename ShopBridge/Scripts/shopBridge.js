var productDataTable = null;

$(function () {
    init();
});

function init() {
    productDataTable = $('#productTable').DataTable({
        scrollY: '400px',
        paging: false,
        ordering: true,
        searching: true,
    });
    clearForm();
    getAllProduct();
}


function clearForm() {
    $("#productForm")[0].reset();
    $("#addButton").show();
    $("#modifyButton").hide();
}

function addProduct() {
    if (validateForm()){
        var data = $("#productForm").serialize();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Product/AddProduct",
            contenttype: 'application/json; charset=utf-8',
            data: data,
            success: function (data) {
                console.log("Success");
                clearForm();
                getAllProduct();
                alert("Saved Successfully");
            },
            error: function (data) {
                console.log("Failure");
                alert("Error while saving");
            }
        });
    }
}

function validateForm() {
    $('.error-message').hide();
    var isValid = false;

    var focusElement = null;
    $("input[type='text'],input[type='number']").each(function () {
        if (!$(this).val().trim()) {
            var errorElement = $(this).next('.error-message');
            $(errorElement).html("<sup>*</sup> Please " + this.placeholder);
            $(errorElement).show();
            focusElement = (focusElement == null) ? $(this) : focusElement;
        }
    });

    if (focusElement == null) {
        isValid = true;
    }
    else {
        focusElement.focus();
    }

    return isValid;
}

function modifyProduct() {
    if (validateForm()) {
        var data = $("#productForm").serialize();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Product/ModifyProduct",
            contenttype: 'application/json; charset=utf-8',
            data: data,
            success: function (data) {
                console.log("Success");
                clearForm();
                getAllProduct();
                alert("Product Modified Successfully");
            },
            error: function (data) {
                console.log("Failure");
                alert("Error while modifying");
            }
        });
    }
}

function deleteProduct(productId) {
    if (confirm("Are you sure to delete the product")) {
        var data = 'productID=' + productId;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Product/DeleteProduct",
            contenttype: 'application/json; charset=utf-8',
            data: data,
            success: function (data) {
                console.log("Success");
                clearForm();
                getAllProduct();
                alert("Deleted Successfully");
            },
            error: function (data) {
                console.log("Failure");
                alert("Error while saving");
            }
        });
    }
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
            for (var i = 0; i < productList.length; i++)
            {
                var actionButton = "<button class='btn btn-sm btn-danger float-right action-btn mr-2' onclick='deleteProduct(\"" + productList[i].productId + "\")'><i class='fa fa-trash'></i></button>" +
                    "<button class='btn btn-sm btn-secondary float-right action-btn mr-2' onclick='getProduct(\"" + productList[i].productId+ "\")'><i class='fa fa-edit'></i></button>";

                productArray.push([
                    productList[i].productName,
                    actionButton,
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

function getProduct(productId) {
    var data = 'productID=' + productId;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Product/GetAllProducts",
        contenttype: 'application/json; charset=utf-8',
        data: data,
        success: function (data) {
            console.log(data);
            $("#productName").val(data.productName);
            $("#productId").val(data.productId);
            $("#productDescription").val(data.productDescription);
            $("#productPrice").val(data.productPrice);
            $("#productCode").val(data.productCode);
            $("#productAvailableCount").val(data.productAvailableCount);
            $("#addButton").hide();
            $("#modifyButton").show();
            
        },
        error: function (data) {
            console.log("Failure");
            alert("Error while retrieving");
        }
    });
}
