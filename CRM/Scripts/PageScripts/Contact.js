var selectedApplicationUsage = new Array();
var getApplicationUsageDetailsData = new Array();
var response;
var closestTR;
var closestAcc;
var CityMaster = '';
var StateMaster = '';
var CountryMaster = '';
var AddresstypeMaster = '';
var repMaster = '';
var emailPlusMaster = '';
var phonePlusMaster = '';
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
            $('.breadCrumbItem')[0].innerHTML = "Contact";
            $('.contentHeader')[0].innerHTML = "Contact";
        }
    });
});

toastr.options = {
    "timeOut": 5000
};




jQuery(document).ready(function ($) {
    if ($('#hdnPageLoadOption').val() == 'ViewContactDetails') {
        GetDetails();
    }
    if ($('#hdnPageLoadOption').val() == 'Contact') {
        bindMasters();
        bindPhoneEmailLookup();
        $('.nav-link').removeClass("active");
        $('.nav-item').removeClass("active menu-open");
        $(this).parent().parent().parent().addClass("active menu-open");
        //$(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
        $(this).parent().addClass('active');
        $(this).css('background-color', '#dee2e6');
        $(this).css('color', 'black');
        $('.breadCrumbItem').addClass("active");
        $('.breadCrumbItem')[0].innerHTML = "Contact";
        $('.contentHeader')[0].innerHTML = "Contact";
        $('.select2').select2();
        $('.select2bs4').select2({
            theme: 'bootstrap4',

        })
    }
    if ($('#hdnPageLoadOption').val() == 'EditContact') {
        bindMasters();
        bindPhoneEmailLookup();
        $('.nav-link').removeClass("active");
        $('.nav-item').removeClass("active menu-open");
        $(this).parent().parent().parent().addClass("active menu-open");
        //$(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
        $(this).parent().addClass('active');
        $(this).css('background-color', '#dee2e6');
        $(this).css('color', 'black');
        $('.breadCrumbItem').addClass("active");
        $('.breadCrumbItem')[0].innerHTML = "Contact";
        $('.contentHeader')[0].innerHTML = "Contact";
        if ($('#hiddenContactId').val() != '') {
            $('.select2').select2();
            $('.select2bs4').select2({
                theme: 'bootstrap4',

            })

            EditContact($('#hiddenPartyId').val());
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
    //$("#txtDate").datepicker({ dateFormat: 'dd/mm/yy' });
    //$("#txtDOB").datepicker({ dateFormat: 'dd/mm/yy' });
    //$("#txtAnniversary").datepicker({ dateFormat: 'dd/mm/yy' });
});
function AddPartyAddress(id) {
    $('#txtPartyId').val(id);
    RefreshAddressModal();
    bindMasters();
    $('#btnSaveAddress').css('display', 'block');
    $('#modal-Address').modal('toggle');
};
$('#btnAddContact').click(function () {
    ShowProgress();
    window.location.href = "/Contact/Contact/Index";
});
$('#btnAddAddress').click(function (e) {
    //RefreshAddressModal();
    //bindMasters();
    //$('#btnSaveAddress').css('display', 'block');
    //$('#btnUpdateAddress').css('display', 'none');
    //$('#modal-Address').modal('toggle');
    var accordionLength = $('.accordion').length;
    var newAccordionId = parseInt(accordionLength) + parseInt(1);
    var contactInfoLength = $('.contactInfoExtra').length;
    var newInfoId = parseInt(contactInfoLength) + parseInt(1);
    var extraEmailTable = $('.ExtraEmailTableData').length;
    var countEmailtable = parseInt(extraEmailTable) + parseInt(1);
    var extraphoneTable = $('.ExtraPhoneTableData').length;
    var countPhntable = parseInt(extraphoneTable) + parseInt(1);
    var row = '';

    row += ' <div id="accordion" class="accordion">';
    row += ' <div class="card card-primary">';
    row += '<div class="card-header" style="background-color:#902ca8">';
    row += '<h4 class="card-title">';
    row += '<a data-toggle="collapse" data-parent="#accordion" href="#collapse' + newAccordionId + '">New Address</a>';
    row += '</h4>';
    row += '<i class="far fa-times-circle" style="cursor:pointer;float:right" onclick="RemoveAccBox(this)"></i>';
    row += '</div>';
    row += '<div id="collapse' + newAccordionId + '" class="panel-collapse collapse in">';
    row += '<div class="card-body">';
    row += '<div class="row dyn" id="dynInfoContact' + newAccordionId + '">';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Address Type </label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlAddressType" style="width:100%" id="ddlAddressType">' + AddresstypeMaster + '</select>';
    row += '</div> </div>';
    row += '<div class="col-lg-10 ">';
    row += '<div class="form-group">';
    row += '<label>Address Line 1 <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm address1" placeholder="Address Line 1" id="txtAddressLine1">';
    row += '<input type="text" style="display:none" class="PartyAddress_Id" id="PartyAddress_Id" value="0">';
    row += '</div></div>';
    row += ' <div class="col-lg-12">';
    row += ' <div class="form-group">';
    row += '<label>Address Line 2</label>';
    row += '<input type="text" class="form-control form-control-sm address2" placeholder="Address Line 2" id="txtAddressLine2">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Country <span style="color:#dc3545">*</span></label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlCountry" style="width:100%" id="ddlCountry">' + CountryMaster + '</select>';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>State <span style="color:#dc3545">*</span></label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlState" style="width:100%" id="ddlState" disabled>' +'<option value="" disabled selected>Select</option>' + StateMaster + '</select>';

    row += '</div></div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>City <span style="color:#dc3545">*</span></label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlCity" style="width:100%" id="ddlCity" disabled>' + '<option value="" disabled selected>Select</option>' + CityMaster + '</select>';
    row += '</div></div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>PIN </label>';
    row += '<input type="text" class="form-control form-control-sm pin" placeholder="PIN" id="txtPIN">';
    row += ' </div> </div>';
    row += '<div class="col-lg-4">';
    row += '<div class="form-group">';
    row += '<label>Email</label>';
    row += '<input type="text" class="form-control form-control-sm email" placeholder="Email Id" id="txtAddressEmail">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Contact No</label>';
    row += '<input type="text" class="form-control form-control-sm numericValidate contactNo" placeholder="Contact No" id="txtAddressContact">';
    row += ' </div> </div>';
    row += '<div class="col-lg-4">';
    row += '<div class="form-group">';
    row += '<label>GST No</label>';
    row += ' <input type="text" class="form-control form-control-sm gst" placeholder="GST No" id="txtAddressGST">';
    row += '</div></div>';
    row += '<div class="col-lg-4">';
    row += '<div class="form-group">';
    row += '<label>Representatives <span style="color:#dc3545">*</span></label>';
    row += '<select class="select  Gradient-inpuHeight ddlRep" style="width:100%" data-select2-id="7" id="ddlRep">' + repMaster + '</select>';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Zone</label>';
    row += '<input type="text" class="form-control form-control-sm zone" placeholder="Zone" id="txtZone">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Is Supplier Address </label>';
    row += '<input type="checkbox" class="form-control isChk" style="width:15px" id="chkIsSupplier" />';
    row += '</div></div>';
    row += '<div class="col-lg-10">';
    row += '<div class="form-group">';
    row += '<label>Remarks </label>';
    row += '<textarea type="text" class="form-control remarks" placeholder="Enter ..." id="txtRemarks" rows="3"></textarea>';
    row += '</div></div>';

    row += '<div id="contactInfoExtra' + newInfoId + '" class="contactInfoExtra">'
    row += '<div class="card">';
    row += '<div class="card-header SubDetailsPageHeader">'
    row += '<h5 class="card-title DetailsTitle">CONTACT INFO</h5>';
    row += '<div class="card-tools">';
    row += '<button type="button" class="btn btn-tool" title="Add new">';
    row += '<i class="fas fa-plus-circle" id="btnAddContactInfo" onclick=btnAddContactInfo(this) style="float:right;cursor: pointer;color:#adb5bd"></i></button>';
    row += '</div>';
    row += '</div>';
    row += '<div class="card-body Gradientcard-outline">';
    row += ' <div class="row">';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>Title</label>';
    row += '<select id="ddlTitle" class="form-control form-control-sm ddlTitle">';
    row += ' <option value="Mr.">Mr.</option><option value="Ms.">Ms.</option>';
    row += ' </select>';
    row += ' </div> </div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>Contact Person</label>';
    row += '<input type="text" class="form-control form-control-sm contactPerson txtContactPerson" placeholder="Contact Person" id="txtContactPerson">';
    row += '<input type="text" style="display:none" class="Contact_Id" id="Contact_Id" value="0">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += ' <div class="form-group">';
    row += '<label>Designation</label>';
    row += '<input type="text" class="form-control form-control-sm designation txtDesignation" placeholder="Designation" id="txtDesignation">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += ' <div class="form-group">';
    row += ' <label>Date Of Birth </label>';
    //row += '<input type="text" class="form-control form-control-sm textbox-n" onfocus=(this.type="date") onblur=(this.type="text") name="DOB" placeholder="Date" id="txtDOB">';
    row += '<input type="text" class="form-control form-control-sm dob txtDOB" placeholder="DD/MM/YYYY" id="txtDOB">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Date Of Anniversary </label>';
    row += ' <input type="text" class="form-control form-control-sm doa txtAnniversary"  placeholder="DD/MM/YYYY" id="txtAnniversary">';
    row += '</div></div>';
    row += '<h1></h1>';
    row += '<div style="overflow:auto;width:50%">';
    row += '<table class="table table-bordered GradientdataTable ExtraEmailTableData" id="ExtraEmailTableData' + countEmailtable + '">';
    row += '<thead>';
    row += '<tr>';
    row += '<th style="width:15%">E-Mail Type</th><th>E-Mail</th>';
    row += ' <th style="width:5%">';
    row += '<button type="button" class="btn btn-tool" title="Add Row">';
    row += '<i class="fas fa-plus-circle" id="btnAddExtraEmails" onclick="btnAddExtraEmails(this)" style="float:right;cursor: pointer;color:purple"></i>';
    row += '</button>';
    row += '</th> </tr>';
    row += '</thead>';
    row += ' <tbody id="ExtraEmailDataBody">';
    row += '<tr id="0">';
    row += ' <td style="display:none" id="EmailListId" class="EmailListId">0</td>';
    row += ' <td class="additionalEmailType">';
    row += '<div class="" style="width:100px">';
    row += '<select class="custom-select Gradient-inpuHeight ddlEmailPlus" style="width:100%" id="ddlEmailPlus" data-select2-id="8">' + emailPlusMaster + '</select>';
    row += '</div>';
    row += '</td>';
    row += '<td class="additionalEmail">';
    row += '<input type="text" class="form-control form-control-sm emailPlus" style="width:230px" onchange="validateEmail(this)" placeholder="Email Id" id="txtEmailIdPlus">';
    row += '</td>';
    row += '<td></td>';
    row += '</tr>';
    row += '</tbody></table> </div>';
    row += '<div style="overflow:auto;padding-left:2px;width:50%">';
    row += '<table class="table table-bordered GradientdataTable ExtraPhoneTableData" id="ExtraPhoneTableData' + countPhntable + '">';
    row += '<thead>';
    row += '<tr>';
    row += ' <th style="width:15%">Phone Type</th>';
    row += '<th>Phone Number</th>';
    row += ' <th style="width:5%">';
    row += '<button type="button" class="btn btn-tool" title="Add Row">';
    row += '<i class="fas fa-plus-circle numbers" id="btnAddExtraPhone" onclick="btnAddExtraPhone(this)" style="float:right;cursor: pointer;color:purple;"></i>';
    row += '</button>';
    row += '</th>';
    row += '</tr>';
    row += '</thead>';
    row += '<tbody id="ExtraPhoneDataBody">';
    row += '<tr id="0">';
    row += ' <td style="display:none" id="PhoneListId" class="PhoneListId">0</td>';
    row += '<td class="additionalPhoneType">';
    row += '<div class="" style="width:100px">';
    row += '<select class="custom-select Gradient-inpuHeight ddlPhonePlus" style="width:100%" id="ddlPhoneTypePlus" data-select2-id="9">' + phonePlusMaster + '</select>';
    row += '</div>';
    row += '</td>';
    row += '<td class="additionalPhone">';
    row += '<input type="text" class="form-control form-control-sm numericValidate phonePlus" style="width:230px" placeholder="Phone Number" id="txtPhoneNoPlus">';
    row += '</td>';
    row += '<td></td>';
    row += '</tr>';
    row += '</tbody>';
    row += '</table>';
    row += '</div>';
    row += '</div>';
    row += '</div></div>';
    row += '</div></div>';
    row += '</div>';
    row += '</div>';
    row += '</div>';
    row += '</div>';
    row += '</div>';
    $('#divAccordionParent').append(row);
    $('.select').select2();


});

function RemoveAccBox(data) {
    $('#Acc-ConfirmBox').modal('show');
    closestAcc = $(data).closest(".accordion");
}
function RemoveContactInfo(data) {
    var closestContactInfo = $(data).closest(".contactInfoExtra");
    $(closestContactInfo).remove();
}
function RemoveAccordion(data) {
    $(closestAcc).remove();
    $('#Acc-ConfirmBox').modal('hide');
}

function EditAddressRow(data) {
    $('#btnSaveAddress').css('display', 'none');
    $('#btnUpdateAddress').css('display', 'block');
    $('#modal-Address').modal('toggle');
    RefreshAddressModal();
    bindMasters();
    var row = $(data).closest("tr");
    closestTR = $(data).closest("tr");
    var chk = row.find('.chkSupplier')[0].innerText == "false" ? false : true;
    $('#ddlAddressType').val(row.find('.addressTypeId ')[0].innerText).attr('selected', 'selected');
    $('#txtAddressLine1').val(row.find('.addressLine1')[0].innerText);
    $('#txtAddressLine2').val(row.find('.addressLine2')[0].innerText);
    $('#ddlCity').val(row.find('.cityId')[0].innerText).attr('selected', 'selected');
    $('#ddlState').val(row.find('.stateId')[0].innerText);
    $('#ddlCountry').val(row.find('.countryId')[0].innerText);
    $('#txtPIN').val(row.find('.pin')[0].innerText);
    $('#txtAddressEmail').val(row.find('.addressEmail')[0].innerText);
    $('#txtAddressContact').val(row.find('.addressContact')[0].innerText);
    $('#txtAddressGST').val(row.find('.addressGst')[0].innerText);
    $('#ddlRep').val(row.find('.repId')[0].innerText);
    $('#txtZone').val(row.find('.zone')[0].innerText)
    $('#chkIsSupplier').prop("checked", chk);
    $('#txtRemarks').val(row.find('.remarks')[0].innerText);
    $('#txtRowId').val(row[0].id);
}
$('#btnUpdateAddress').click(function (e) {
    $('#modal-Address').modal("hide");
    if ($('#ddlRep').val() == "0") {
        toastr.error('Representative is required.');
        $('#ddlRep').focus();
        return false;
    }
    else if ($('#ddlCountry').val() == "0") {
        toastr.error('Country is required.');
        $('#ddlCountry').focus();
        return false;
    }
    
    else if ($('#ddlState').val() == "0") {
        toastr.error('State is required.');
        $('#ddlState').focus();
        return false;
    }
   
    else if ($('#ddlCity').val() == "0") {
        toastr.error('City is required.');
        $('#ddlCity').focus();
        return false;
    }

    var checkAddressType = $('#ddlAddressType').val() == "0" ? "" : $('#ddlAddressType').find(':selected').text();
    var checkCity = $('#ddlCity').val() == "0" ? "" : $('#ddlCity').find(':selected').text();
    var checkState = $('#ddlState').val() == "0" ? "" : $('#ddlState').find(':selected').text();
    var checkCountry = $('#ddlCountry').val() == "0" ? "" : $('#ddlCountry').find(':selected').text();
    var checkRep = $('#ddlRep').val() == "0" ? "" : $('#ddlRep').find(':selected').text();
    var isSupplier = $('#chkIsSupplier')[0].checked == true ? "Yes" : "No";
    closestTR.find('.addressType')[0].innerText = checkAddressType;
    closestTR.find('.addressLine1')[0].innerText = $('#txtAddressLine1').val();
    closestTR.find('.addressLine2')[0].innerText = $('#txtAddressLine2').val();
    closestTR.find('.city')[0].innerText = checkCity;
    closestTR.find('.state')[0].innerText = checkState;
    closestTR.find('.country')[0].innerText = checkCountry;
    closestTR.find('.pin')[0].innerText = $('#txtPIN').val();
    closestTR.find('.addressEmail')[0].innerText = $('#txtAddressEmail').val();
    closestTR.find('.addressContact')[0].innerText = $('#txtAddressContact').val();
    closestTR.find('.addressGst')[0].innerText = $('#txtAddressGST').val();
    closestTR.find('.zone')[0].innerText = $('#txtZone').val();
    closestTR.find('.rep')[0].innerText = checkRep;
    closestTR.find('.supplier')[0].innerText = isSupplier;
    closestTR.find('.remarks')[0].innerText = $('#txtRemarks').val();
    closestTR.find('.addressTypeId')[0].innerText = $('#ddlAddressType').val();
    closestTR.find('.cityId')[0].innerText = $('#ddlCity').val();
    closestTR.find('.stateId')[0].innerText = $('#ddlState').val();
    closestTR.find('.countryId')[0].innerText = $('#ddlCountry').val();
    closestTR.find('.repId')[0].innerText = $('#ddlRep').val();
    closestTR.find('.chkSupplier')[0].innerText = $('#chkIsSupplier')[0].checked;
    RefreshAddressModal();
});
function DeleteAddressRow(data) {
    var row = $(data).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete")) {
        var table = $("#ViewAddressTableData")[0];
        table.deleteRow(row[0].rowIndex);
    }
}
$('#btnAddContact').click(function () {
    ShowProgress();
    var url = window.location.pathname;
    var activePage = url;
    $('.nav li a').each(function () {
        //var currentPage = stripTrailingSlash($(this).attr('href'));

        //if (activePage == currentPage) {
        $('.nav-link').removeClass("active");
        $('.nav-item').removeClass("active menu-open");
        $(this).parent().parent().parent().addClass("active menu-open");
        //$(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
        $(this).parent().addClass('active');
        $(this).css('background-color', '#dee2e6');
        $(this).css('color', 'black');
        $('.breadCrumbItem').addClass("active");
        $('.breadCrumbItem')[0].innerHTML = "Contact";
        $('.contentHeader')[0].innerHTML = "Contact";
        //}
    });
    window.location.href = "/Contact/Contact/Index";
});







$('#btnSaveContact').click(function (e) {
    e.preventDefault();

    // Validate required fields
    var companyName = $('#txtCompanyName').val();
    var addressLine1 = $('.address1').val();
    var city = $('.ddlCity').val();
    var country = $('.ddlCountry').val();
    var representativeName = $('.ddlRep').val();
    var state = $('.ddlState').val();

    if (!companyName) {
        toastr.error('Company name is required.');
        return false;
    }
    if (!addressLine1) {
        toastr.error('Address is required.');
        return false;
    }
    if (!country || country === "0") {
        toastr.error('Country is required.');
        return false;
    }
    if (!state || state === "0") {
        toastr.error('State is required.');
        return false;
    }
    if (!city || city === "0") {
        toastr.error('City is required.');
        return false;
    }
    if (!representativeName || representativeName === "0") {
        toastr.error('Representative Name is required.');
        return false;
    }

    // Collect main party emails
    var partyEmails = [];
    $('#EmailTableData > tbody > tr').each(function (index, tr) {
        var row = $(tr).closest("tr");
        if (row.find('.firstEmail')[0].children[0].value == "") {
            return true; // Skip empty rows
        }
        var partyEmailObj = {};
        partyEmailObj.Email_Type = row.find('.firstEmailType :selected').val() === "0" ? null : row.find('.firstEmailType :selected').val();
        partyEmailObj.Email = row.find('.firstEmail')[0].children[0].value;
        partyEmails.push(partyEmailObj);
    });

    // Collect main party phones
    var partyPhones = [];
    $('#PhoneTableData > tbody > tr').each(function (index, tr) {
        var row = $(tr).closest("tr");
        if (row.find('.firstPhone')[0].children[0].value == "") {
            return true; // Skip empty rows
        }
        var partyPhoneObj = {};
        partyPhoneObj.Phone_Type = row.find('.firstPhoneType :selected').val() === "0" ? null : row.find('.firstPhoneType :selected').val();
        partyPhoneObj.Phone_Number = row.find('.firstPhone')[0].children[0].value;
        partyPhones.push(partyPhoneObj);
    });

    // Collect party addresses with contact info
    var partyAddress = [];
    var acc = $('.accordion');
    for (var i = 0; i < acc.length; i++) {
        var partyAddressObj = {};
        partyAddressObj.Contact_Email = $(acc[i]).find('.email').val();
        partyAddressObj.Contact_Phone = $(acc[i]).find('.contactNo').val();
        partyAddressObj.Address_Type = $(acc[i]).find('.ddlAddressType').val() === "0" ? "1" : $(acc[i]).find('.ddlAddressType').val();
        partyAddressObj.Address_Line1 = $(acc[i]).find('.address1').val();
        partyAddressObj.Address_Line2 = $(acc[i]).find('.address2').val();
        partyAddressObj.CountryId = $(acc[i]).find('.ddlCountry').val();
        partyAddressObj.StateId = $(acc[i]).find('.ddlState').val();
        partyAddressObj.CityId = $(acc[i]).find('.ddlCity').val();
        partyAddressObj.GST = $(acc[i]).find('.gst').val();
        partyAddressObj.Supplier_Address = $(acc[i]).find('.isChk')[0].checked;
        partyAddressObj.Contact_Remark = $(acc[i]).find('.remarks').val();
        partyAddressObj.Pincode = $(acc[i]).find('.pin').val();
        partyAddressObj.Representative_Id = $(acc[i]).find('.ddlRep').val() === "0" ? null : $(acc[i]).find('.ddlRep').val();
        partyAddressObj.Zone = $(acc[i]).find('.zone').val();

        // Collect contact info for this address
        var extraInfoHeader = $(acc[i]).find('.contactInfoExtra');
        var additionalInfo = [];
        for (var k = 0; k < extraInfoHeader.length; k++) {
            var addCon = {};
            addCon.Contact_Id = $(extraInfoHeader[k]).find('.Contact_Id').val() || "0";
            addCon.ContactTitle = $(extraInfoHeader[k]).find('.ddlTitle').val();
            addCon.ContactPersonName = $(extraInfoHeader[k]).find('.txtContactPerson').val();
            addCon.Designation = $(extraInfoHeader[k]).find('.txtDesignation').val();
            addCon.DateOfBirth = ConvertDateFormatYYMMDD($(extraInfoHeader[k]).find('.txtDOB').val());
            addCon.DateOfAnniversary = ConvertDateFormatYYMMDD($(extraInfoHeader[k]).find('.txtAnniversary').val());

            // Collect emails for this contact
            var emailListModels = [];
            $(extraInfoHeader[k]).find('.ExtraEmailTableData tbody tr').each(function () {
                var emailRow = $(this);
                var emailObj = {};
                emailObj.EmailListId = emailRow.find('.EmailListId').text() || "0";
                emailObj.Email_Type = emailRow.find('.ddlEmailPlus').val() === "0" ? null : emailRow.find('.ddlEmailPlus').val();
                emailObj.Email = emailRow.find('.emailPlus').val();
                if (emailObj.Email) { // Only include non-empty emails
                    emailListModels.push(emailObj);
                }
            });
            addCon.emailListModels = emailListModels;

            // Collect phones for this contact
            var phoneListModels = [];
            $(extraInfoHeader[k]).find('.ExtraPhoneTableData tbody tr').each(function () {
                var phoneRow = $(this);
                var phoneObj = {};
                phoneObj.PhoneListId = phoneRow.find('.PhoneListId').text() || "0";
                phoneObj.Phone_Type = phoneRow.find('.ddlPhonePlus').val() === "0" ? null : phoneRow.find('.ddlPhonePlus').val();
                phoneObj.PhoneNumber = phoneRow.find('.phonePlus').val();
                if (phoneObj.PhoneNumber) { // Only include non-empty phones
                    phoneListModels.push(phoneObj);
                }
            });
            addCon.phoneListModels = phoneListModels;

            additionalInfo.push(addCon);
        }
        partyAddressObj.Contact = additionalInfo;
        partyAddress.push(partyAddressObj);
    }

    var obj = {
        Party_Name: companyName,
        Manufacturer_Type: $('#txtManufacturer').val(),
        PartyEmails: partyEmails.length > 0 ? partyEmails : null,
        PartyPhone: partyPhones.length > 0 ? partyPhones : null,
        PartyAddress: partyAddress.length > 0 ? partyAddress : null
    };

    ShowProgress();
    $.ajax({
        url: "/Contact/Contact/AddParty",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            if (result.ErrorCodes != null) {
                SweetErrorMessage(result.ErrorCodes);
                HideProgress();
            } else {
                SweetSuccessMessage();
                RefreshAddressModal();
                HideProgress();
                setTimeout(function () { window.location = "/Contact/ContactDetails"; }, 1000);
            }
        },
        error: function (msg) {
            toastr.error(msg.responseText || "An error occurred.");
            HideProgress();
            return false;
        }
    });
});


































$('#btnUpdateContact').click(function (e) {
    e.preventDefault();

    // Validate required fields
    var companyName = $('#txtCompanyName').val().trim();
    var addressLine1 = $('.address1').val().trim();
    var city = $('.ddlCity').val();
    var state = $('.ddlState').val();
    var country = $('.ddlCountry').val();
    var representativeName = $('.ddlRep').val();

    if (!companyName) {
        toastr.error('Company name is required.');
        return false;
    }
    if (!addressLine1) {
        toastr.error('Address is required.');
        return false;
    }
    if (!country || country === "0") {
        toastr.error('Country is required.');
        return false;
    }
    if (!state || state === "0") {
        toastr.error('State is required.');
        return false;
    }
    if (!city || city === "0") {
        toastr.error('City is required.');
        return false;
    }
    if (!representativeName || representativeName === "0") {
        toastr.error('Representative Name is required.');
        return false;
    }

    var url = "/Contact/EditContact/UpdateParty";

    // Collect main party emails
    var partyEmails = [];
    $('#EmailTableData > tbody > tr').each(function (index, tr) {
        var row = $(tr).closest("tr");
        var partyEmailObj = {};
        var emailIdText = row.find('.PartyEmail_Id')[0].innerText.trim();
        partyEmailObj.PartyEmail_Id = emailIdText ? parseInt(emailIdText, 10) || 0 : 0;
        partyEmailObj.Email_Type = row.find('.firstEmailType :selected').val() === "0" ? null : row.find('.firstEmailType :selected').val();
        partyEmailObj.Email = row.find('.firstEmail')[0].children[0].value;
        if (partyEmailObj.Email) { // Only include non-empty emails
            partyEmails.push(partyEmailObj);
        }
    });

    // Collect main party phones
    var partyPhones = [];
    $('#PhoneTableData > tbody > tr').each(function (index, tr) {
        var row = $(tr).closest("tr");
        var partyPhoneObj = {};
        var phoneIdText = row.find('.PartyPhone_Id')[0].innerText.trim();
        partyPhoneObj.PartyPhone_Id = phoneIdText ? parseInt(phoneIdText, 10) || 0 : 0;
        partyPhoneObj.Phone_Type = row.find('.firstPhoneType :selected').val() === "0" ? null : row.find('.firstPhoneType :selected').val();
        partyPhoneObj.Phone_Number = row.find('.firstPhone')[0].children[0].value;
        if (partyPhoneObj.Phone_Number) { // Only include non-empty phones
            partyPhones.push(partyPhoneObj);
        }
    });

    // Collect party addresses with contact info
    var partyAddress = [];
    var acc = $('.accordion');
    for (var i = 0; i < acc.length; i++) {
        var partyAddressObj = {};
        partyAddressObj.PartyAddress_Id = $(acc[i]).find('.PartyAddress_Id').val() || "0";
        partyAddressObj.Contact_Email = $(acc[i]).find('.email').val();
        partyAddressObj.Contact_Phone = $(acc[i]).find('.contactNo').val();
        partyAddressObj.Address_Type = $(acc[i]).find('.ddlAddressType').val() === "0" ? "1" : $(acc[i]).find('.ddlAddressType').val();
        partyAddressObj.Address_Line1 = $(acc[i]).find('.address1').val();
        partyAddressObj.Address_Line2 = $(acc[i]).find('.address2').val();
        partyAddressObj.CountryId = $(acc[i]).find('.ddlCountry').val();
        partyAddressObj.StateId = $(acc[i]).find('.ddlState').val();
        partyAddressObj.CityId = $(acc[i]).find('.ddlCity').val();
        partyAddressObj.GST = $(acc[i]).find('.gst').val();
        partyAddressObj.Supplier_Address = $(acc[i]).find('.isChk')[0].checked;
        partyAddressObj.Contact_Remark = $(acc[i]).find('.remarks').val();
        partyAddressObj.Pincode = $(acc[i]).find('.pin').val();
        partyAddressObj.Representative_Id = $(acc[i]).find('.ddlRep').val() === "0" ? null : $(acc[i]).find('.ddlRep').val();
        partyAddressObj.Zone = $(acc[i]).find('.zone').val();

        // Collect contact info for this address
        var extraInfoHeader = $(acc[i]).find('.contactInfoExtra');
        var additionalInfo = [];
        for (var k = 0; k < extraInfoHeader.length; k++) {
            var addCon = {};
            addCon.Contact_Id = $(extraInfoHeader[k]).find('.Contact_Id').val() || "0";
            addCon.ContactTitle = $(extraInfoHeader[k]).find('.ddlTitle').val();
            addCon.ContactPersonName = $(extraInfoHeader[k]).find('.txtContactPerson').val();
            addCon.Designation = $(extraInfoHeader[k]).find('.txtDesignation').val();
            addCon.DateOfBirth = ConvertDateFormatYYMMDD($(extraInfoHeader[k]).find('.txtDOB').val());
            addCon.DateOfAnniversary = ConvertDateFormatYYMMDD($(extraInfoHeader[k]).find('.txtAnniversary').val());

            // Collect emails for this contact
            var emailListModels = [];
            $(extraInfoHeader[k]).find('.ExtraEmailTableData tbody tr').each(function () {
                var emailRow = $(this);
                var emailObj = {};
                emailObj.EmailListId = emailRow.find('.EmailListId').text() || "0";
                emailObj.Email_Type = emailRow.find('.ddlEmailPlus').val() === "0" ? null : emailRow.find('.ddlEmailPlus').val();
                emailObj.Email = emailRow.find('.emailPlus').val();
                if (emailObj.Email) { // Only include non-empty emails
                    emailListModels.push(emailObj);
                }
            });
            addCon.emailListModels = emailListModels;

            // Collect phones for this contact
            var phoneListModels = [];
            $(extraInfoHeader[k]).find('.ExtraPhoneTableData tbody tr').each(function () {
                var phoneRow = $(this);
                var phoneObj = {};
                phoneObj.PhoneListId = phoneRow.find('.PhoneListId').text() || "0";
                phoneObj.Phone_Type = phoneRow.find('.ddlPhonePlus').val() === "0" ? null : phoneRow.find('.ddlPhonePlus').val();
                phoneObj.PhoneNumber = phoneRow.find('.phonePlus').val();
                if (phoneObj.PhoneNumber) { // Only include non-empty phones
                    phoneListModels.push(phoneObj);
                }
            });
            addCon.phoneListModels = phoneListModels;

            additionalInfo.push(addCon);
        }
        partyAddressObj.Contact = additionalInfo;
        partyAddress.push(partyAddressObj);
    }

    var obj = {
        Party_Id: $('#txtPartyId').val() || 0,
        Party_Name: $('#txtCompanyName').val() || "N/A",
        Manufacturer_Type: $('#txtManufacturer').val() || "",
        PartyEmails: partyEmails.length > 0 ? partyEmails : null,
        PartyPhone: partyPhones.length > 0 ? partyPhones : null,
        PartyAddress: partyAddress.length > 0 ? partyAddress : null
    };

    ShowProgress();
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Put",
        success: function (result) {
            if (result.ErrorCodes != null) {
                SweetErrorMessage(result.ErrorCodes);
                HideProgress();
            } else {
                SweetUpdateMessage();
                HideProgress();
                setTimeout(function () { window.location = "/Contact/ContactDetails/Index"; }, 1000);
            }
        },
        error: function (msg) {
            let errorMessage = msg.responseText || "An error occurred.";
            toastr.error(errorMessage);
            HideProgress();
        }
    });
});



































function btnAddEmails() {
    var EmailTypeData = response.filter(item => item.Type == "PARTY-EMAIL-TYPE");
    EmailType = '';
    EmailType += '<option value=0>Select</option>';
    for (var i = 0; i < EmailTypeData.length; i++) {
        EmailType += '<option value=' + EmailTypeData[i].LookupId + '>' + EmailTypeData[i].Description + '</option>';
    }
    var i = $('#EmailTableData tbody tr').length + 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td  style='display:none' class='PartyEmail_Id'>0</td>";
    Newrow += "<td class='firstEmailType'><div style='width:200px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlEmail" + i + "'>" + EmailType + "</select></div></td>";
    Newrow += "<td class='firstEmail'><input type='text' class='form-control form-control-sm ' placeholder='Email Id' id='txtEmailId" + i + "'></td>";
    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveEmailRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#EmailDataBody tr:last').before(Newrow);
    //$('.select').select2();
    //$('.select2-selection').css('border-color', 'blue');
}
function RemoveEmailRow(data) {
    var row = $(data).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete")) {
        var table = $("#EmailTableData")[0];
        table.deleteRow(row[0].rowIndex);
    }
};
function btnAddPhone() {
    var PhoneTypeData = response.filter(item => item.Type == "PARTY-PHONE-TYPE");
    PhoneType = '';
    PhoneType += '<option value=0>Select</option>';
    for (var i = 0; i < PhoneTypeData.length; i++) {
        PhoneType += '<option value=' + PhoneTypeData[i].LookupId + '>' + PhoneTypeData[i].Description + '</option>';
    }
    var i = $('#PhoneTableData tbody tr').length - 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td  style='display:none' class='PartyPhone_Id'>0</td>";
    Newrow += "<td class='firstPhoneType''><div style='width:200px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlPhoneType" + i + "'>" + PhoneType + "</select></div></td>";
    Newrow += "<td class='firstPhone'><input type='text' class='form-control form-control-sm numericValidate' placeholder='Phone Number' id='txtPhoneNo" + i + "'></td>";
    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemovePhoneRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#PhoneDataBody tr:last').before(Newrow);
    $('.select').select2();
    //$('.select2-selection').css('border-color', 'blue');
};
function RemovePhoneRow(data) {
    var row = $(data).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete")) {
        var table = $("#PhoneTableData")[0];
        table.deleteRow(row[0].rowIndex);
    }
};
//function btnAddContactInfo(data) {
//    var closerAccordion = data.closest('.dyn');
//    var closestInfoTable = data.closest('.dyn').id;
//    var extraInfoTable = $(closerAccordion).find('.contactInfoExtra').length;
//    var extraInfoTableCount = parseInt(extraInfoTable) + parseInt(1);
//    var extraEmailTable = $('.ExtraEmailTableData').length;
//    var countEmailtable = parseInt(extraEmailTable) + parseInt(1);
//    var extraphoneTable = $('.ExtraPhoneTableData').length;
//    var countPhntable = parseInt(extraphoneTable) + parseInt(1);
//    row = '';
//    row += '<div id="contactInfoExtra' + extraInfoTableCount + '" class="contactInfoExtra">'
//    row += '<div class="card">';
//    row += '<div class="card-header SubDetailsPageHeader">'
//    row += '<h5 class="card-title DetailsTitle">CONTACT INFO</h5>';
//    row += '<div class="card-tools">';
//    //row += '<button type="button" class="btn btn-tool" title="Add new">';
//    row += '<i class="far fa-times-circle" style="cursor:pointer;float:right;padding-right:10px" onclick="RemoveContactInfo(this)"></i>';
//    //row += '</button>';
//    row += '</div>';
//    row += '</div>';
//    row += '<div class="card-body Gradientcard-outline">';
//    row += ' <div class="row">';
//    row += '<div class="col-lg-2">';
//    row += '<div class="form-group">';
//    row += ' <label>Title</label>';
//    row += '<select id="ddlTitle" class="form-control form-control-sm ddlTitle">';
//    row += ' <option value="Mr.">Mr.</option><option value="Ms.">Ms.</option>';
//    row += ' </select>';
//    row += ' </div> </div>';
//    row += ' <div class="col-lg-2">';
//    row += '<div class="form-group">';
//    row += ' <label>Contact Person <span style="color:#dc3545">*</span></label>';
//    row += '<input type="text" class="form-control form-control-sm contactPerson txtContactPerson" placeholder="Contact Person" id="txtContactPerson">';
//    row += '<input type="text" style="display:none" class="Contact_Id" id="Contact_Id" value="0">';
//    row += '</div></div>';
//    row += '<div class="col-lg-2">';
//    row += ' <div class="form-group">';
//    row += '<label>Designation <span style="color:#dc3545">*</span></label>';
//    row += '<input type="text" class="form-control form-control-sm designation txtDesignation" placeholder="Designation" id="txtDesignation">';
//    row += '</div></div>';
//    row += '<div class="col-lg-2">';
//    row += ' <div class="form-group">';
//    row += ' <label>Date Of Birth <span style="color:#dc3545">*</span></label>';
//    //row += '<input type="text" class="form-control form-control-sm textbox-n" onfocus=(this.type="date") onblur=(this.type="text") name="DOB" placeholder="Date" id="txtDOB">';
//    row += '<input type="text" class="form-control form-control-sm dob txtDOB" placeholder="DD/MM/YYYY" id="txtDOB">';
//    row += '</div></div>';
//    row += '<div class="col-lg-2">';
//    row += '<div class="form-group">';
//    row += '<label>Date Of Anniversary <span style="color:#dc3545">*</span></label>';
//    row += ' <input type="text" class="form-control form-control-sm dob txtAnniversary"  placeholder="DD/MM/YYYY" id="txtAnniversary">';
//    row += '</div></div>';
//    row += '<h1></h1>';
//    row += '<div style="overflow:auto;width:50%">';
//    row += '<table class="table table-bordered GradientdataTable ExtraEmailTableData" id="ExtraEmailTableData' + countEmailtable + '">';
//    row += '<thead>';
//    row += '<tr>';
//    row += '<th style="width:15%">E-Mail Type</th><th>E-Mail</th>';
//    row += ' <th style="width:5%">';
//    row += '<button type="button" class="btn btn-tool" title="Add Row">';
//    row += '<i class="fas fa-plus-circle" id="btnAddExtraEmails" onclick="btnAddExtraEmails(this)" style="float:right;cursor: pointer;color:purple"></i>';
//    row += '</button>';
//    row += '</th> </tr>';
//    row += '</thead>';
//    row += ' <tbody id="ExtraEmailDataBody">';
//    row += '<tr id="0">';
//    row += ' <td style="display:none" id="EmailListId" class="EmailListId">0</td>';
//    row += ' <td class="additionalEmailType">';
//    row += '<div class="" style="width:100px">';
//    row += '<select class="custom-select Gradient-inpuHeight ddlEmailPlus" style="width:100px" id="ddlEmailPlus" data-select2-id="8">' + emailPlusMaster + '</select>';
//    row += '</div>';
//    row += '</td>';
//    row += '<td class="additionalEmail">';
//    row += '<input type="text" class="form-control form-control-sm emailPlus" style="width:230px" placeholder="Email Id" id="txtEmailIdPlus">';
//    row += '</td>';
//    row += '<td></td>';
//    row += '</tr>';
//    row += '</tbody></table> </div>';
//    row += '<div style="overflow:auto;padding-left:2px;width:50%">';
//    row += '<table class="table table-bordered GradientdataTable ExtraPhoneTableData" id="ExtraPhoneTableData' + countPhntable + '">';
//    row += '<thead>';
//    row += '<tr>';
//    row += ' <th style="width:15%">Phone Type</th>';
//    row += '<th>Phone Number</th>';
//    row += ' <th style="width:5%">';
//    row += '<button type="button" class="btn btn-tool" title="Add Row">';
//    row += '<i class="fas fa-plus-circle numbers" id="btnAddExtraPhone" onclick="btnAddExtraPhone(this)" style="float:right;cursor: pointer;color:purple;"></i>';
//    row += '</button>';
//    row += '</th>';
//    row += '</tr>';
//    row += '</thead>';
//    row += '<tbody id="ExtraPhoneDataBody">';
//    row += '<tr id="0">';
//    row += ' <td style="display:none" id="PhoneListId" class="PhoneListId">0</td>';
//    row += '<td class="additionalPhoneType">';
//    row += '<div class="" style="width:100px">';
//    row += '<select class="custom-select Gradient-inpuHeight ddlPhonePlus" style="width:100%" id="ddlPhoneTypePlus" data-select2-id="9">' + phonePlusMaster + '</select>';
//    row += '</div>';
//    row += '</td>';
//    row += '<td class="additionalPhone">';
//    row += '<input type="text" class="form-control form-control-sm numericValidate phonePlus" style="width:230px" placeholder="Phone Number" id="txtPhoneNoPlus">';
//    row += '</td>';
//    row += '<td></td>';
//    row += '</tr>';
//    row += '</tbody>';
//    row += ' </table>';
//    row += '</div>';
//    row += '</div>';
//    row += '</div></div>';
//    row += '</div></div>';
//    row += '</div>';
//    $('#' + closestInfoTable).append(row);
//}

function btnAddContactInfo(data) {
    var closerAccordion = data.closest('.dyn');
    var closestInfoTable = closerAccordion.id;
    var extraInfoTable = $(closerAccordion).find('.contactInfoExtra').length;
    var extraInfoTableCount = parseInt(extraInfoTable) + parseInt(1);
    var extraEmailTable = $('.ExtraEmailTableData').length;
    var countEmailtable = parseInt(extraEmailTable) + parseInt(1);
    var extraphoneTable = $('.ExtraPhoneTableData').length;
    var countPhntable = parseInt(extraphoneTable) + parseInt(1);
    var row = '';
    row += '<div id="contactInfoExtra' + extraInfoTableCount + '" class="contactInfoExtra">';
    row += '<div class="card">';
    row += '<div class="card-header SubDetailsPageHeader">';
    row += '<h5 class="card-title DetailsTitle">CONTACT INFO</h5>';
    row += '<div class="card-tools">';
    row += '<i class="far fa-times-circle" style="cursor:pointer;float:right;padding-right:10px" onclick="RemoveContactInfo(this)"></i>';
    row += '</div>';
    row += '</div>';
    row += '<div class="card-body Gradientcard-outline">';
    row += ' <div class="row">';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>Title</label>';
    row += '<select id="ddlTitle" class="form-control form-control-sm ddlTitle select">';
    row += ' <option value="Mr.">Mr.</option><option value="Ms.">Ms.</option>';
    row += ' </select>';
    row += ' </div> </div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>Contact Person <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm contactPerson txtContactPerson" placeholder="Contact Person" id="txtContactPerson">';
    row += '<input type="text" style="display:none" class="Contact_Id" id="Contact_Id" value="0">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += ' <div class="form-group">';
    row += '<label>Designation <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm designation txtDesignation" placeholder="Designation" id="txtDesignation">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += ' <div class="form-group">';
    row += ' <label>Date Of Birth <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm dob txtDOB" placeholder="DD/MM/YYYY" id="txtDOB">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Date Of Anniversary <span style="color:#dc3545">*</span></label>';
    row += ' <input type="text" class="form-control form-control-sm dob txtAnniversary" placeholder="DD/MM/YYYY" id="txtAnniversary">';
    row += '</div></div>';
    row += '<h1></h1>';
    row += '<div style="overflow:auto;width:50%">';
    row += '<table class="table table-bordered GradientdataTable ExtraEmailTableData" id="ExtraEmailTableData' + countEmailtable + '">';
    row += '<thead>';
    row += '<tr>';
    row += '<th style="width:15%">E-Mail Type</th><th>E-Mail</th>';
    row += ' <th style="width:5%">';
    row += '<button type="button" class="btn btn-tool" title="Add Row">';
    row += '<i class="fas fa-plus-circle" id="btnAddExtraEmails" onclick="btnAddExtraEmails(this)" style="float:right;cursor: pointer;color:purple"></i>';
    row += '</button>';
    row += '</th> </tr>';
    row += '</thead>';
    row += ' <tbody id="ExtraEmailDataBody">';
    row += '<tr id="0">';
    row += ' <td style="display:none" id="EmailListId" class="EmailListId">0</td>';
    row += ' <td class="additionalEmailType">';
    row += '<div class="" style="width:100px">';
    row += '<select class="custom-select Gradient-inpuHeight ddlEmailPlus select" style="width:100%" id="ddlEmailPlus" data-select2-id="8">' + emailPlusMaster + '</select>';
    row += '</div>';
    row += '</td>';
    row += '<td class="additionalEmail">';
    row += '<input type="text" class="form-control form-control-sm emailPlus" style="width:230px" placeholder="Email Id" id="txtEmailIdPlus">';
    row += '</td>';
    row += '<td><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraEmailRow(this)"></i></td>';
    row += '</tr>';
    row += '</tbody></table> </div>';
    row += '<div style="overflow:auto;padding-left:2px;width:50%">';
    row += '<table class="table table-bordered GradientdataTable ExtraPhoneTableData" id="ExtraPhoneTableData' + countPhntable + '">';
    row += '<thead>';
    row += '<tr>';
    row += ' <th style="width:15%">Phone Type</th>';
    row += '<th>Phone Number</th>';
    row += ' <th style="width:5%">';
    row += '<button type="button" class="btn btn-tool" title="Add Row">';
    row += '<i class="fas fa-plus-circle numbers" id="btnAddExtraPhone" onclick="btnAddExtraPhone(this)" style="float:right;cursor: pointer;color:purple;"></i>';
    row += '</button>';
    row += '</th>';
    row += '</tr>';
    row += '</thead>';
    row += '<tbody id="ExtraPhoneDataBody">';
    row += '<tr id="0">';
    row += ' <td style="display:none" id="PhoneListId" class="PhoneListId">0</td>';
    row += '<td class="additionalPhoneType">';
    row += '<div class="" style="width:100px">';
    row += '<select class="custom-select Gradient-inpuHeight ddlPhonePlus select" style="width:100%" id="ddlPhoneTypePlus" data-select2-id="9">' + phonePlusMaster + '</select>';
    row += '</div>';
    row += '</td>';
    row += '<td class="additionalPhone">';
    row += '<input type="text" class="form-control form-control-sm numericValidate phonePlus" style="width:230px" placeholder="Phone Number" id="txtPhoneNoPlus">';
    row += '</td>';
    row += '<td><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraPhoneRow(this)"></i></td>';
    row += '</tr>';
    row += '</tbody>';
    row += '</table>';
    row += '</div>';
    row += '</div>';
    row += '</div></div>';
    row += '</div>';
    $('#' + closestInfoTable).append(row);
    $('#' + closestInfoTable + ' .select').select2(); // Initialize Select2 for all dropdowns in the new contact info
}
















//function btnAddExtraEmails(data) {
//    var closest = data.closest('.ExtraEmailTableData');
//    var closestTable = closest.id;
//    var EmailTypeData = response.filter(item => item.Type == "PARTY-EMAIL-TYPE");
//    EmailType = '';
//    EmailType += '<option value=0>Select</option>';
//    for (var i = 0; i < EmailTypeData.length; i++) {
//        EmailType += '<option value=' + EmailTypeData[i].LookupId + '>' + EmailTypeData[i].Description + '</option>';
//    }
//    //var i = $(closest + ' tbody tr').length - 1;
//    var i = closest.tBodies.ExtraEmailDataBody.rows.length - 1;
//    var Newrow = "";
//    Newrow += "<tr id=" + i + ">";
//    Newrow += "<td class='additionalEmailType'><div style='width:100px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlEmailPlus" + i + "'>" + EmailType + "</select></div></td>";
//    Newrow += "<td class='additionalEmail'><input type='text' style='width:230px' class='form-control form-control-sm' placeholder='Email Id' id='txtEmailIdPlus" + i + "'></td>";
//    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraEmailRow(this)"></i></td>';
//    Newrow += '</tr>';
//    $('#' + closestTable + ' tr:last').before(Newrow);

//    $('.select').select2();
//    //$('.select2-selection').css('border-color', 'blue');
//};

function btnAddExtraEmails(data) {
    var closest = data.closest('.ExtraEmailTableData');
    var closestTable = closest.id;
    var EmailTypeData = response.filter(item => item.Type == "PARTY-EMAIL-TYPE");
    EmailType = '';
    EmailType += '<option value=0>Select</option>';
    for (var i = 0; i < EmailTypeData.length; i++) {
        EmailType += '<option value=' + EmailTypeData[i].LookupId + '>' + EmailTypeData[i].Description + '</option>';
    }
    var i = closest.tBodies.ExtraEmailDataBody.rows.length - 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td style='display:none' class='EmailListId'>0</td>";
    Newrow += "<td class='additionalEmailType'><div style='width:100px'><select class='custom-select Gradient-inpuHeight ddlEmailPlus select' style='width:100%' id='ddlEmailPlus" + i + "'>" + EmailType + "</select></div></td>";
    Newrow += "<td class='additionalEmail'><input type='text' style='width:230px' class='form-control form-control-sm emailPlus' placeholder='Email Id' id='txtEmailIdPlus" + i + "'></td>";
    Newrow += '<td><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraEmailRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#' + closestTable + ' tr:last').before(Newrow);
    $('#' + closestTable + ' .select').select2(); // Initialize Select2 for new dropdown
}













function RemoveExtraEmailRow(data) {
    var row = $(data).closest("TR");
    if (confirm("Do you want to delete")) {
        row.remove();
    }
};













//function btnAddExtraPhone(data) {
//    var closest = data.closest('.ExtraPhoneTableData');
//    var closestTable = closest.id;
//    var PhoneTypeData = response.filter(item => item.Type == "PARTY-PHONE-TYPE");
//    PhoneType = '';
//    PhoneType += '<option value=0>Select</option>';
//    for (var i = 0; i < PhoneTypeData.length; i++) {
//        PhoneType += '<option value=' + PhoneTypeData[i].LookupId + '>' + PhoneTypeData[i].Description + '</option>';
//    }
//    //var i = $('#ExtraPhoneTableData tbody tr').length - 1;
//    var i = closest.tBodies.ExtraPhoneDataBody.rows.length - 1;
//    var Newrow = "";
//    Newrow += "<tr id=" + i + ">";
//    Newrow += "<td class='additionalPhoneType'><div style='width:100px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlPhoneTypePlus" + i + "'>" + PhoneType + "</select></div></td>";
//    Newrow += "<td class='additionalPhone'><input type='text' class='form-control form-control-sm numericValidate' style='width:230px' placeholder='Phone Number' id='txtPhoneNoPlus" + i + "'></td>";
//    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraPhoneRow(this)"></i></td>';
//    Newrow += '</tr>';
//    $('#' + closestTable + ' tr:last').before(Newrow);
//    $('.select').select2();
//    //$('.select2-selection').css('border-color', 'blue');
//};


function btnAddExtraPhone(data) {
    var closest = data.closest('.ExtraPhoneTableData');
    var closestTable = closest.id;
    var PhoneTypeData = response.filter(item => item.Type == "PARTY-PHONE-TYPE");
    PhoneType = '';
    PhoneType += '<option value=0>Select</option>';
    for (var i = 0; i < PhoneTypeData.length; i++) {
        PhoneType += '<option value=' + PhoneTypeData[i].LookupId + '>' + PhoneTypeData[i].Description + '</option>';
    }
    var i = closest.tBodies.ExtraPhoneDataBody.rows.length - 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td style='display:none' class='PhoneListId'>0</td>";
    Newrow += "<td class='additionalPhoneType'><div style='width:100px'><select class='custom-select Gradient-inpuHeight ddlPhonePlus select' style='width:100%' id='ddlPhoneTypePlus" + i + "'>" + PhoneType + "</select></div></td>";
    Newrow += "<td class='additionalPhone'><input type='text' style='width:230px' class='form-control form-control-sm numericValidate phonePlus' placeholder='Phone Number' id='txtPhoneNoPlus" + i + "'></td>";
    Newrow += '<td><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraPhoneRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#' + closestTable + ' tr:last').before(Newrow);
    $('#' + closestTable + ' .select').select2(); // Initialize Select2 for new dropdown
}










function RemoveExtraPhoneRow(data) {
    var row = $(data).closest("TR");
    if (confirm("Do you want to delete")) {
        //var table = $("#ExtraPhoneTableData")[0];
        //table.deleteRow(row[0].rowIndex);
        row.remove();
    }
};
function GetDetails() {

    ShowProgress();
    var obj = {};
    $.ajax({
        url: "/Contact/ContactDetails/GetDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Get",
        success: function (result) {
            if (result == null) {
                //toastr.error(NoRecordMessage());
                NoRecordFound();
                var permissions = JSON.parse($('#hiddenPermission').val());
                if (permissions.Contact.IsAdd) {
                    $('#dvAddButton').show();
                }
                HideProgress();
            }
            else {
                var permissions = JSON.parse($('#hiddenPermission').val());
                if (permissions.Contact != null) {
                    if (permissions.Contact.IsView) {
                        bindData(result);
                    }
                }
                else {
                    //toastr.error(NotPermission());
                    NoPermission();
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
function DeleteConfirmation(Id) {
    $("#txtPartyId").val(Id);
    $('#ConfirmBox').modal('show');
}
function DeleteContact(Id) {
    var Id = $("#txtPartyId").val();
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Contact/ContactDetails/DeleteDetails/" + Id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: { id: Id },
            type: "Delete",
            success: function (result) {
                if (result.ErrorCodes != null) {
                    //toastr.error(ErrorCodes(result.ErrorCodes));
                    SweetErrorMessage(result.ErrorCodes);
                    $('#ConfirmBox').modal('hide');
                    HideProgress();
                }
                else {
                    //toastr.success(DeleteMessage());
                    SweetDeleteMessage();
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
        //toastr.error('No details found for this record.');
        NoDataForId();
        HideProgress();
    }
}





function bindData(result) {
    $('#tblContactData thead').html('');
    $('#tblContactData tbody').html('');
    var permissions = JSON.parse($('#hiddenPermission').val());

    if (permissions.Contact.IsAdd) {
        $('#dvAddButton').show();
    }

    if (result.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th style='width: 55px;'>Sr.No.</th>";  // Adjusted the header for Sr.No.
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblContactData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Company Name : activate to sort column descending'> Company Name</th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblContactData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Manufacturer : activate to sort column descending'> Manufacturer</th>";
        //thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblContactData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='IsActive : activate to sort column descending'> IsActive</th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblContactData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='IsActive : activate to sort column descending'> Updated By</th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblContactData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='IsActive : activate to sort column descending'> Updated On</th>";

        if (permissions.Contact.IsEdit || permissions.Contact.IsDeleted || permissions.Contact.IsAddAddress) {
            thead += "<th> Action </th>";
        }

        thead += "</tr>";
        $('#tblContactData thead').append(thead);

        var display = permissions.Contact.IsEdit == true ? "inline" : "none";
        var displayDel = permissions.Contact.IsDeleted == true ? "inline" : "none";
        var displayAdd = permissions.Contact.IsAddAddress == true ? "inline" : "none";

        var row = '';
        for (var i = 0; i < result.length; i++) {
            var updatedBy = result[i].UserDetailsUpdatedBy == null ? "" : result[i].UserDetailsUpdatedBy?.UserName;
            var updatedOn = result[i].Party_DOU == null ? "" : result[i].Party_DOU;
            row += "<tr role='row'>";
            row += "<td class='sorting_1' id='ContactId" + result[i].Party_Id + "' style='display:none'>" + result[i].Party_Id + "</td>";
            row += "<td></td>";  // Placeholder for Sr.No.
            row += "<td>" + result[i].Party_Name + "</td>";
            row += "<td>" + result[i].Manufacturer_Type + "</td>";
            //row += "<td><input type='checkBox' checked=" + result[i].IsActive + " disabled /></td>";
            row += "<td>" + updatedBy + "</td>";
            row += "<td>" + ConvertDateDDMMYYYY(updatedOn) + "</td>";

            if (permissions.Contact.IsEdit || permissions.Contact.IsDeleted || permissions.Contact.IsAddAddress) {
                row += "<td><a   title='Edit Contact' href='/Contact/EditContact/Index?id=" + result[i].Party_Id + "')><i class='fas fa-edit' style='font-size:20px;color:#902ca8;display:" + display + "'></i></a> &nbsp;<a  title='Delete Contact'  onclick=DeleteConfirmation(" + result[i].Party_Id + ")><i class='far fa-trash-alt' style='font-size:20px;color:red;display:" + displayDel + "'></i></a></td>";
            }
            row += "</tr>";
        }

        HideProgress();

        // Clear and destroy any previous DataTable instance
        if ($.fn.DataTable.isDataTable('#tblContactData')) {
            $('#tblContactData').DataTable().clear().destroy();
        }

        // Append rows to the table body
        $('#tblContactData tbody').append(row);

        // Initialize DataTable with Sr.No. handling
        var table = $('#tblContactData').DataTable({
            "order": [],
            "columnDefs": [{
                "targets": 1, // Target the Sr.No. column
                "searchable": false,
                "orderable": false,
                "render": function (data, type, row, meta) {
                    return meta.row + 1; // Dynamically display Sr.No.
                }
            }]
        });

        // Update Sr.No. after sorting/filtering
        table.on('draw', function () {
            table.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1; // Update Sr.No. dynamically after sorting/filtering
            });
        });
    } else {
        HideProgress();
    }
}









function RefreshAddressModal() {
    $('#ddlAddressType').val('0');
    $('#txtAddressLine1').val('');
    $('#txtAddressLine2').val('');
    $('#ddlCity').val('0');
    $('#ddlState').val('0');
    $('#ddlCountry').val('0');
    $('#txtPIN').val('');
    $('#txtAddressEmail').val('');
    $('#txtAddressContact').val('');
    $('#txtAddressGST').val('');
    $('#ddlRep').val('0');
    $('#txtRemarks').val('');
    $('#txtZone').val('');
    //$('#chkIsSupplier')[0].checked = false;

}




let AllStates = []; // Store all states globally
let AllCities = [];

function bindMasters() {
    if ($('#hdnLookup').val() != null && $('#hdnLookup').val() != '') {
        response = JSON.parse($('#hdnLookup').val());

        let City = response.filter(item => item.Type == "CITY-TYPE");
        let State = response.filter(item => item.Type == "STATE-TYPE");
        let Country = response.filter(item => item.Type == "COUNTRY-TYPE");
        let AddressType = response.filter(item => item.Type == "PARTY-ADDRESS-TYPE");

        if (City.length > 0) {
            $("#ddlCity").html('<option value=0>Select</option>');
            $("#ddlCity").prop('disabled', true);
            AllCities = City; // Just store, don't populate initially
        }

        if (State.length > 0) {
            $("#ddlState").html('<option value=0>Select</option>');
            $("#ddlState").prop('disabled', true); // Disable initially
            AllStates = State; // Just store for filtering later
        }



        if (Country.length > 0) {
            $("#ddlCountry").html('');
            var country = '';
            $("#ddlCountry").append('<option value=0>Select</option>');
            country += '<option value=0>-Select-</option>';
            for (var i = 0; i < Country.length; i++) {
                $("#ddlCountry").append('<option value=' + Country[i].LookupId + '>' + Country[i].Description + '</option>');
                country += '<option value=' + Country[i].LookupId + '>' + Country[i].Description + '</option>';
            }
            CountryMaster = country;
        }

        if (AddressType.length > 0) {
            $("#ddlAddressType").html('');
            var addressType = '';
            $("#ddlAddressType").append('<option value=0>Select</option>');
            addressType += '<option value=0>-Select-</option>';
            for (var i = 0; i < AddressType.length; i++) {
                $("#ddlAddressType").append('<option value=' + AddressType[i].LookupId + '>' + AddressType[i].Description + '</option>');
                addressType += '<option value=' + AddressType[i].LookupId + '>' + AddressType[i].Description + '</option>';
            }
            AddresstypeMaster = addressType;
        }
    }

    if ($('#hdnRepresentatives').val() != null && $('#hdnRepresentatives').val() != '') {
        $("#ddlRep").html('');
        var rep = '';
        var res = JSON.parse($('#hdnRepresentatives').val());
        $("#ddlRep").append('<option value=0>Select</option>');
        rep += '<option value=0>-Select-</option>';
        for (var i = 0; i < res.length; i++) {
            $("#ddlRep").append('<option value=' + res[i].Rep_ID + '>' + res[i].Rep_Name + '</option>');
            rep += '<option value=' + res[i].Rep_ID + '>' + res[i].Rep_Name + '</option>';
        }
        repMaster = rep;
    }
}


// Delegate change event for country dropdown
$(document).on('change', '.ddlCountry', function () {
    const selectedCountryId = $(this).val();
    const $stateDropdown = $(this).closest('.dyn').find('.ddlState');
    const $cityDropdown = $(this).closest('.dyn').find('.ddlCity');

    // Filter states based on selected country
    const matchedStates = AllStates.filter(state => state.LookupParentID == selectedCountryId);

    $stateDropdown.html('<option value="0">Select</option>');
    $cityDropdown.html('<option value="0">Select</option>').prop('disabled', true);

    if (matchedStates.length > 0) {
        matchedStates.forEach(state => {
            $stateDropdown.append(`<option value="${state.LookupId}">${state.Description}</option>`);
        });
        $stateDropdown.prop('disabled', false);
    } else {
        $stateDropdown.prop('disabled', true);
    }
});

// Delegate change event for state dropdown
$(document).on('change', '.ddlState', function () {
    const selectedStateId = $(this).val();
    const $cityDropdown = $(this).closest('.dyn').find('.ddlCity');

    // Filter cities based on selected state
    const matchedCities = AllCities.filter(city => city.LookupParentID == selectedStateId);

    $cityDropdown.html('<option value="0">Select</option>');

    if (matchedCities.length > 0) {
        matchedCities.forEach(city => {
            $cityDropdown.append(`<option value="${city.LookupId}">${city.Description}</option>`);
        });
        $cityDropdown.prop('disabled', false);
    } else {
        $cityDropdown.prop('disabled', true);
    }
});






function bindPhoneEmailLookup() {
    if ($('#hdnLookup').val() != null && $('#hdnLookup').val() != '') {
        response = JSON.parse($('#hdnLookup').val());
        let PhoneType = response.filter(item => item.Type == "PARTY-PHONE-TYPE");
        let EmailType = response.filter(item => item.Type == "PARTY-EMAIL-TYPE");
        if (PhoneType.length > 0) {
            $("#ddlPhoneType").html('');
            $("#ddlPhoneTypePlus").html('');
            var phonePlus = '';
            $("#ddlPhoneType").append('<option value=0>Select</option>');
            for (var i = 0; i < PhoneType.length; i++) {
                $("#ddlPhoneType").append('<option value=' + PhoneType[i].LookupId + '>' + PhoneType[i].Description + '</option>');
            }
            $("#ddlPhoneTypePlus").append('<option value=0>Select</option>');
            phonePlus += '<option value=0>Select</option>';
            for (var i = 0; i < PhoneType.length; i++) {
                $("#ddlPhoneTypePlus").append('<option value=' + PhoneType[i].LookupId + '>' + PhoneType[i].Description + '</option>');
                phonePlus += '<option value=' + PhoneType[i].LookupId + '>' + PhoneType[i].Description + '</option>';
            }
            phonePlusMaster = phonePlus;
        }
        if (EmailType.length > 0) {
            $("#ddlEmail").html('');
            $("#ddlEmailPlus").html('');
            var emailPlus = '';
            $("#ddlEmail").append('<option value=0>Select</option>');
            emailPlus += '<option value=0>Select</option>';
            for (var i = 0; i < EmailType.length; i++) {
                $("#ddlEmail").append('<option value=' + EmailType[i].LookupId + '>' + EmailType[i].Description + '</option>');
            }
            $("#ddlEmailPlus").append('<option value=0>Select</option>');
            for (var i = 0; i < EmailType.length; i++) {
                $("#ddlEmailPlus").append('<option value=' + EmailType[i].LookupId + '>' + EmailType[i].Description + '</option>');
                emailPlus += '<option value=' + EmailType[i].LookupId + '>' + EmailType[i].Description + '</option>';
            }
            emailPlusMaster = emailPlus;
        }
    }
}
//////////////------EDIT-------///////////// 
function EditContact(Id) {
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Contact/EditContact/GetDetailsById",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { id: Id },
            type: "Get",
            success: function (result) {
                $('#txtPartyId').val(result.Party_Id);
                $('#txtCompanyName').val(result.Party_Name);
                $('#txtManufacturer').val(result.Manufacturer_Type);
                result.PartyEmails.forEach(function (item, index) {
                    if (index == 0) {
                        $('#PartyEmail_Id').html(item.PartyEmail_Id);
                        $('#ddlEmail').val(item.Email_Type);
                        $('#txtEmailId').val(item.Email);
                    }
                    else {
                        btnAddEmailsWithData(item);
                    }
                });
                result.PartyPhone.forEach(function (item, index) {
                    if (index == 0) {
                        $('#PartyPhone_Id').html(item.PartyPhone_Id);
                        $('#ddlPhoneType').val(item.Phone_Type);
                        $('#txtPhoneNo').val(item.Phone_Number);
                    }
                    else {
                        btnAddPhoneWithData(item);
                    }
                });


                bindEditAddressAccordion(result.PartyAddress);
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
        //toastr.error('No details found for this record.');
        NoDataForId();
        HideProgress();
    }
}
function btnAddEmailsWithData(item) {
    var EmailTypeData = response.filter(item => item.Type == "PARTY-EMAIL-TYPE");
    EmailType = '';
    EmailType += '<option value=0>Select</option>';
    for (var i = 0; i < EmailTypeData.length; i++) {
        EmailType += '<option value=' + EmailTypeData[i].LookupId + '>' + EmailTypeData[i].Description + '</option>';
    }
    var i = $('#EmailTableData tbody tr').length + 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td  style='display:none' class='PartyEmail_Id'>" + item.PartyEmail_Id + "</td>";
    Newrow += "<td class='firstEmailType'><div style='width:200px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlEmail" + i + "'>" + EmailType + "</select></div></td>";
    Newrow += "<td class='firstEmail'><input type='text' class='form-control form-control-sm ' placeholder='Email Id' id='txtEmailId" + i + "' value=" + item.Email + "></td>";
    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveEmailRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#EmailDataBody tr:last').before(Newrow);
    $('#ddlEmail' + i).val(item.Email_Type);
    //$('.select').select2();
    //$('.select2-selection').css('border-color', 'blue');
}
function btnAddPhoneWithData(item) {
    var PhoneTypeData = response.filter(item => item.Type == "PARTY-PHONE-TYPE");
    PhoneType = '';
    PhoneType += '<option value=0>Select</option>';
    for (var i = 0; i < PhoneTypeData.length; i++) {
        PhoneType += '<option value=' + PhoneTypeData[i].LookupId + '>' + PhoneTypeData[i].Description + '</option>';
    }
    var i = $('#PhoneTableData tbody tr').length - 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td  style='display:none' class='PartyPhone_Id'>" + item.PartyPhone_Id + "</td>";
    Newrow += "<td class='firstPhoneType''><div style='width:200px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlPhoneType" + i + "'>" + PhoneType + "</select></div></td>";
    Newrow += "<td class='firstPhone'><input type='text' class='form-control form-control-sm numericValidate' placeholder='Phone Number' id='txtPhoneNo" + i + "' value=" + item.Phone_Number + "></td>";
    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemovePhoneRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#PhoneDataBody tr:last').before(Newrow);
    $('#ddlPhoneType' + i).val(item.Phone_Type);
};
function btnAddExtraEmailsWithData(item) {
    var EmailTypeData = response.filter(item => item.Type == "PARTY-EMAIL-TYPE");
    EmailType = '';
    EmailType += '<option value=0>Select</option>';
    for (var i = 0; i < EmailTypeData.length; i++) {
        EmailType += '<option value=' + EmailTypeData[i].LookupId + '>' + EmailTypeData[i].Description + '</option>';
    }

    var i = $('#ExtraEmailTableData tbody tr').length - 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td class='additionalEmailType'><div style='width:200px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlEmailPlus" + i + "'>" + EmailType + "</select></div></td>";
    Newrow += "<td class='additionalEmail'><input type='text' class='form-control form-control-sm' placeholder='Email Id' id='txtEmailIdPlus" + i + "' value=" + item.Email + "></td>";
    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraEmailRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#ExtraEmailDataBody tr:last').before(Newrow);
    $('#ddlEmailPlus' + i).val(item.Email_Type);
};
function btnAddExtraPhoneWithData(item) {
    var PhoneTypeData = response.filter(item => item.Type == "PARTY-PHONE-TYPE");
    PhoneType = '';
    PhoneType += '<option value=0>Select</option>';
    for (var i = 0; i < PhoneTypeData.length; i++) {
        PhoneType += '<option value=' + PhoneTypeData[i].LookupId + '>' + PhoneTypeData[i].Description + '</option>';
    }
    var i = $('#ExtraPhoneTableData tbody tr').length - 1;
    var Newrow = "";
    Newrow += "<tr id=" + i + ">";
    Newrow += "<td ><div style='width:200px''><select class='custom-select Gradient-inpuHeight' style='width:100%' id='ddlPhoneTypePlus" + i + "'>" + PhoneType + "</select></div></td>";
    Newrow += "<td><input type='text' class='form-control form-control-sm numericValidate' placeholder='Phone Number' id='txtPhoneNoPlus" + i + "' value=" + item.PhoneNumber + "></td>";
    Newrow += '<td ><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraPhoneRow(this)"></i></td>';
    Newrow += '</tr>';
    $('#ExtraPhoneDataBody tr:last').before(Newrow);
    $('#ddlPhoneTypePlus' + i).val(item.Phone_Type);
};
function bindEditAddressTable(item) {
    var accordionLength = $('.accordion').length;
    var newAccordionId = parseInt(accordionLength) + parseInt(1);
    var row = '';
    row += ' <div id="accordion" class="accordion">';
    row += ' <div class="card card-primary">';
    row += '<div class="card-header" style="background-color:#902ca8">';
    row += '<h4 class="card-title">';
    row += '<a data-toggle="collapse" data-parent="#accordion" href="#collapse' + newAccordionId + '">New Address</a>';
    row += '</h4>';
    row += '<i class="far fa-times-circle" style="cursor:pointer;float:right" onclick="RemoveAccBox(this)"></i>';
    row += '</div>';
    row += '<div id="collapse' + newAccordionId + '" class="panel-collapse collapse in">';
    row += '<div class="card-body">';
    row += '<div class="row dyn">';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Address Type</label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlAddressType" style="width:100%" id="ddlAddressType">' + AddresstypeMaster + '</select>';
    row += '</div> </div>';
    row += '<div class="col-lg-10 ">';
    row += '<div class="form-group">';
    row += '<label>Address Line 1 <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm address1" value="' + item.Address_Line1 + '" placeholder="Address Line 1" id="txtAddressLine1">';
    row += '</div></div>';
    row += ' <div class="col-lg-12">';
    row += ' <div class="form-group">';
    row += '<label>Address Line 2</label>';
    row += '<input type="text" class="form-control form-control-sm address2" value="' + item.Address_Line2 + '" placeholder="Address Line 2" id="txtAddressLine2">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Country <span style="color:#dc3545">*</span></label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlCountry" style="width:100%" id="ddlCountry">' + CountryMaster + '</select>';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>State <span style="color:#dc3545">*</span></label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlState" style="width:100%" id="ddlState">' + StateMaster + '</select>';
    row += '</div></div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>City <span style="color:#dc3545">*</span></label>';
    row += '<select class="custom-select Gradient-inpuHeight ddlCity" style="width:100%" id="ddlCity">' + CityMaster + '</select>';
    row += '</div></div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>PIN </label>';
    row += '<input type="text" class="form-control form-control-sm pin" value="' + item.Pincode + '" placeholder="PIN" id="txtPIN">';
    row += ' </div> </div>';
    row += '<div class="col-lg-4">';
    row += '<div class="form-group">';
    row += '<label>Email</label>';
    row += '<input type="text" class="form-control form-control-sm email" value="' + item.Contact_Email + '" placeholder="Email Id" id="txtAddressEmail">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Contact No</label>';
    row += '<input type="text" class="form-control form-control-sm numericValidate contactNo" value="' + item.Contact_Phone + '" placeholder="Contact No" id="txtAddressContact">';
    row += ' </div> </div>';
    row += '<div class="col-lg-4">';
    row += '<div class="form-group">';
    row += '<label>GST No</label>';
    row += ' <input type="text" class="form-control form-control-sm gst" value="' + item.GST + '" placeholder="GST No" id="txtAddressGST">';
    row += '</div></div>';
    row += '<div class="col-lg-4">';
    row += '<div class="form-group">';
    row += '<label>Representatives  </label>';
    row += '<select class="select  Gradient-inpuHeight ddlRep" style="width:100%" data-select2-id="7" id="ddlRep">' + repMaster + '</select>';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Zone</label>';
    row += '<input type="text" class="form-control form-control-sm zone" value="' + item.Zone + '" placeholder="Zone" id="txtZone">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Is Supplier Address </label>';
    row += '<input type="checkbox" class="form-control isChk" style="width:15px" id="chkIsSupplier" />';
    row += '</div></div>';
    row += '<div class="col-lg-10">';
    row += '<div class="form-group">';
    row += '<label>Remarks <span style="color:#dc3545">*</span></label>';
    row += '<textarea type="text" class="form-control remarks" placeholder="Enter ..." value="' + item.Contact_Remark + '" id="txtRemarks" rows="3"></textarea>';
    row += '</div></div>';
    row += '<h5 style="color:lightblue">Contact Info:</h5>';
    row += '<hr />';
    row += ' <div class="row">';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>Title</label>';
    row += '<select id="ddlTitle" class="form-control form-control-sm ddlTitle">';
    row += ' <option value="Mr.">Mr.</option><option value="Ms.">Ms.</option>';
    row += ' </select>';
    row += ' </div> </div>';
    row += ' <div class="col-lg-2">';
    row += '<div class="form-group">';
    row += ' <label>Contact Person <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm contactPerson" value="' + item.Contact[0].ContactPersonName + '" placeholder="Contact Person" id="txtContactPerson">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += ' <div class="form-group">';
    row += '<label>Designation <span style="color:#dc3545">*</span></label>';
    row += '<input type="text" class="form-control form-control-sm designation"  value="' + item.Contact[0].Designation + '" placeholder="Designation" id="txtDesignation">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += ' <div class="form-group">';
    row += ' <label>Date Of Birth <span style="color:#dc3545">*</span></label>';
    //row += '<input type="text" class="form-control form-control-sm textbox-n" onfocus=(this.type="date") onblur=(this.type="text") name="DOB" placeholder="Date" id="txtDOB">';
    row += '<input type="text" class="form-control form-control-sm dob"  value="' + item.Contact[0].DateOfBirth + '" placeholder="DD/MM/YYYY" id="txtDOB">';
    row += '</div></div>';
    row += '<div class="col-lg-2">';
    row += '<div class="form-group">';
    row += '<label>Date Of Anniversary <span style="color:#dc3545">*</span></label>';
    row += ' <input type="text" class="form-control form-control-sm doa"  value="' + item.Contact[0].DateOfAnniversary + '"  placeholder="DD/MM/YYYY" id="txtAnniversary">';
    row += '</div></div>';
    row += '<h1></h1>';
    row += '<div style="overflow:auto;width:50%">';
    row += '<table class="table table-bordered GradientdataTable" id="ExtraEmailTableData">';
    row += '<thead>';
    row += '<tr>';
    row += '<th style="width:15%">E-Mail Type</th><th>E-Mail</th>';
    row += ' <th style="width:5%">';
    row += '<button type="button" class="btn btn-tool" title="Add Row">';
    row += '<i class="fas fa-plus-circle" id="btnAddExtraEmails" onclick="btnAddExtraEmails()" style="float:right;cursor: pointer;color:purple"></i>';
    row += '</button>';
    row += '</th> </tr>';
    row += '</thead>';
    row += ' <tbody id="ExtraEmailDataBody">';
    for (var j = 0; j < item.Contact[0].emailListModels; j++) {
        row += '<tr id="0">';
        row += ' <td class="additionalEmailType">';
        row += '<div class="" style="width:200px">';
        row += '<select class="custom-select Gradient-inpuHeight ddlEmailPlus" style="width:100%" id="ddlEmailPlus" data-select2-id="8">' + emailPlusMaster + '</select>';
        row += '</div>';
        row += '</td>';
        row += '<td class="additionalEmail">';
        row += '<input type="text" class="form-control form-control-sm emailPlus" placeholder="Email Id" id="txtEmailIdPlus">';
        row += '</td>';
        row += '<td></td>';
        row += '</tr>';
    }
    row += '</tbody></table> </div>';
    row += '<div style="overflow:auto;padding-left:2px;width:50%">';
    row += '<table class="table table-bordered GradientdataTable" id="ExtraPhoneTableData">';
    row += '<thead>';
    row += '<tr>';
    row += ' <th style="width:15%">Phone Type</th>';
    row += '<th>Phone Number</th>';
    row += ' <th style="width:5%">';
    row += '<button type="button" class="btn btn-tool" title="Add Row">';
    row += '<i class="fas fa-plus-circle numbers" id="btnAddExtraPhone" onclick="btnAddExtraPhone()" style="float:right;cursor: pointer;color:purple;"></i>';
    row += '</button>';
    row += '</th>';
    row += '</tr>';
    row += '</thead>';
    row += '<tbody id="ExtraPhoneDataBody">';
    for (var j = 0; j < item.Contact[0].emailListModels; j++) {
        row += '<tr id="0">';
        row += '<td class="additionalPhoneType">';
        row += '<div class="" style="width:200px">';
        row += '<select class="custom-select Gradient-inpuHeight ddlPhonePlus" style="width:100%" id="ddlPhoneTypePlus" data-select2-id="9">' + phonePlusMaster + '</select>';
        row += '</div>';
        row += '</td>';
        row += '<td class="additionalPhone">';
        row += '<input type="text" class="form-control form-control-sm numericValidate phonePlus" placeholder="Phone Number" id="txtPhoneNoPlus">';
        row += '</td>';
        row += '<td></td>';
        row += '</tr>';
    }
    row += '</tbody>';
    row += ' </table>';
    row += '</div>';
    row += '</div>';
    row += '</div></div>';
    row += '</div>';
    row += '</div>';
    row += '</div>';
    row += '</div>';
    $('#divAccordionParent').append(row);
    $('.select').select2();
}

function bindEditAddressAccordion(data) {
    if (data.length > 0) {
        data.forEach(function (item) {
            var accordionLength = $('.accordion').length;
            var newAccordionId = parseInt(accordionLength) + parseInt(1);
            var contactInfoLength = $('.contactInfoExtra').length;
            var newInfoId = parseInt(contactInfoLength) + parseInt(1);
            var extraEmailTable = $('.ExtraEmailTableData').length;
            var countEmailtable = parseInt(extraEmailTable) + parseInt(1);
            var extraphoneTable = $('.ExtraPhoneTableData').length;
            var countPhntable = parseInt(extraphoneTable) + parseInt(1);
            var row = '';

            row += ' <div id="accordion" class="accordion">';
            row += ' <div class="card card-primary">';
            row += '<div class="card-header" style="background-color:#902ca8">';
            row += '<h4 class="card-title">';
            row += '<a data-toggle="collapse" data-parent="#accordion" href="#collapse' + newAccordionId + '">' + item.FullAddress + '</a>';
            row += '</h4>';
            row += '<i class="far fa-times-circle" style="cursor:pointer;float:right" onclick="RemoveAccBox(this)"></i>';
            row += '</div>';
            row += '<div id="collapse' + newAccordionId + '" class="panel-collapse collapse in">';
            row += '<div class="card-body">';
            row += '<div class="row dyn" id="dynInfoContact' + newAccordionId + '">';
            row += ' <div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>Address Type</label>';
            var addressTypeRow = '<select class="custom-select Gradient-inpuHeight ddlAddressType" style="width:100%" id="ddlAddressType">' + AddresstypeMaster + '</select>';
            addressTypeRow = addressTypeRow.replace('value=' + item.Address_Type + '', 'value=' + item.Address_Type + ' selected');
            row += addressTypeRow;
            row += '</div> </div>';
            row += '<div class="col-lg-10 ">';
            row += '<div class="form-group">';
            row += '<label>Address Line 1 <span style="color:#dc3545">*</span></label>';
            row += '<input type="text" class="form-control form-control-sm address1" placeholder="Address Line 1" id="txtAddressLine1" value="' + item.Address_Line1 + '">';
            row += '<input type="text" style="display:none" class="PartyAddress_Id" id="PartyAddress_Id" value="' + item.PartyAddress_Id + '">';
            row += '</div></div>';
            row += ' <div class="col-lg-12">';
            row += ' <div class="form-group">';
            row += '<label>Address Line 2</label>';
            row += '<input type="text" class="form-control form-control-sm address2" placeholder="Address Line 2" id="txtAddressLine2" value="' + item.Address_Line2 + '">';
            row += '</div></div>';
            row += '<div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>Country <span style="color:#dc3545">*</span></label>';
            var countryRow = '<select class="custom-select Gradient-inpuHeight ddlCountry" style="width:100%" id="ddlCountry">' + CountryMaster + '</select>';
            countryRow = countryRow.replace('value=' + item.CountryId + '', 'value=' + item.CountryId + ' selected');
            row += countryRow;
            row += '</div></div>';
            row += '<div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>State <span style="color:#dc3545">*</span></label>';

            // Dynamically populate states based on CountryId
            var matchedStates = AllStates.filter(state => state.LookupParentID == item.CountryId);
            var stateOptions = '<option value="0">Select</option>';
            matchedStates.forEach(state => {
                stateOptions += `<option value="${state.LookupId}" ${state.LookupId == item.StateId ? 'selected' : ''}>${state.Description}</option>`;
            });
            row += '<select class="custom-select Gradient-inpuHeight ddlState" style="width:100%" id="ddlState">' + stateOptions + '</select>';
            row += '</div></div>';
            row += ' <div class="col-lg-2">';
            row += '<div class="form-group">';
            row += ' <label>City <span style="color:#dc3545">*</span></label>';

            // Dynamically populate cities based on StateId
            var matchedCities = AllCities.filter(city => city.LookupParentID == item.StateId);
            var cityOptions = '<option value="0">Select</option>';
            matchedCities.forEach(city => {
                cityOptions += `<option value="${city.LookupId}" ${city.LookupId == item.CityId ? 'selected' : ''}>${city.Description}</option>`;
            });
            row += '<select class="custom-select Gradient-inpuHeight ddlCity" style="width:100%" id="ddlCity">' + cityOptions + '</select>';
            row += '</div></div>';
            row += ' <div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>PIN </label>';
            row += '<input type="text" class="form-control form-control-sm pin" placeholder="PIN" id="txtPIN" value="' + item.Pincode + '">';
            row += ' </div> </div>';
            row += '<div class="col-lg-4">';
            row += '<div class="form-group">';
            row += '<label>Email</label>';
            row += '<input type="text" class="form-control form-control-sm email" placeholder="Email Id" id="txtAddressEmail" value="' + item.Contact_Email + '">';
            row += '</div></div>';
            row += '<div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>Contact No</label>';
            row += '<input type="text" class="form-control form-control-sm numericValidate contactNo" placeholder="Contact No" id="txtAddressContact" value="' + item.Contact_Phone + '">';
            row += ' </div> </div>';
            row += '<div class="col-lg-4">';
            row += '<div class="form-group">';
            row += '<label>GST No</label>';
            row += ' <input type="text" class="form-control form-control-sm gst" placeholder="GST No" id="txtAddressGST" value="' + item.GST + '">';
            row += '</div></div>';
            row += '<div class="col-lg-4">';
            row += '<div class="form-group">';
            row += '<label>Representatives <span style="color:#dc3545">*</span></label>';
            var repRow = '<select class="select Gradient-inpuHeight ddlRep" style="width:100%" data-select2-id="7" id="ddlRep">' + repMaster + '</select>';
            repRow = repRow.replace('value=' + item.Representative_Id + '', 'value=' + item.Representative_Id + ' selected');
            row += repRow;
            row += '</div></div>';
            row += '<div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>Zone</label>';
            row += '<input type="text" class="form-control form-control-sm zone" placeholder="Zone" id="txtZone" value="' + item.Zone + '">';
            row += '</div></div>';
            row += '<div class="col-lg-2">';
            row += '<div class="form-group">';
            row += '<label>Is Supplier Address </label>';
            row += '<input type="checkbox" class="form-control isChk" style="width:15px" id="chkIsSupplier" ' + (item.Supplier_Address ? 'checked' : '') + ' />';
            row += '</div></div>';
            row += '<div class="col-lg-10">';
            row += '<div class="form-group">';
            row += '<label>Remarks </label>';
            row += '<textarea type="text" class="form-control remarks" placeholder="Enter ..." id="txtRemarks" rows="3">' + item.Contact_Remark + '</textarea>';
            row += '</div></div>';

            // Contact Info
            item.Contact.forEach(function (cont, i) {
                row += '<div id="contactInfoExtra' + newInfoId + '" class="contactInfoExtra">';
                row += '<div class="card">';
                row += '<div class="card-header SubDetailsPageHeader">';
                row += '<h5 class="card-title DetailsTitle">CONTACT INFO</h5>';
                row += '<div class="card-tools">';
                row += '<button type="button" class="btn btn-tool" title="Add new">';
                row += '<i class="fas fa-plus-circle" id="btnAddContactInfo" onclick="btnAddContactInfo(this)" style="float:right;cursor: pointer;color:#adb5bd"></i></button>';
                row += '</div>';
                row += '</div>';
                row += '<div class="card-body Gradientcard-outline">';
                row += ' <div class="row">';
                row += '<div class="col-lg-2">';
                row += '<div class="form-group">';
                row += ' <label>Title</label>';
                row += '<select id="ddlTitle" class="form-control form-control-sm ddlTitle">';
                row += '<option value="Mr."' + ("Mr." == cont.ContactTitle ? ' selected' : '') + '>Mr.</option>';
                row += '<option value="Ms."' + ("Ms." == cont.ContactTitle ? ' selected' : '') + '>Ms.</option>';
                row += ' </select>';
                row += ' </div> </div>';
                row += ' <div class="col-lg-2">';
                row += '<div class="form-group">';
                row += ' <label>Contact Person <span style="color:#dc3545">*</span></label>';
                row += '<input type="text" class="form-control form-control-sm contactPerson txtContactPerson" placeholder="Contact Person" id="txtContactPerson" value="' + cont.ContactPersonName + '">';
                row += '<input type="text" style="display:none" class="Contact_Id" id="Contact_Id" value="' + cont.Contact_Id + '">';
                row += '</div></div>';
                row += '<div class="col-lg-2">';
                row += ' <div class="form-group">';
                row += '<label>Designation <span style="color:#dc3545">*</span></label>';
                row += '<input type="text" class="form-control form-control-sm designation txtDesignation" placeholder="Designation" id="txtDesignation" value="' + cont.Designation + '">';
                row += '</div></div>';
                row += '<div class="col-lg-2">';
                row += ' <div class="form-group">';
                row += ' <label>Date Of Birth <span style="color:#dc3545">*</span></label>';
                row += '<input type="text" class="form-control form-control-sm dob txtDOB" placeholder="DD/MM/YYYY" id="txtDOB" value="' + ConvertDateDDMMYYYY(cont.DateOfBirth) + '">';
                row += '</div></div>';
                row += '<div class="col-lg-2">';
                row += '<div class="form-group">';
                row += '<label>Date Of Anniversary <span style="color:#dc3545">*</span></label>';
                row += ' <input type="text" class="form-control form-control-sm doa txtAnniversary" placeholder="DD/MM/YYYY" id="txtAnniversary" value="' + ConvertDateDDMMYYYY(cont.DateOfAnniversary) + '">';
                row += '</div></div>';
                row += '<h1></h1>';
                row += '<div style="overflow:auto;width:50%">';
                row += '<table class="table table-bordered GradientdataTable ExtraEmailTableData" id="ExtraEmailTableData' + countEmailtable + '">';
                row += '<thead>';
                row += '<tr>';
                row += '<th style="width:15%">E-Mail Type</th><th>E-Mail</th>';
                row += ' <th style="width:5%">';
                row += '<button type="button" class="btn btn-tool" title="Add Row">';
                row += '<i class="fas fa-plus-circle" id="btnAddExtraEmails" onclick="btnAddExtraEmails(this)" style="float:right;cursor: pointer;color:purple"></i>';
                row += '</button>';
                row += '</th> </tr>';
                row += '</thead>';
                row += ' <tbody id="ExtraEmailDataBody">';
                cont.emailListModels.forEach(function (email) {
                    row += '<tr id="0">';
                    row += ' <td style="display:none" id="EmailListId" class="EmailListId">' + email.EmailListId + '</td>';
                    row += ' <td class="additionalEmailType">';
                    row += '<div class="" style="width:100px">';
                    var emailTypeRow = '<select class="custom-select Gradient-inpuHeight ddlEmailPlus" style="width:100%" id="ddlEmailPlus" data-select2-id="8">' + emailPlusMaster + '</select>';
                    emailTypeRow = emailTypeRow.replace('value=' + email.Email_Type + '', 'value=' + email.Email_Type + ' selected');
                    row += emailTypeRow;
                    row += '</div>';
                    row += '</td>';
                    row += '<td class="additionalEmail">';
                    row += '<input type="text" class="form-control form-control-sm emailPlus" style="width:230px" onchange="validateEmail(this)" placeholder="Email Id" id="txtEmailIdPlus" value="' + email.Email + '">';
                    row += '</td>';
                    row += '<td><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraEmailRow(this)"></i></td>';
                    row += '</tr>';
                });
                row += '</tbody></table> </div>';
                row += '<div style="overflow:auto;padding-left:2px;width:50%">';
                row += '<table class="table table-bordered GradientdataTable ExtraPhoneTableData" id="ExtraPhoneTableData' + countPhntable + '">';
                row += '<thead>';
                row += '<tr>';
                row += ' <th style="width:15%">Phone Type</th>';
                row += '<th>Phone Number</th>';
                row += ' <th style="width:5%">';
                row += '<button type="button" class="btn btn-tool" title="Add Row">';
                row += '<i class="fas fa-plus-circle numbers" id="btnAddExtraPhone" onclick="btnAddExtraPhone(this)" style="float:right;cursor: pointer;color:purple;"></i>';
                row += '</button>';
                row += '</th>';
                row += '</tr>';
                row += '</thead>';
                row += '<tbody id="ExtraPhoneDataBody">';
                cont.phoneListModels.forEach(function (phn) {
                    row += '<tr id="0">';
                    row += ' <td style="display:none" id="PhoneListId" class="PhoneListId">' + phn.PhoneListId + '</td>';
                    row += '<td class="additionalPhoneType">';
                    row += '<div class="" style="width:100px">';
                    var phoneTypeRow = '<select class="custom-select Gradient-inpuHeight ddlPhonePlus" style="width:100%" id="ddlPhoneTypePlus" data-select2-id="9">' + phonePlusMaster + '</select>';
                    phoneTypeRow = phoneTypeRow.replace('value=' + phn.Phone_Type + '', 'value=' + phn.Phone_Type + ' selected');
                    row += phoneTypeRow;
                    row += '</div>';
                    row += '</td>';
                    row += '<td class="additionalPhone">';
                    row += '<input type="text" class="form-control form-control-sm numericValidate phonePlus" style="width:230px" placeholder="Phone Number" id="txtPhoneNoPlus" value="' + phn.PhoneNumber + '">';
                    row += '</td>';
                    row += '<td><i class="far fa-times-circle" style="cursor:pointer" onclick="RemoveExtraPhoneRow(this)"></i></td>';
                    row += '</tr>';
                });
                row += '</tbody>';
                row += '</table>';
                row += '</div>';
                row += '</div>';
                row += '</div></div>';
                row += '</div>';
                countEmailtable++;
                countPhntable++;
            });
            row += '</div>';
            row += '</div>';
            row += '</div>';
            row += '</div>';
            row += '</div>';
            $('#divAccordionParent').append(row);

            // Initialize select2 for all dropdowns
            $('.select').select2();

            // Ensure the dropdowns are correctly populated and enabled
            $('#divAccordionParent .accordion:last').find('.ddlCountry').trigger('change');
            $('#divAccordionParent .accordion:last').find('.ddlState').trigger('change');
        });
    }
}
















///////////////////////////////////////////
$('#btnAdditionalAddress').click(function (e) {

    e.preventDefault();
    var url = "/Contact/Contact/AddAddressByParty";

    var obj = {
        Party_Id: $('#txtPartyId').val(),
        Contact_Email: $('#txtAddressEmail').val(),
        Contact_Phone: $('#txtAddressContact').val(),
        Address_Type: $('#ddlAddressType').val(),
        Address_Line1: $('#txtAddressLine1').val(),
        Address_Line2: $('#txtAddressLine2').val(),
        CountryId: $('#ddlCountry').val(),
        StateId: $('#ddlState').val(),
        CityId: $('#ddlCity').val(),
        GST: $('#txtAddressGST').val(),
        Supplier_Address: $('#chkIsSupplier')[0].checked,
        Contact_Remark: $('#txtRemarks').val(),
        Pincode: $('#txtPIN').val(),
        Representative_Id: $('#ddlRep').val(),
        Zone: $('#txtZone').val(),
    }
    if ($('#ddlRep').val() == "0") {
        toastr.error('Representative is required.');
        $('#ddlRep').focus();
        return false;
    }
    else if ($('#ddlCountry').val() == "0") {
        toastr.error('Country is required.');
        $('#ddlCountry').focus();
        return false;
    }
    
    else if ($('#ddlState').val() == "0") {
        toastr.error('State is required.');
        $('#ddlState').focus();
        return false;
    }
    else if ($('#ddlCity').val() == "0") {
        toastr.error('City is required.');
        $('#ddlCity').focus();
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
                    //toastr.error(ErrorCodes(result.ErrorCodes));
                    SweetErrorMessage(result.ErrorCodes);
                    HideProgress();
                }
                else {
                    //toastr.success(SuccessMessage());
                    SweetSuccessMessage();
                    $('#modal-Address').hide();
                    RefreshAddressModal();
                    HideProgress();
                    setTimeout(function () { window.location = "/Contact/ContactDetails"; }, 1000);
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