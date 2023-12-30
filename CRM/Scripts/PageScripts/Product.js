var selectedApplicationUsage = new Array();
var getApplicationUsageDetailsData = new Array();
$(function () {
    function stripTrailingSlash(str) {
        if (str.substr(-1) == '/') {
            return str.substr(0, str.length - 1);
        }
        return str;
    }
    var url = window.location.pathname;
    var activePage = stripTrailingSlash(url);
    $('.nav li a').each(function () {
        var currentPage = stripTrailingSlash($(this).attr('href'));

        if (activePage == currentPage) {
            $('.nav-link').removeClass("active");
            $('.nav-item').removeClass("active menu-open");
            $(this).parent().parent().parent().addClass("active menu-open");
            //$(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
            $(this).parent().addClass('active');
            $(this).css('background-color', '#dee2e6');
            $(this).css('color', 'black');
            $('.breadCrumbItem').addClass("active");
            $('.breadCrumbItem')[0].innerHTML = "Product";
            $('.contentHeader')[0].innerHTML = "Product";
        }
    });
});
jQuery(document).ready(function ($) {
    bindMasters();
    if ($('#hdnPageLoadOption').val() == 'ViewProductDetails') {
        GetDetails();
    }
    if ($('#hdnPageLoadOption').val() == 'LoadEditProduct') {
        if ($('#hiddenProductId').val() != '') {
            $('.select2').select2();
            $('.select2bs4').select2({
                theme: 'bootstrap4',

            })
            $("select").on("select2:select", function (evt) {
                var element = evt.params.data.element;
                var $element = $(element);
                $element.detach();
                $(this).append($element);
                $(this).trigger("change");
            });
            EditProduct($('#hiddenProductId').val());
        }
    }
    $('.select2').select2();
    $('.select2bs4').select2({
        theme: 'bootstrap4',

    })
    $("select").on("select2:select", function (evt) {
        var element = evt.params.data.element;
        var $element = $(element);
        $element.detach();
        $(this).append($element);
        $(this).trigger("change");
    });
    $("#txtDate").datepicker({
        dateFormat: 'dd/mm/yy', beforeShow: function () {
            setTimeout(function () {
                $('.ui-datepicker').css('z-index', 9999);
            }, 0);
        }
    });
});
function bindMasters() {
    if ($('#hdnApplicationUsage').val() != null && $('#hdnApplicationUsage').val() != '') {
        $("#ddlApplicationUsage").html('');
        var res = JSON.parse($('#hdnApplicationUsage').val());
        $("#ddlApplicationUsage").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlApplicationUsage").append('<option value=' + res[i].ApplicationUsage_Id + '>' + res[i].ApplicationUsage_Name + '</option>');
        }
    }
}
$('#btnAddProduct').click(function () {
    $('#btnSave').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#header')[0].innerText = "Add Product";
    Refresh();
    bindMasters();
    $('#modal-lg').modal('toggle');
});

$('#btnSave').click(function (e) {

    e.preventDefault();
    var url = "/Product/Product/AddProduct";
    var appUsage = [];
    $('#ddlApplicationUsage :selected').each(function () {
        //appUsage[$(this).val()] = $(this).text();
        var appUsageObj = {};
        appUsageObj.ApplicationUsage_Id = $(this).val()
        appUsage.push(appUsageObj)
    });
    var obj = {
        Product_Name: $('#txtProduct').val(),
        Product_Price: $('#txtPrice').val(),
        Product_UpdateOn: ConvertDateFormatYYMMDD($('#txtDate').val()),
        ProductApplicationUsages: appUsage
    }

    if ($('#txtProduct').val() == '') {
        toastr.error('Product Name is required.');
        return false;
    }
    else if ($('#txtPrice').val() == '') {
        toastr.error('Price is required.');
        return false;
    }
    else if ($('#txtDate').val() == '') {
        toastr.error('Date is required.');
        return false;
    }
    else {
        ShowProgress();
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "data": JSON.stringify(obj) }),
            type: "Post",
            success: function (result) {
                if (result.ErrorCodes != null) {
                    toastr.error(ErrorCodes(result.ErrorCodes));
                    $('#modal-lg').modal('hide');
                    HideProgress();
                }
                else {
                    toastr.success(SuccessMessage());
                    Refresh();
                    HideProgress();
                    $('#modal-lg').modal('hide');
                    GetDetails();
                }
            },
            error: function (msg) {
                toastr.error(msg);
                HideProgress();
                return false;
            }
        });
    }
});

$('#btnUpdate').click(function (e) {

    e.preventDefault();
    var url = "/Product/Product/UpdateProduct";
    var appUsage = [];
    $('#ddlApplicationUsage :selected').each(function () {
        //appUsage[$(this).val()] = $(this).text();
        var appUsageObj = {};
        appUsageObj.ApplicationUsage_Id = $(this).val()
        appUsage.push(appUsageObj)
    });
    var obj = {
        Product_Id: $('#txtProductId').val(),
        Product_Name: $('#txtProduct').val(),
        Product_Price: $('#txtPrice').val(),
        Product_UpdateOn: ConvertDateFormatYYMMDD($('#txtDate').val()),
        ProductApplicationUsages: appUsage
    }
    if ($('#txtProduct').val() == '') {
        toastr.error('Product Name is required.');
        return false;
    }
    else if ($('#txtPrice').val() == '') {
        toastr.error('Price is required.');
        return false;
    }
    else if ($('#txtDate').val() == '') {
        toastr.error('Date is required.');
        return false;
    }
    else {
        ShowProgress();
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "data": JSON.stringify(obj) }),
            type: "Put",
            success: function (result) {
                if (result.ErrorCodes != null) {
                    toastr.error(ErrorCodes(result.ErrorCodes));
                    $('#modal-lg').modal('hide');
                    HideProgress();
                }
                else {
                    toastr.success(UpdateMessage());
                    Refresh();
                    HideProgress();
                    $('#modal-lg').modal('hide');
                    GetDetails();
                }
            },
            error: function (msg) {
                toastr.error(msg);
                HideProgress();
                return false;
            }
        });
    }
});

function Refresh() {
    $('#txtProduct').val('');
    $('#txtPrice').val('');
    $('#txtDate').val('');
}

function GetDetails() {

    ShowProgress();
    var obj = {};
    $.ajax({
        url: "/Product/Product/GetDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "Get",
        success: function (result) {
            if (result == null) {
                toastr.error(NoRecordMessage());
                HideProgress();
            }
            else {
                var permissions = JSON.parse($('#hiddenPermission').val());
                if (permissions.Product != null) {
                    if (permissions.Product.IsView) {
                        bindData(result);
                    }
                }
                else {
                    toastr.error(NotPermission());
                    HideProgress();
                    return false;
                }
                HideProgress();
            }
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
}

function EditProduct(Id) {
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Product/Product/GetDetailsById",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { id: Id },
            type: "Get",
            success: function (result) {
                bindMasters();
                $('#btnSave').css('display', 'none');
                $('#btnUpdate').css('display', 'block');
                $('#header')[0].innerText = "Update Product";
                $('#modal-lg').modal('toggle');
                $('#txtProductId').val(result.Product_Id);
                $('#txtProduct').val(result.Product_Name);
                $('#txtPrice').val(result.Product_Price);
                var date = ConvertDateDDMMYYYY(result.Product_UpdateOn);
                $('#txtDate').val(date);
                selectedApplicationUsage = new Array();
                for (i = 0; i < result.ProductApplicationUsages.length; i++) {
                    selectedApplicationUsage.push(result.ProductApplicationUsages[i].ApplicationUsage_Id);
                }
                $('#ddlApplicationUsage').val(selectedApplicationUsage);
                var array = result.ProductApplicationUsages;
                var flags = [], output = [], appUsage = array.length, i;
                for (i = 0; i < appUsage; i++) {
                    if (flags[array[i].ApplicationUsage_Id]) continue;
                    flags[array[i].ApplicationUsage_Id] = true;
                    output.push({ ApplicationUsage_Id: array[i].ApplicationUsage.ApplicationUsage_Id, ApplicationUsageName: array[i].ApplicationUsage.ApplicationUsage_Name });
                }

                for (i = 0; i < output.length; i++) {
                    $("#ddlApplicationUsage").attr('<option value=' + output[i].ApplicationUsage_Id + ' selected="selected">' + output[i].ApplicationUsage_Name + '</option>');
                }

                HideProgress();

            },
            error: function (msg) {
                toastr.error(msg);
                HideProgress();
                return false;
            }
        });
    }
    else {
        toastr.error(IdBlank());
        HideProgress();
    }
}
function DeleteConfirmation(Id) {
    $("#txtProductId").val(Id);
    $('#ConfirmBox').modal('show');
}
function DeleteProduct(Id) {
    var Id = $("#txtProductId").val();
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Product/Product/DeleteDetails/" + Id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: { id: Id },
            type: "Delete",
            success: function (result) {
                if (result.ErrorCodes != null) {
                    toastr.error(ErrorCodes(result.ErrorCodes));
                    $('#ConfirmBox').modal('hide');
                    HideProgress();
                }
                else {
                    toastr.error(DeleteMessage());
                    $('#ConfirmBox').modal('hide');
                    GetDetails();
                }
            },
            error: function (msg) {
                toastr.error(msg);
                HideProgress();
                return false;
            }
        });
    }
    else {
        toastr.error(IdBlank());
        HideProgress();
    }
}

function bindData(result) {
    $('#tblProductData thead').html('');
    $('#tblProductData tbody').html('');
    var permissions = JSON.parse($('#hiddenPermission').val());
    if (permissions.Product.IsAdd) {
        $('#dvAddButton').show();
    }
    if (result.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblProductData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblProductData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Product : activate to sort column descending'> Product Name</th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblProductData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='IsActive : activate to sort column descending'> IsActive</th>";
        if (permissions.Product.IsEdit || permissions.Product.IsDeleted) {
            thead += "<th> Action </th>";
        }
        thead += "</tr>";
        $('#tblProductData thead').append(thead);
        var display = permissions.Product.IsEdit == true ? "inline" : "none";
        var displayDel = permissions.Product.IsDeleted == true ? "inline" : "none";
        var row = '';
        for (var i = 0; i < result.length; i++) {
            row += "<tr role='row'>";
            row += "<td class='sorting_1' id='ProductId" + result[i].Product_Id + "' style='display:none'>" + result[i].Product_Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result[i].Product_Name + "</td>";
            row += "<td><input type='checkBox' checked=" + result[i].IsActive + " disabled /></td>";
            if (permissions.Product.IsEdit || permissions.Product.IsDeleted) {
                row += "<td><a onclick=EditProduct(" + result[i].Product_Id + ")><i class='fas fa-edit' style='font-size:20px;color:#902ca8;display:" + display + "'></i></a> &nbsp;<a onclick=DeleteConfirmation(" + result[i].Product_Id + ")><i class='far fa-trash-alt' style='font-size:20px;color:red;display:" + displayDel + "'></i></a></td>";
            }
            row += "</tr>";

        }
        HideProgress();
        if ($.fn.DataTable.isDataTable('#tblProductData')) {
            $('#tblProductData').DataTable().clear().destroy();
        }
        $('#tblProductDataBody').append(row);
        $('#tblProductData').DataTable(
            { "order": [] }
        );
    }
    else {
        HideProgress();
    }
}