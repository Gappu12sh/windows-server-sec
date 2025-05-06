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
            $('.breadCrumbItem')[0].innerHTML = "Representatives";
            $('.contentHeader')[0].innerHTML = "Representatives";
        }
    });
});
jQuery(document).ready(function ($) {

    if ($('#hdnPageLoadOption').val() == 'ViewRepresentativesDetails') {
        GetDetails();

    }
    if ($('#hdnPageLoadOption').val() == 'LoadEditRepresentatives') {
        if ($('#hiddenRepresentativesId').val() != '') {
            bindMasters();
            EditRepresentatives($('#hiddenRepresentativesId').val());
        }
    }

});
$('#btnAddRepresentatives').click(function () {
    $('#btnSave').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#header')[0].innerText = "Add Representatives";
    Refresh();
    bindMasters();
    $('#modal-lg').modal('toggle');
});
function bindMasters() {
    if ($('#hdnRepresentativesGroupMaster').val() != null && $('#hdnRepresentativesGroupMaster').val() != '') {
        $("#ddlRepresentativesGroup").html('');
        var a = JSON.parse($('#hdnRepresentativesGroupMaster').val());
        $("#ddlRepresentativesGroup").append('<option value=0>-Select-</option>');
        for (var i = 0; i < a.length; i++) {
            $("#ddlRepresentativesGroup").append('<option value=' + a[i].Value + '>' + a[i].Text + '</option>');
        }
    }
}


toastr.options = {
    "timeOut": 5000
};




$('#btnSave').click(function (e) {
    e.preventDefault();

    // Get the input values
    var repName = $('#txtRepresentatives').val();
    var repCode = $('#txtRepCode').val();  // Get the Rep_Code value

    // Validate the inputs
    if (repName == '') {
        toastr.error('Representatives Name is required.');
        return false;
    }
    if (repCode == '') {
        toastr.error('Representative Code is required.');
        return false;
    }
    
    // Create the object to send to the server
    var obj = {
        Rep_Name: repName,
        Rep_Code: repCode  // Add the Rep_Code to the object
    };

    var url = "/Representatives/Representatives/AddRepresentatives";
    ShowProgress();

    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }), // Send the object as JSON
        type: "Post",
        success: function (result) {
            if (result.ErrorCodes != null) {
                SweetErrorMessage(result.ErrorCodes);
                $('#modal-lg').modal('hide');
                HideProgress();
            } else {
                SweetSuccessMessage();
                Refresh();
                HideProgress();
                $('#modal-lg').modal('hide');
                GetDetails();  // Refresh the list of representatives
            }
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});





$('#btnUpdate').click(function (e) {
    e.preventDefault();

    // Get the input values
    var repId = $('#txtRepresentativesId').val();
    var repName = $('#txtRepresentatives').val();
    var repCode = $('#txtRepCode').val();  // Get the Rep_Code value
    var userId = $('#hiddenUserId').val();

    // Validate the inputs
    if (repName == '') {
        toastr.error('Representatives Name is required.');
        return false;
    }
    if (repCode == '') {
        toastr.error('Representative Code is required.');
        return false;
    }

    // Create the object to send to the server
    var obj = {
        Rep_ID: repId,
        UserId: userId,
        Rep_Name: repName,
        Rep_Code: repCode  // Add the Rep_Code to the object
    };

    var url = "/Representatives/Representatives/UpdateRepresentatives";
    ShowProgress();

    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),  // Send the object as JSON
        type: "PUT",  // Corrected from "Put" to "PUT"
        success: function (result) {
            if (result.ErrorCodes != null) {
                SweetErrorMessage(result.ErrorCodes);
                $('#modal-lg').modal('hide');
                HideProgress();
            } else {
                SweetUpdateMessage();
                Refresh();
                HideProgress();
                $('#modal-lg').modal('hide');
                GetDetails();  // Refresh the list of representatives
            }
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});















function Refresh() {
    $('#txtRepresentatives').val('');
}

function GetDetails() {

    ShowProgress();
    var obj = {};
    $.ajax({
        url: "/Representatives/Representatives/GetDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Get",
        success: function (result) {
            if (result == null) {
                //toastr.error(NoRecordMessage());
                NoRecordFound();
                var permissions = JSON.parse($('#hiddenPermission').val());
                if (permissions.Representatives.IsAdd) {
                    $('#dvAddButton').show();
                }
                HideProgress();
            }
            else {
                var permissions = JSON.parse($('#hiddenPermission').val());
                if (permissions.Representatives != null) {
                    if (permissions.Representatives.IsView) {
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

function EditRepresentatives(Id) {
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Representatives/Representatives/GetDetailsById",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { id: Id },
            type: "Get",
            success: function (result) {
                $('#txtRepresentativesId').val(result.Rep_ID);
                $('#txtRepresentatives').val(result.Rep_Name);
                HideProgress();
                $('#btnSave').css('display', 'none');
                $('#btnUpdate').css('display', 'block');
                $('#header')[0].innerText = "Update Representatives";
                $('#modal-lg').modal('toggle');
            },
            error: function (msg) {
                toastr.error(msg);
                HideProgress();
                return false;
            }
        });
    }
    else {
        //toastr.error(IdBlank());
        NoDataForId();
        HideProgress();
    }
}
function DeleteConfirmation(Id) {
    $("#txtRepresentativesId").val(Id);
    $('#ConfirmBox').modal('show');
}
function DeleteRepresentatives() {
    var Id = $("#txtRepresentativesId").val();
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Representatives/Representatives/DeleteDetails/" + Id,
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
        //toastr.error(IdBlank());
        NoDataForId();
        HideProgress();
    }
}




var representativeNames = []; // For storing representative names

function bindData(result) {
    $('#tblRepresentativesData thead').html('');
    $('#tblRepresentativesData tbody').html('');
    representativeNames = [];

    var permissions = JSON.parse($('#hiddenPermission').val());

    // Show add button if permissions allow adding representatives
    if (permissions.Representatives.IsAdd) {
        $('#dvAddButton').show();
    }

    if (result.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'></th>"; // Hidden ID column
        thead += "<th style='width: 55px;'>Sr.No.</th>";
        thead += "<th>Representative Code</th>"; // New column for Rep_Code
        thead += "<th>Representatives Name</th>";
        //thead += "<th>IsActive</th>";
        if (permissions.Representatives.IsEdit || permissions.Representatives.IsDeleted) {
            thead += "<th>Action</th>";
        }
        thead += "</tr>";

        $('#tblRepresentativesData thead').append(thead);

        var display = permissions.Representatives.IsEdit ? "inline" : "none";
        var displayDel = permissions.Representatives.IsDeleted ? "inline" : "none";

        var row = '';
        // Loop through the result and generate the rows
        for (var i = 0; i < result.length; i++) {
            var representativeName = result[i].Rep_Name;
            var representativeCode = result[i].Rep_Code || ""; // Fetching the representative code

            if (representativeName) representativeNames.push(representativeName); // Storing representative names

            row += "<tr role='row'>";
            row += "<td style='display:none' id='RepresentativesId" + result[i].Rep_ID + "'>" + result[i].Rep_ID + "</td>";
            row += "<td></td>"; // Placeholder for Sr.No.
            row += "<td>" + representativeCode + "</td>"; // Display Representative Code
            row += "<td>" + representativeName + "</td>"; // Display Representative Name

            //row += "<td><input type='checkbox' " + (result[i].IsActive ? "checked" : "") + " disabled /></td>";

            if (permissions.Representatives.IsEdit || permissions.Representatives.IsDeleted) {
                row += "<td>";
                row += "<a title='View Parties' onclick='ViewParties(" + result[i].Rep_ID + ")'><i class='fa fa-eye' style='font-size:20px;color:Green;display:" + display + "; margin-right: 10px;'></i></a> &nbsp;";
                row += "<a title='Edit Representative' onclick='EditRepresentatives(" + result[i].Rep_ID + ")'><i class='fas fa-edit' style='font-size:20px;color:#902ca8;display:" + display + "; margin-right: 10px;'></i></a> "; // Added margin-right
                row += "&nbsp;<a  title='Delete Representative' onclick='DeleteConfirmation(" + result[i].Rep_ID + ")'><i class='far fa-trash-alt' style='font-size:20px;color:red;display:" + displayDel + "'></i></a>";
                row += "</td>";
            }
            row += "</tr>";
        }

        HideProgress();

        // Clear and destroy any previous DataTable instance
        if ($.fn.DataTable.isDataTable('#tblRepresentativesData')) {
            $('#tblRepresentativesData').DataTable().clear().destroy();
        }

        // Append rows to the table body
        $('#tblRepresentativesData tbody').append(row);

        // Initialize DataTable
        var table = $('#tblRepresentativesData').DataTable({
            "order": [],
            "columnDefs": [{
                "targets": 1,
                "searchable": false,
                "orderable": false,
                "render": function (data, type, row, meta) {
                    return meta.row + 1; // Display the Sr.No. dynamically
                }
            }]
        });

        table.on('draw', function () {
            table.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1; // Update Sr.No. after sorting/filtering
            });
        });
    } else {
        HideProgress();
    }
}













function ViewParties(Id) {
    if (Id > 0) {
        ShowProgress();
        $.ajax({
            url: "/Representatives/Representatives/GetPartyByRepId",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { id: Id },
            type: "Get",
            success: function (result) {
                if (result == null) {
                    //toastr.error(NoRecordMessage());
                    NoRecordFound();
                    HideProgress();
                }
                else {
                    var permissions = JSON.parse($('#hiddenPermission').val());
                    //if (permissions.Representatives != null) {
                    //    if (permissions.Representatives.IsView) {
                    if (result.length > 0) {
                        bindPartiesData(result);
                    }
                    else {
                        NoRecordFound();
                    }
                    //    }
                    //}
                    //else {
                    //    toastr.error(NotPermission());
                    //    HideProgress();
                    //    return false;
                    //}
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
    else {
        //toastr.error(IdBlank());
        NoDataForId();
        HideProgress();
    }
}




//function bindPartiesData(result) {
//    $('#modal-view-Parties').modal('toggle');
//    $('#tblPartiesData thead').html('');
//    $('#tblPartiesData tbody').html('');
//    var permissions = JSON.parse($('#hiddenPermission').val());

//    if (result.length > 0) {
//        var theader = "<tr role='row'>";
//        theader += "<th colspan='5' style='text-align:center;'><b>" + (result[0].PartyAddress?.[0]?.Representatives?.Rep_Name || "Representative") + "</b></th>";
//        theader += "</tr>";
//        $('#tblPartiesData thead').append(theader);

//        var thead = "<tr role='row'>";
//        thead += "<th style='display:none'></th>";
//        thead += "<th style='width:5%'>Sr.No.</th>";
//        thead += "<th style='width:45%'>Contact Name</th>";
//        thead += "<th style='width:20%'>Contact No</th>";
//        thead += "<th>Email</th>";
//        thead += "</tr>";
//        $('#tblPartiesData thead').append(thead);

//        var row = '';
//        for (var i = 0; i < result.length; i++) {
//            row += "<tr role='row'>";
//            row += "<td style='display:none'>" + result[i].Rep_ID + "</td>";
//            row += "<td>" + (i + 1) + "</td>";

//            row += "<td colspan='4'>";
//            row += "<p style='margin-top:0px;margin-bottom:0px;background-color:lightgray;'><b>Party: </b>" + result[i].Party_Name + "</br></p>";

//            var partyAddress = result[i].PartyAddress || [];
//            if (partyAddress.length > 0) {
//                for (var j = 0; j < partyAddress.length; j++) {
//                    // Safely access address properties
//                    var addressType = partyAddress[j].AddressType?.Description || "Unknown Type";
//                    var fullAddress = partyAddress[j].FullAddress || partyAddress[j].Address_Line1 + " " + (partyAddress[j].Address_Line2 || "");
//                    // Log the full address to console
//                    console.log("Full Address:", partyAddress[j]);
//                    row += "<p style='margin-top:2px;margin-bottom:0px'><b>Address (" + addressType + "):</b> " + fullAddress + "</p>";

//                    var contact = partyAddress[j].Contact || [];
//                    if (contact.length > 0) {
//                        for (var k = 0; k < contact.length; k++) {
//                            row += "<div style='width:100%' class='row'>";
//                            row += "<p style='margin-left:8px;margin-top:2px;margin-bottom:0px;width:50%'><b>Contact Person:</b> " + (contact[k].ContactPersonName || "N/A") + "</p>";

//                            var phone = contact[k].phoneListModels || [];
//                            if (phone.length > 0) {
//                                row += "<div style='width:25%' class='row'>";
//                                for (var r = 0; r < phone.length; r++) {
//                                    row += "<p style='margin-top:2px;margin-bottom:0px;width:100%'><b>Phone:</b> " + (phone[r].PhoneNumber || "N/A") + "</p>";
//                                }
//                                row += "</div>";
//                            }

//                            var email = contact[k].emailListModels || [];
//                            if (email.length > 0) {
//                                row += "<div style='width:25%' class='row'>";
//                                for (var t = 0; t < email.length; t++) {
//                                    row += "<p style='margin-top:2px;margin-bottom:0px;width:100%;'><b>Email:</b> " + (email[t].Email || "N/A") + "</p>";
//                                }
//                                row += "</div>";
//                            }
//                            row += "</div>";
//                        }
//                    }
//                }
//            }
//            row += "</td>";
//            row += "</tr>";
//        }
//        HideProgress();
//        $('#tblPartiesData').append(row);
//    } else {
//        HideProgress();
//    }
//}






function bindPartiesData(result) {
    $('#modal-view-Parties').modal('toggle');
    $('#tblPartiesData thead').html('');
    $('#tblPartiesData tbody').html('');
    var permissions = JSON.parse($('#hiddenPermission').val());

    if (result.length > 0) {
        var theader = "<tr role='row'>";
        theader += "<th colspan='5' style='text-align:center;'><b>" + (result[0].PartyAddress?.[0]?.Representatives?.Rep_Name || "Representative") + "</b></th>";
        theader += "</tr>";
        $('#tblPartiesData thead').append(theader);

        var thead = "<tr role='row'>";
        thead += "<th style='display:none'></th>";
        thead += "<th style='width:5%'>Sr.No.</th>";
        thead += "<th style='width:45%'>Contact Name</th>";
        thead += "<th style='width:20%'>Contact No</th>";
        thead += "<th>Email</th>";
        thead += "</tr>";
        $('#tblPartiesData thead').append(thead);

        var row = '';
        for (var i = 0; i < result.length; i++) {
            row += "<tr role='row'>";
            row += "<td style='display:none'>" + result[i].Rep_ID + "</td>";
            row += "<td>" + (i + 1) + "</td>";

            // Collect Contact Name column data
            var contactNameHTML = "<p style='margin-top:0px;margin-bottom:0px;background-color:lightgray;'><b>Party: </b>" + result[i].Party_Name + "</p>";
            var contactNoHTML = "<p style='margin-top:0px;margin-bottom:0px;background-color:lightgray;'><b>Contact No: </b></p>"; // Add background color here
            var emailHTML = "<p style='margin-top:0px;margin-bottom:0px;background-color:lightgray;'><b>Email: </b></p>"; // Add background color here

            var partyAddress = result[i].PartyAddress || [];
            if (partyAddress.length > 0) {
                for (var j = 0; j < partyAddress.length; j++) {
                    var addressType = partyAddress[j].AddressType?.Description || "Unknown Type";
                    var fullAddress = partyAddress[j].FullAddress || (partyAddress[j].Address_Line1 + " " + (partyAddress[j].Address_Line2 || ""));
                    contactNameHTML += "<p><b>Address (" + addressType + "):</b> " + fullAddress + "</p>";

                    var contact = partyAddress[j].Contact || [];
                    if (contact.length > 0) {
                        for (var k = 0; k < contact.length; k++) {
                            var person = contact[k].ContactPersonName || "N/A";
                            var phones = contact[k].phoneListModels || [];
                            var emails = contact[k].emailListModels || [];

                            var maxLength = Math.max(phones.length, emails.length);
                            maxLength = maxLength > 0 ? maxLength : 1;

                            for (var m = 0; m < maxLength; m++) {
                                contactNameHTML += "<p>" + (m === 0 ? "<b>Contact Person:</b> " + person : "&nbsp;") + "</p>";
                                contactNoHTML += "<p>" + (phones[m]?.PhoneNumber || "&nbsp;") + "</p>";
                                emailHTML += "<p>" + (emails[m]?.Email || "&nbsp;") + "</p>";
                            }
                        }

                    }
                }
            }

            row += "<td>" + contactNameHTML + "</td>";
            row += "<td>" + contactNoHTML + "</td>";
            row += "<td>" + emailHTML + "</td>";

            row += "</tr>";
        }
        HideProgress();
        $('#tblPartiesData tbody').append(row);
    } else {
        HideProgress();
    }
}


























$(function () {
    $("#btnPrints").click(function () {
        $('#tblPartiesData').printThis({
            //printDelay: 1,
        });
    });
});