var selectedApplicationUsage = new Array();
var getApplicationUsageDetailsData = new Array();
var response;
var closestTR;
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
            $('.breadCrumbItem')[0].innerHTML = "Samples";
            $('.contentHeader')[0].innerHTML = "Samples";
        }
    });
});
jQuery(document).ready(function ($) {
    if ($('#hdnPageLoadOption').val() == 'ViewSamplesDetails') {
        GetDetails();
    }
    if ($('#hdnPageLoadOption').val() == 'Samples') {
        bindMasters();
        $('.nav-link').removeClass("active");
        $('.nav-item').removeClass("active menu-open");
        $(this).parent().parent().parent().addClass("active menu-open");
        //$(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
        $(this).parent().addClass('active');
        $(this).css('background-color', '#dee2e6');
        $(this).css('color', 'black');
        $('.breadCrumbItem').addClass("active");
        $('.breadCrumbItem')[0].innerHTML = "Samples";
        $('.contentHeader')[0].innerHTML = "Samples";
        $('.select2').select2();
        $('.select2bs4').select2({
            theme: 'bootstrap4',

        })
    }
    if ($('#hdnPageLoadOption').val() == 'EditSamples') {
        $('.nav-link').removeClass("active");
        $('.nav-item').removeClass("active menu-open");
        $(this).parent().parent().parent().addClass("active menu-open");
        //$(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
        $(this).parent().addClass('active');
        $(this).css('background-color', '#dee2e6');
        $(this).css('color', 'black');
        $('.breadCrumbItem').addClass("active");
        $('.breadCrumbItem')[0].innerHTML = "Samples";
        $('.contentHeader')[0].innerHTML = "Samples";
        if ($('#hiddenQuotationId').val() != '') {
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
            bindMasters();
            EditSamples($('#hiddenQuotationId').val());
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
    $("#txtQuotationDate").datepicker({ dateFormat: 'dd/mm/yy' });
});
$('#btnAddNewProduct').click(function () {
    $('#btnSave').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#header')[0].innerText = "Add Product";
    //Refresh();
    //bindMasters();
    $('#modal-product').modal('toggle');
});
$('#btnAddSamples').click(function () {
    ShowProgress();
    window.location.href = "/Samples/Samples/Index";
});
function GetDetails() {

    ShowProgress();
    var obj = {};
    $.ajax({
        url: "/Samples/SamplesDetails/GetDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Get",
        success: function (result) {
            var a = result;
            if (result.length > 0) {
                bindData(result);
            }
            else {
                toastr.error('No data found.');
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

function DeleteSamples(Id) {
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Samples/Samples/DeleteDetails/" + Id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: { id: Id },
            type: "Delete",
            success: function (result) {
                var a = result;
                toastr.error('Details removed successfully.');
                GetDetails();
            },
            error: function (msg) {
                toastr.error(msg);
                HideProgress();
                return false;
            }
        });
    }
    else {
        toastr.error('No details found for this record.');
    }
}

function bindData(result) {
    $('#tblSamplesData thead').html('');
    $('#tblSamplesData tbody').html('');
    $('#dvAddButton').show();
    if (result.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblSamplesData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblSamplesData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Company Name : activate to sort column descending'> Company Name</th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblSamplesData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Manufacturer : activate to sort column descending'> Quotation No</th>";

        thead += "<th> Action </th>";
        //thead += "<th> Delete </th>";
        thead += "</tr>";
        $('#tblSamplesData thead').append(thead);
        var row = '';
        for (var i = 0; i < result.length; i++) {
            row += "<tr role='row'>";
            row += "<td class='sorting_1' id='SamplesId" + result[i].QuotationMasterId + "' style='display:none'>" + result[i].QuotationMasterId + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result[i].PartyName + "</td>";
            row += "<td>" + result[i].QuotationNo + "</td>";
            //row += "<td><a href='/Samples/EditSamples/Index?id=" + result.listclsSamplesDetailsModel[i].Id + "'><img src='../Images/edit.png' style='width:15px; height:15px'/></a></td>";
            //row += "<td><img onclick=EditSamples(" + result[i].Rep_ID + ") src='/Images/edit.png' style='width:25px; height:25px'/></td>";
            row += "<td><a href='/Samples/EditSamples/Index?id=" + result[i].QuotationMasterId + "')><i class='fas fa-edit' style='font-size:20px;color:blue'></i></a></td>";
            //row += "<td><img onclick=DeleteSamples(" + result.listclsSamplesDetailsModel[i].Id + ") src='/Images/delete.png' style='width:25px; height:25px'/></td>";
            row += "</tr>";

        }
        HideProgress();
        if ($.fn.DataTable.isDataTable('#tblSamplesData')) {
            $('#tblSamplesData').DataTable().clear().destroy();
        }
        $('#tblSamplesDataBody').append(row);
        $('#tblSamplesData').DataTable();
    }
    else {
        HideProgress();
    }
}

function bindMasters() {
    if ($('#hdnLookup').val() != null && $('#hdnLookup').val() != '') {
        response = JSON.parse($('#hdnLookup').val());
        let Unit = response.filter(item => item.Type == "UNIT-TYPE");

        if (Unit.length > 0) {
            $("#ddlUnit").html('');
            $("#ddlUnit").append('<option value=0>Select</option>');
            for (var i = 0; i < Unit.length; i++) {
                $("#ddlUnit").append('<option value=' + Unit[i].LookupId + '>' + Unit[i].Description + '</option>');
            }
        }
    }
    if ($('#hdnCompany').val() != null && $('#hdnCompany').val() != '') {
        $("#ddlCompany").html('');
        var res = JSON.parse($('#hdnCompany').val());
        $("#ddlCompany").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlCompany").append('<option value=' + res[i].Party_Id + '>' + res[i].Party_Name + '</option>');

        }

    }
    if ($('#hdnProduct').val() != null && $('#hdnProduct').val() != '') {
        $("#ddlProduct").html('');
        var res = JSON.parse($('#hdnProduct').val());
        $("#ddlProduct").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlProduct").append('<option value=' + res[i].Product_Id + '>' + res[i].Product_Name + '</option>');
        }
    }
    if ($('#hdnApplicationUsage').val() != null && $('#hdnApplicationUsage').val() != '') {
        $("#ddlAppUsage").html('');
        var res = JSON.parse($('#hdnApplicationUsage').val());
        $("#ddlAppUsage").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlAppUsage").append('<option value=' + res[i].ApplicationUsage_Id + '>' + res[i].ApplicationUsage_Name + '</option>');
        }
    }
    if ($('#hdnLookup').val() != null && $('#hdnLookup').val() != '') {
        response = JSON.parse($('#hdnLookup').val());
        let termsCondition = response.filter(item => item.Type == "TERMS");

        if (termsCondition.length > 0) {
            $('#txtTermCond').val(termsCondition[0].Description);
        }
    }
}
function bindContactDetails() {
    $("#ddlAttTo").html('');
    $("#ddlAddress").html('');
    var party = JSON.parse($('#hdnCompany').val());
    let selected_Party = party.filter(item => item.Party_Id == $('#ddlCompany').val());
    if (selected_Party[0].Contact.length > 0) {

        $("#ddlAttTo").append('<option value=0>Select</option>');
        for (var i = 0; i < selected_Party[0].Contact.length; i++) {
            $("#ddlAttTo").append('<option value=' + selected_Party[0].Contact[i].Contact_Id + '>' + selected_Party[0].Contact[i].ContactPersonName + '</option>');
        }
    }
    if (selected_Party[0].PartyAddress.length > 0) {
        $("#ddlAddress").html('');
        $("#ddlAddress").append('<option value=0>Select</option>');
        for (var i = 0; i < selected_Party[0].PartyAddress.length; i++) {
            $("#ddlAddress").append('<option value=' + selected_Party[0].PartyAddress[i].PartyAddress_Id + '>' + selected_Party[0].PartyAddress[i].FullAddress + '</option>');
        }
    }
}
$('#btnSaveSamples').click(function (e) {

    e.preventDefault();
    var url = "/Samples/Samples/AddSamples";
    var quotationList = [];
    if ($('#ViewQuotationTableData > tbody > tr').length > 0) {
        $('#ViewQuotationTableData > tbody > tr').each(function (index, tr) {
            //appUsage[$(this).val()] = $(this).text();
            var row = $(tr).closest("tr");
            var quotation = {};
            quotation.SampleName = row.find('.sampleName').val()
            quotation.ActualName = row.find('.productId')[0].innerText
            quotation.Group = row.find('.appUsage')[0].children[0].value
            quotation.Quantity = row.find('.quantity')[0].innerText
            quotation.UnitId = row.find('.unitId')[0].innerText
            quotation.Rate = row.find('.rate')[0].innerText
            quotation.GroupId = row.find('.appUsageId')[0].children[0].value
            quotation.ActualNameValue = row.find('.product')[0].innerText
            quotationList.push(quotation)
        });
    }
    else {
        var quotation = {};
        quotation.SampleName = $('#txtSampleName').val();
        quotation.ActualName = $('#ddlProduct').val();
        quotation.Group = $('#ddlAppUsage').find(':selected').text();
        quotation.Quantity = $('#txtQuantity').val();
        quotation.UnitId = $('#ddlUnit').val();
        quotation.Rate = $('#txtRate').val();
        quotation.GroupId = $('#ddlAppUsage').val();
        quotation.ActualNameValue = $('#ddlProduct').find(':selected').text();
        quotationList.push(quotation)
    }

    var obj = {
        QuotationNo: $('#txtQuotationNo').val(), ////////
        RegisterNo: $('#txtRegisterNo').val(), /////////
        PartyId: $('#ddlCompany').val(), /////////////
        AddressId: $('#ddlAddress').val(), //////////////
        ShippingAddress: $('#ddlAddress').find(':selected').text(),/////
        KindAttTo: $('#ddlAttTo').find(':selected').text(), /////////
        PartyName: $('#ddlCompany').find(':selected').text(),
        ContactId: $('#ddlAttTo').val(),
        Terms: $('#txtTermCond').val(), /////////////
        ShippingAddressRemarks: $('#txtRemarks').val(), //////////////
        QuotationDetails: quotationList
    }
    if ($('#txtCompanyName').val() == '') {
        toastr.error('Company name is required.');
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
                if (result.ErrorCodes == null) {
                    toastr.success("Saved successfully");
                    HideProgress();
                    setTimeout(function () { window.location = "/Samples/SamplesDetails/Index"; }, 1000);
                }
                else {

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
$('#btnUpdateSamples').click(function (e) {

    e.preventDefault();
    var url = "/Samples/EditSamples/UpdateSamples";
    var quotationList = [];
    if ($('#ViewQuotationTableData > tbody > tr').length > 0) {
        $('#ViewQuotationTableData > tbody > tr').each(function (index, tr) {
            //appUsage[$(this).val()] = $(this).text();
            var row = $(tr).closest("tr");
            var quotation = {};
            quotation.SampleName = row.find('.sampleName').val()
            quotation.ActualName = row.find('.productId')[0].innerText
            quotation.Group = row.find('.appUsage')[0].children[0].value
            quotation.Quantity = row.find('.quantity')[0].innerText
            quotation.UnitId = row.find('.unitId')[0].innerText
            quotation.Rate = row.find('.rate')[0].innerText
            quotation.GroupId = row.find('.appUsageId')[0].children[0].value
            quotation.ActualNameValue = row.find('.product')[0].innerText
            quotation.QuotationDetailsId = row.find('.quotationDetailsId')[0].innerText
            quotationList.push(quotation)
        });
    }
    else {
        var quotation = {};
        quotation.SampleName = $('#txtSampleName').val();
        quotation.ActualName = $('#ddlProduct').val();
        quotation.Group = $('#ddlAppUsage').find(':selected').text();
        quotation.Quantity = $('#txtQuantity').val();
        quotation.UnitId = $('#ddlUnit').val();
        quotation.Rate = $('#txtRate').val();
        quotation.GroupId = $('#ddlAppUsage').val();
        quotation.ActualNameValue = $('#ddlProduct').find(':selected').text();
        quotation.QuotationDetailsId = $('#txtQuotationDetailsId').val();
        quotationList.push(quotation)
    }

    var obj = {
        QuotationMasterId: $('#txtQuotationId').val(),
        QuotationNo: $('#txtQuotationNo').val(), ////////
        RegisterNo: $('#txtRegisterNo').val(), /////////
        PartyId: $('#ddlCompany').val(), /////////////
        AddressId: $('#ddlAddress').val(), //////////////
        ShippingAddress: $('#ddlAddress').find(':selected').text(),/////
        KindAttTo: $('#ddlAttTo').find(':selected').text(), /////////
        PartyName: $('#ddlCompany').find(':selected').text(),
        ContactId: $('#ddlAttTo').val(),
        Terms: $('#txtTermCond').val(), /////////////
        ShippingAddressRemarks: $('#txtRemarks').val(), //////////////
        QuotationDetails: quotationList
    }
    if ($('#txtCompanyName').val() == '') {
        toastr.error('Company name is required.');
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
                if (result.ErrorCodes == null) {
                    toastr.success("Updated successfully");
                    HideProgress();
                    setTimeout(function () { window.location = "/Samples/SamplesDetails/Index"; }, 1000);
                }
                else {

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
function btnAddQuotationDetail() {
    $('#divQuotationDetails').show();
    if ($('#ddlProduct').val() == "0") {
        toastr.error('Product is required.');
        $('#ddlProduct').focus();
        return false;
    }
    else if ($('#txtSampleName').val() == "") {
        toastr.error('Sample name is required.');
        $('#txtSampleName').focus();
        return false;
    }
    else if ($('#txtQuantity').val() == "") {
        toastr.error('Quantity is required.');
        $('#txtQuantity').focus();
        return false;
    }
    var i = $('#ViewQuotationTableData tbody tr').length - 1;
    var product = $('#ddlProduct').val() == "0" ? "" : $('#ddlProduct').find(':selected').text();
    var appUsage = $('#ddlAppUsage').val() == "0" ? "" : $('#ddlAppUsage').find(':selected').text();
    var unit = $('#ddlUnit').val() == "0" ? "" : $('#ddlUnit').find(':selected').text();
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td class='appUsage'>" + appUsage + "</td>";
    Newrow += "<td class='sampleName'>" + $('#txtSampleName').val() + "</td>";
    Newrow += "<td class='product'>" + product + "</td>";
    Newrow += "<td class='quantity'>" + $('#txtQuantity').val() + "</td>";
    Newrow += "<td class='unit'>" + unit + "</td>";
    Newrow += "<td class='rate'>" + $('#txtRate').val() + "</td>";
    Newrow += "<td><input type='checkBox' checked=checked disabled /></td>";
    Newrow += "<td class='appUsageId' style='display:none'>" + $('#ddlAppUsage').val() + "</td>";
    Newrow += "<td class='productId' style='display:none'>" + $('#ddlProduct').val() + "</td>";
    Newrow += "<td class='unitId' style='display:none'>" + $('#ddlUnit').val() + "</td>";
    Newrow += "<td><a onclick=EditDetailsRow(this)><i class='fas fa-edit' style='font-size:20px;color:blue'></i></a> &nbsp;<a onclick=DeleteDetailsRow(this)><i class='far fa-trash-alt' style='font-size:20px;color:red'></i></a></td>";
    Newrow += "</tr>";
    $('#ViewQutationDataBody').append(Newrow);
    RefreshQuotationDetails();
};
function RefreshQuotationDetails() {
    $('#txtSampleName').val('');
    $('#txtQuantity').val('');
    $('#txtRate').val('');
    if ($('#hdnLookup').val() != null && $('#hdnLookup').val() != '') {
        response = JSON.parse($('#hdnLookup').val());
        let Unit = response.filter(item => item.Type == "UNIT-TYPE");

        if (Unit.length > 0) {
            $("#ddlUnit").html('');
            $("#ddlUnit").append('<option value=0>Select</option>');
            for (var i = 0; i < Unit.length; i++) {
                $("#ddlUnit").append('<option value=' + Unit[i].LookupId + '>' + Unit[i].Description + '</option>');
            }
        }
    }

    if ($('#hdnProduct').val() != null && $('#hdnProduct').val() != '') {
        $("#ddlProduct").html('');
        var res = JSON.parse($('#hdnProduct').val());
        $("#ddlProduct").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlProduct").append('<option value=' + res[i].Product_Id + '>' + res[i].Product_Name + '</option>');
        }
    }
    if ($('#hdnApplicationUsage').val() != null && $('#hdnApplicationUsage').val() != '') {
        $("#ddlAppUsage").html('');
        var res = JSON.parse($('#hdnApplicationUsage').val());
        $("#ddlAppUsage").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlAppUsage").append('<option value=' + res[i].ApplicationUsage_Id + '>' + res[i].ApplicationUsage_Name + '</option>');
        }
    }
}
function EditDetailsRow(data) {
    RefreshQuotationDetails();
    var row = $(data).closest("tr");
    closestTR = $(data).closest("tr");
    $('#ddlAppUsage').val(row.find('.appUsageId ')[0].innerText).attr('selected', 'selected');
    $('#txtSampleName').val(row.find('.sampleName')[0].innerText);
    $('#txtQuantity').val(row.find('.quantity')[0].innerText);
    $('#txtRate').val(row.find('.rate')[0].innerText);
    $('#ddlProduct').val(row.find('.productId ')[0].innerText).attr('selected', 'selected');
    $('#ddlUnit').val(row.find('.unitId')[0].innerText).attr('selected', 'selected');
    $('#btnAddQuotationDetails').hide();
    $('#btnUpdateQuotationDetails').show();
}

function btnUpdateQuotationDetail() {

    if ($('#ddlProduct').val() == "0") {
        toastr.error('Product is required.');
        $('#ddlProduct').focus();
        return false;
    }
    else if ($('#txtSampleName').val() == "") {
        toastr.error('Sample name is required.');
        $('#txtSampleName').focus();
        return false;
    }
    else if ($('#txtQuantity').val() == "") {
        toastr.error('Quantity is required.');
        $('#txtQuantity').focus();
        return false;
    }
    var product = $('#ddlProduct').val() == "0" ? "" : $('#ddlProduct').find(':selected').text();
    var appUsage = $('#ddlAppUsage').val() == "0" ? "" : $('#ddlAppUsage').find(':selected').text();
    var unit = $('#ddlUnit').val() == "0" ? "" : $('#ddlUnit').find(':selected').text();

    closestTR.find('.appUsage')[0].innerText = appUsage;
    closestTR.find('.sampleName')[0].innerText = $('#txtSampleName').val();
    closestTR.find('.product')[0].innerText = product;
    closestTR.find('.quantity')[0].innerText = $('#txtQuantity').val();
    closestTR.find('.unit')[0].innerText = unit;
    closestTR.find('.rate')[0].innerText = $('#txtRate').val();
    closestTR.find('.appUsageId')[0].innerText = $('#ddlAppUsage').val();
    closestTR.find('.productId')[0].innerText = $('#ddlProduct').val();
    closestTR.find('.unitId')[0].innerText = $('#ddlUnit').val();
    RefreshQuotationDetails();
    $('#btnAddQuotationDetails').show();
    $('#btnUpdateQuotationDetails').hide();
};
function DeleteDetailsRow(data) {
    var row = $(data).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete")) {
        var table = $("#ViewQuotationTableData")[0];
        table.deleteRow(row[0].rowIndex);
    }
    if ($('#ViewQuotationTableData tbody tr').length == 0) {
        $('#divQuotationDetails').hide();
    }
}

function EditSamples(Id) {
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Samples/EditSamples/GetDetailsById",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { id: Id },
            type: "Get",
            success: function (result) {
                bindMasters();
                $('#txtQuotationId').val(result.QuotationMasterId);
                $('#ddlCompany').val(result.PartyId).attr('selected', 'selected');
                bindContactDetails(result.PartyId)
                $('#ddlAttTo').val(result.ContactId).attr('selected', 'selected');;
                $('#ddlAddress').val(result.AddressId).attr('selected', 'selected');;
                $('#txtQuotationDate').val(ConvertDateDDMMYYYY(result.Quotation_DOE));
                $('#txtQuotationNo').val(result.QuotationNo);
                $('#txtRegisterNo').val(result.RegisterNo);
                $('#txtRemarks').val(result.ShippingAddressRemarks);
                bindEditQuotationDetails(result.QuotationDetails)
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
        toastr.error('No details found for this record.');
        HideProgress();
    }
}
function bindEditQuotationDetails(result) {
    if (result.length == 1) {
        $('#ddlAppUsage').val(result[0].GroupId).attr('selected', 'selected');
        $('#txtSampleName').val(result[0].SampleName);
        $('#txtQuantity').val(result[0].Quantity);
        $('#txtRate').val(result[0].Rate);
        $('#ddlProduct').val(result[0].ActualName).attr('selected', 'selected');
        $('#ddlUnit').val(result[0].UnitId).attr('selected', 'selected');
        $('#txtQuotationDetailsId').val(result[0].QuotationDetailsId);
    }
    else {
        $('#divQuotationDetails').show();
        $('#ViewQutationDataBody tbody').html('');
        if (result.length > 0) {
            for (var i = 0; i < result.length; i++) {
                var Newrow = "";
                Newrow += "<tr id=" + i + ">";
                Newrow += "<td class='appUsage'>" + result[i].Group + "</td>";
                Newrow += "<td class='sampleName'>" + result[i].SampleName + "</td>";
                Newrow += "<td class='product'>" + result[i].ActualNameValue + "</td>";
                Newrow += "<td class='quantity'>" + result[i].Quantity + "</td>";
                Newrow += "<td class='unit'>" + result[i].Unit.Description + "</td>";
                Newrow += "<td class='rate'>" + result[i].Rate + "</td>";
                Newrow += "<td><input type='checkBox' checked=checked disabled /></td>";
                Newrow += "<td class='appUsageId' style='display:none'>" + result[i].GroupId + "</td>";
                Newrow += "<td class='productId' style='display:none'>" + result[i].ActualName + "</td>";
                Newrow += "<td class='unitId' style='display:none'>" + result[i].UnitId + "</td>";
                Newrow += "<td class='quotationDetailsId' style='display:none'>" + result[i].QuotationDetailsId + "</td>";
                Newrow += "<td><a onclick=EditDetailsRow(this)><i class='fas fa-edit' style='font-size:20px;color:blue'></i></a> &nbsp;<a onclick=DeleteDetailsRow(this)><i class='far fa-trash-alt' style='font-size:20px;color:red'></i></a></td>";
                Newrow += "</tr>";
                $('#ViewQutationDataBody').append(Newrow);
            }

            HideProgress();
        }
        else {
            HideProgress();
        }
    }
}