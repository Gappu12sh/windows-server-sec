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
$('#btnSave').click(function (e) {

    e.preventDefault();
    var url = "/Representatives/Representatives/AddRepresentatives";
    var obj = {};
    obj.Rep_Name = $('#txtRepresentatives').val();
    if ($('#txtRepresentatives').val() == '') {
        toastr.error('Representatives Name is required.');
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
    var url = "/Representatives/Representatives/UpdateRepresentatives";
    var obj = {};
    obj.Rep_ID = $('#txtRepresentativesId').val();;
    obj.UserId = $('#hiddenUserId').val();
    obj.Rep_Name = $('#txtRepresentatives').val();
    if ($('#txtRepresentatives').val() == '') {
        toastr.error('Representatives Name is required.');
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
                toastr.error(NoRecordMessage());
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
        toastr.error(IdBlank());
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
                    toastr.error(ErrorCodes(result.ErrorCodes));
                    $('#ConfirmBox').modal('hide');
                    HideProgress();
                }
                else {
                    toastr.success(DeleteMessage());
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
    $('#tblRepresentativesData thead').html('');
    $('#tblRepresentativesData tbody').html('');
    var permissions = JSON.parse($('#hiddenPermission').val());
    if (permissions.Representatives.IsAdd) {
        $('#dvAddButton').show();
    }
    if (result.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblRepresentativesData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblRepresentativesData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Representatives : activate to sort column descending'> Representatives Name</th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblRepresentativesData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='IsActive : activate to sort column descending'> IsActive</th>";
        if (permissions.Representatives.IsView ||permissions.Representatives.IsEdit || permissions.Representatives.IsDeleted) {
            thead += "<th> Action </th>";
        }
        thead += "</tr>";
        $('#tblRepresentativesData thead').append(thead);
        var display = permissions.Representatives.IsEdit == true ? "inline" : "none";
        var displayDel = permissions.Representatives.IsDeleted == true ? "inline" : "none";
        var displayView = permissions.Representatives.IsViewParty == true ? "inline" : "none";
        var row = '';
        for (var i = 0; i < result.length; i++) {
            row += "<tr role='row'>";
            row += "<td class='sorting_1' id='RepresentativesId" + result[i].Rep_ID + "' style='display:none'>" + result[i].Rep_ID + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result[i].Rep_Name + "</td>";
            row += "<td><input type='checkBox' checked=" + result[i].IsActive + " disabled /></td>";
            if (permissions.Representatives.IsViewParty || permissions.Representatives.IsEdit || permissions.Representatives.IsDeleted) {
                row += "<td><a title='View Parties' onclick=ViewParties(" + result[i].Rep_ID + ")><i class='fa fa-eye' style='font-size:20px;color:Green;display:" + displayView + "'></i></a>&nbsp;&nbsp;<a onclick=EditRepresentatives(" + result[i].Rep_ID + ")><i class='fas fa-edit' style='font-size:20px;color:#902ca8;display:" + display + "'></i></a> &nbsp;<a onclick=DeleteConfirmation(" + result[i].Rep_ID + ")><i class='far fa-trash-alt' style='font-size:20px;color:red;display:" + displayDel + "'></i></a></td>";
            }
            row += "</tr>";

        }
        HideProgress();
        if ($.fn.DataTable.isDataTable('#tblRepresentativesData')) {
            $('#tblRepresentativesData').DataTable().clear().destroy();
        }
        $('#tblRepresentativesDataBody').append(row);
        $('#tblRepresentativesData').DataTable(
            { "order": [] }
        );
    }
    else {
        HideProgress();
    }
}