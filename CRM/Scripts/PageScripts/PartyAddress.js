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
            $(this).parent().addClass('active');
            $(this).css('background-color', '#dee2e6');
            $(this).css('color', 'black');
            $('.breadCrumbItem').addClass("active");
            $('.breadCrumbItem')[0].innerHTML = "Party Address";
            $('.contentHeader')[0].innerHTML = "Party Address";
        }
    });
});

toastr.options = { "timeOut": 5000 };

jQuery(document).ready(function ($) {
    bindMasters();
    if ($('#hdnPageLoadOption').val() == 'ViewPartyAddressDetails') {
        // Add any specific logic for ViewPartyAddressDetails if needed
    }
    $('.select2').select2();
    $('.select2bs4').select2({
        theme: 'bootstrap4',
    });
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

function getContact(data) {
    var partyId = data.value;
    var data = localStorage.getItem('parties');
    const result = JSON.parse(data);
    var record = result.filter(item => item.Party_Id == partyId);
    if (record.length > 0) {
        bindData(record);
    } else {
        toastr.error('No details found for this record.');
        $('#tblPartyAddressData thead').html('');
        $('#tblPartyAddressData tbody').html('');
    }
}

function bindData(result) {
    $('#tblPartyAddressData thead').html('');
    $('#tblPartyAddressData tbody').html('');

    if (result.length > 0) {
        // Add company name to the table header
        var theader = "<tr role='row'>";
        theader += "<th colspan='10'> Company: <b>" + $('#ddlParty').find(':selected').text() + " </b></th>";
        theader += "</tr>";
        $('#tblPartyAddressData thead').append(theader);

        // Define table column headers
        var thead = "<tr role='row'>";
        thead += "<th> Address </th>";
        thead += "<th> City </th>";
        thead += "<th> State </th>";
        thead += "<th> Country </th>";
        thead += "<th> PIN </th>";
        thead += "<th> Email </th>";
        thead += "<th> Contact No </th>";
        thead += "<th> Contact Remark </th>";
        thead += "<th> Address Type </th>";
        thead += "<th> Is Supplier Address </th>";
        thead += "</tr>";
        $('#tblPartyAddressData thead').append(thead);

        // Loop through each party
        var row = '';
        for (var i = 0; i < result.length; i++) {
            // Loop through all addresses for the current party
            if (Array.isArray(result[i].PartyAddress)) {
                for (var j = 0; j < result[i].PartyAddress.length; j++) {
                    row += "<tr role='row'>";
                    row += "<td>" + (result[i].PartyAddress[j].Address_Line1 || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].City?.Description || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].State?.Description || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].Country?.Description || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].Pincode || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].Contact_Email || '') + "</td>";
                    // Use PartyPhone[j] if available, otherwise fallback to empty string
                    row += "<td>" + (Array.isArray(result[i].PartyPhone) && result[i].PartyPhone[j]?.Phone_Number || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].Contact_Remark || '') + "</td>";
                    row += "<td>" + (result[i].PartyAddress[j].AddressType?.Description || '') + "</td>";
                    row += "<td><input style='margin-top:2%' type='checkbox' " + (result[i].PartyAddress[j].Supplier_Address ? "checked" : "") + " disabled /></td>";
                    row += "</tr>";
                }
            }
        }

        // Hide progress bar
        HideProgress();

        // Clear and destroy existing DataTable if initialized
        if ($.fn.DataTable.isDataTable('#tblPartyAddressData')) {
            $('#tblPartyAddressData').DataTable().clear().destroy();
        }

        // Append rows to table body
        $('#tblPartyAddressDataBody').append(row);

        // Initialize DataTable
        $('#tblPartyAddressData').DataTable();
        $('#tblPartyAddressData').css('display', 'block');
    } else {
        HideProgress();
        toastr.error('No details found for this record.');
    }
}

$(function () {
    $("#btnPrints").click(function () {
        $('#tblPartyAddressData').printThis({
            //printDelay: 1,
        });
    });
});