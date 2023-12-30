
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
            $('.breadCrumbItem')[0].innerHTML = "View Quotations";
            $('.contentHeader')[0].innerHTML = "View Quotations";
        }
    });
});
jQuery(document).ready(function ($) {
    bindMasters();    
    if ($('#hdnPageLoadOption').val() == 'ViewQuotationsDetails') {

    }
    $('.select2').select2();
    $('.select2bs4').select2({
        theme: 'bootstrap4',
    })
});
function bindMasters() {
    if ($('#hdnCompanies').val() != null && $('#hdnCompanies').val() != '') {
        $("#ddlParty").html('');
        var res = JSON.parse($('#hdnCompanies').val());
        $("#ddlParty").append('<option value=0>Select</option>');
        for (var i = 0; i < res.length; i++) {
            $("#ddlParty").append('<option value=' + res[i].Party_Id + '>' + res[i].Party_Name + '</option>');
        }
        const jsonArray = JSON.stringify(res);
        localStorage.clear();
        localStorage.setItem('parties', jsonArray);
    }
}
function getQuotations(data) {
    var Id = data.value;
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Quotations/Quotations/GetDetailsByParty",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { id: Id },
            type: "Get",
            success: function (result) {
                if (result[0].QuotationDetails.length > 0) {
                    bindData(result);
                }
                else {
                    toastr.error('No details found for this record.');
                    $('#tblQuotationsData thead').html('');
                    $('#tblQuotationsData tbody').html('');
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
        toastr.error('No details found for this record.');
        $('#tblQuotationsData thead').html('');
        $('#tblQuotationsData tbody').html('');
        HideProgress();
    }
}
function bindData(result) {
    $('#tblQuotationsData thead').html('');
    $('#tblQuotationsData tbody').html('');
    if (result.length > 0) {
        var theader = "<tr role='row'>";
        theader += "<th colspan='12'> Company: <b>" + $('#ddlParty').find(':selected').text() + " </b></th>";
        theader += "</tr>";
        $('#tblQuotationsData thead').append(theader);
        var thead = "<tr role='row'>";
        //thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Quotation No: activate to sort column descending'> Quotation No </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Register No: activate to sort column descending'> Register No </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Party Name: activate to sort column descending'> Party Name </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Address: activate to sort column descending'> Address </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Remarks: activate to sort column descending'> Remarks </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Kind Attn. To: activate to sort column descending'> Kind Attn. To </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sample Name: activate to sort column descending'> Sample Name </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Original Code: activate to sort column descending'> Original Code </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Application/Usage: activate to sort column descending'> Application/Usage </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Quantity: activate to sort column descending'> Quantity </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Rate: activate to sort column descending'> Rate </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblQuotationsData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Quotation Date: activate to sort column descending'> Quotation Date </th>";
        thead += "</tr>";
        $('#tblQuotationsData thead').append(thead);
        var row = '';
        for (var i = 0; i < result.length; i++) {

            row += "<tr role='row'>";
            row += "<td>" + result[i].QuotationNo + "</td>";
            row += "<td>" + result[i].RegisterNo + "</td>";
            row += "<td>" + result[i].PartyName + "</td>";
            row += "<td>" + result[i].ShippingAddress + "</td>";
            row += "<td>" + result[i].ShippingAddressRemarks + "</td>";
            row += "<td>" + result[i].KindAttTo + "</td>";
            row += "<td>" + result[i].QuotationDetails[0].SampleName + "</td>";
            row += "<td>" + result[i].QuotationDetails[0].ActualNameValue + "</td>";
            row += "<td>" + result[i].QuotationDetails[0].Group + "</td>";
            row += "<td>" + result[i].QuotationDetails[0].Quantity + "</td>";
            row += "<td>" + result[i].QuotationDetails[0].Rate + "</td>";
            row += "<td>" + ConvertDateFormat(result[i].Quotation_DOE) + "</td>";
            row += "</tr>";

        }
        HideProgress();
        if ($.fn.DataTable.isDataTable('#tblQuotationsData')) {
            $('#tblQuotationsData').DataTable().clear().destroy();
        }
        $('#tblQuotationsDataBody').append(row);    
        $('#tblQuotationsData').DataTable();
        $('#tblQuotationsData').css('display', 'block');
        
    }
    else {
        HideProgress();
    }
}
$(function () {
    $("#btnPrints").click(function () {
        $('#dvContents').printThis({
            //printDelay: 1,
        });
    });
});
   