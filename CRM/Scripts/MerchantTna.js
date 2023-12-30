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
            $(this).parent().parent().parent()[0].firstElementChild.className = "nav-link active";
            $(this).parent().addClass('active');
            $(this).css('background-color', 'rgba(255,255,255,.9)');
            $(this).css('color', 'black');

        }
    });
});
jQuery(document).ready(function ($) {

    if ($('#hdnPageLoadOption').val() == 'ViewMerchantTnaDetails') {
        GetDetails();

    }
    if ($('#hdnPageLoadOption').val() == 'LoadEditMerchantTna') {
        if ($('#hiddenMerchantTnaId').val() != '') {
            bindMasters();
            EditMerchantTna($('#hiddenMerchantTnaId').val());
        }
    }

});
$('#btnAddMerchantTna').click(function () {
    $('#btnSave').css('display', 'none');
    $('#btnUpdate').css('display', 'none');
    $('#header')[0].innerText = "Add MerchantTna";
    Refresh();
    bindMasters();
    GetOcnList();
    $('#modal-lg').modal('toggle');
});
function uploadimage() {
    var data = new FormData();
    var files = $("#txtItemImage").get(0).files;
    if (files.length > 0) {
        data.append("Images1", files[0]);
    }
    $.ajax({
        url: "/MerchantTna/MerchantTna/UploadItemImage",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
        },
        error: function (er) {
            alert(er);
        }

    });
}
function bindMasters() {
    if ($('#hdnTemplateMaster').val() != null && $('#hdnTemplateMaster').val() != '') {
        $("#ddlTemplate").html('');
        var a = JSON.parse($('#hdnTemplateMaster').val());
        $("#ddlTemplate").append('<option value=0>-Select-</option>');
        for (var i = 0; i < a.length; i++) {
            $("#ddlTemplate").append('<option value=' + a[i].Value + '>' + a[i].Text + '</option>');
        }
    }
}
$('#btnSave').click(function (e) {

    e.preventDefault();
    var url = "/MerchantTna/MerchantTna/AddEditDeleteMerchantTna";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'Add';
    var FabricTemplateData = $('#tblMerchantTnaDetails tbody tr:has(td)').map(function () {
        var $td = $('td', this);
        return {
            ProcessName: $td[3].innerText,
            StartDay: $td[4].children[0].value,
            EndDay: $td[5].children[0].value,
            ScheduleStartDate: $td[6].innerText,
            ScheduleEndDate: $td[7].innerText,
            BuyerId: $td[8].innerText,
            OcnNo: $td[9].innerText,
            TemplateId: $td[10].innerText,
            DepartmentName: $td[11].innerText,
        }
    }).get();
    ShowProgress();
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj), "templatedetails": JSON.stringify(FabricTemplateData) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            Refresh();
            HideProgress();
            $('#modal-lg').modal('hide');
            GetDetails();
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});
$('#btncomments').click(function (e) {
    e.preventDefault();
    var url = "/MerchantTna/MerchantTna/AddEditDeleteMerchantTna";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'AddComments';
    obj.ProcessName = $('#txtprocessId')[0].innerText;
    obj.Type = $('#txttype')[0].innerText;
    obj.Comments = $('#txtcomments').val();
    obj.CommentsType = 'General';
    if ($("#txtItemImage").get(0).files.length > 0) {
        obj.ItemImage = $("#txtItemImage").get(0).files[0].name;
    }
    if ($("#txtcomments").val() == "") {
        toastr.error('Comments is required.');
        return false;
    }
    if ($("#txtItemImage").get(0).files.length == 0) {
        toastr.error('File is required.');
        return false;
    }

    ShowProgress();
    if ($("#txtItemImage").get(0).files.length > 0) {
        uploadimage();
    }
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            GetCommentsById();
            $('#txtcomments').val('');
            $("#txtItemImage").val('');
            $('#txtsprocessColor').val('');
            $('#modal-Comments').modal('hide');
            GetMerchantCommentsById($('#txtprocessId')[0].innerText);
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});
$('#btnrejectcomments').click(function (e) {
    e.preventDefault();
    var url = "/MerchantTna/MerchantTna/AddEditDeleteMerchantTna";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'AddFabricComments';
    obj.ProcessName = $('#txtprocessId')[0].innerText;
    obj.Type = $('#txttype')[0].innerText;
    obj.Comments = $('#txtrejectcomments').val();
    obj.CommentsType = 'Rejected'
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    if ($("#txtItemImage").get(0).files.length > 0) {
        obj.ItemImage = $("#txtItemImage").get(0).files[0].name;
    }
    if ($("#txtrejectcomments").val() == "") {
        toastr.error('Comments is required.');
        return false;
    }
    ShowProgress();
    if ($("#txtItemImage").get(0).files.length > 0) {
        uploadimage();
    }
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            GetRejectCommentsById();
            RejectProcess();
            $('#txtrejectcomments').val('');
            $("#txtItemImage").val('');
            $('#txtsprocessColor').val('');
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});
$('#btnapprovecomments').click(function (e) {
    e.preventDefault();
    var url = "/MerchantTna/MerchantTna/AddEditDeleteMerchantTna";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'AddFabricComments';
    obj.ProcessName = $('#txtprocessId')[0].innerText;
    obj.Type = $('#txttype')[0].innerText;
    obj.Comments = $('#txtapprovecomments').val();
    obj.CommentsType = 'Approved';
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    if ($("#txtItemImage").get(0).files.length > 0) {
        obj.ItemImage = $("#txtItemImage").get(0).files[0].name;
    }
    if ($("#txtapprovecomments").val() == "") {
        toastr.error('Comments is required.');
        return false;
    }
    ShowProgress();
    if ($("#txtItemImage").get(0).files.length > 0) {
        uploadimage();
    }
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            GetApprovedCommentsById();
            ApproveProcess();
            $('#txtrejectcomments').val('');
            $("#txtItemImage").val('');
            $('#txtsprocessColor').val('');
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});
$('#btnrejectTrimscomments').click(function (e) {
    e.preventDefault();
    var url = "/MerchantTna/MerchantTna/AddEditDeleteMerchantTna";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'AddTrimsComments';
    obj.ProcessName = $('#txtprocessId')[0].innerText;
    obj.Type = $('#txttype')[0].innerText;
    obj.Comments = $('#txtrejectTrimscomments').val();
    obj.CommentsType = 'Rejected'
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    if ($("#txtItemImage").get(0).files.length > 0) {
        obj.ItemImage = $("#txtItemImage").get(0).files[0].name;
    }
    if ($("#txtrejectTrimscomments").val() == "") {
        toastr.error('Comments is required.');
        return false;
    }
    ShowProgress();
    if ($("#txtItemImage").get(0).files.length > 0) {
        uploadimage();
    }
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            GetRejectTrimsCommentsById();
            RejectTrimsProcess();
            $('#txtrejectTrimscomments').val('');
            $("#txtItemImage").val('');
            $('#txtsprocessColor').val('');
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});
$('#btnapproveTrimscomments').click(function (e) {
    e.preventDefault();
    var url = "/MerchantTna/MerchantTna/AddEditDeleteMerchantTna";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'AddTrimsComments';
    obj.ProcessName = $('#txtprocessId')[0].innerText;
    obj.Type = $('#txttype')[0].innerText;
    obj.Comments = $('#txtapproveTrimscomments').val();
    obj.CommentsType = 'Approved';
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    if ($("#txtItemImage").get(0).files.length > 0) {
        obj.ItemImage = $("#txtItemImage").get(0).files[0].name;
    }
    if ($("#txtapproveTrimscomments").val() == "") {
        toastr.error('Comments is required.');
        return false;
    }
    ShowProgress();
    if ($("#txtItemImage").get(0).files.length > 0) {
        uploadimage();
    }
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            GetApprovedTrimsCommentsById();
            ApproveTrimsProcess();
            $('#txtrejectcomments').val('');
            $("#txtItemImage").val('');
            $('#txtsprocessColor').val('');
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
});
function Refresh() {
    $('#ddlOcn').val('0');
    $('#ddlTemplate').val('0');
    $('#tblMerchantTnaDetails thead').html('');
    $('#tblMerchantTnaDetails tbody').html('');
}
function GetDetails() {

    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'View';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
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

function GetDataByDept(OcnNo, TemplateName, DepartmentName) {
    $('#modal-GetProcessData').modal('show');
    $('#txtOcnNo')[0].innerText = OcnNo;
    $('#txtTemplateName')[0].innerText = TemplateName;
    $('#txtDepartment')[0].innerText = DepartmentName.className;
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.OcnNo = OcnNo;
    obj.TemplateName = TemplateName;
    obj.DepartmentName = DepartmentName.className;
    obj.Operation = 'GetDataByDept';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetDataByDept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                bindGetDataByDeptData(result);
                HideProgress();
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
function GetDataByDeptOnDate(OcnNo, TemplateName, DepartmentName) {
    $('#modal-GetProcessData').modal('show');

    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.OcnNo = OcnNo;
    obj.TemplateName = TemplateName;
    obj.DepartmentName = DepartmentName;
    obj.Operation = 'GetDataByDept';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetDataByDept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                bindGetDataByDeptData(result);
                HideProgress();
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

function GetProcessCompletedStatus(data, Type) {
    var CellId = data.id;
    var ProcessId = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 1].innerText;
    ShowProgress();
    var obj = {};
    obj.Id = ProcessId;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCompletedStatus';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetDataByDept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                if (result.listclsMerchantTnaDetailsModel[0].ProcessWay == 'Two Way') {
                    GetEndworkDate(data, Type);
                }
                else {
                    if (result.listclsMerchantTnaDetailsModel[0].CompletedStartStatus == '') {
                        toastr.error('Please fill start date.');
                    }
                    else {
                        GetEndworkDate(data, Type);
                    }
                }
                HideProgress();
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
function GetOcnList() {

    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetOcnList';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetOcnList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $("#ddlOcn").html('');
                $("#ddlOcn").append('<option value=0>-Select-</option>');
                for (var i = 0; i < result.clsGetOcnList.length; i++) {
                    $("#ddlOcn").append('<option value=' + result.clsGetOcnList[i].BoId + '>' + result.clsGetOcnList[i].OcnNo + '</option>');
                }
                HideProgress();
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
function GetTemplateData() {

    ShowProgress();
    var obj = {};
    obj.Id = $("#ddlTemplate").val();
    obj.OcnNo = $("#ddlOcn").val();
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetTemplateData';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetTemplateData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                bindTemplate(result);
                $('#btnSave').css('display', 'block');
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
function GetCurrentDate(data) {
    var ProcessColor = $('#txtsprocessColor').val();
    if (ProcessColor == 'red') {
        toastr.error('Please add comments before active process.');
    }
    else {
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output =
            (month < 10 ? '0' : '') + month + '/' +
            (day < 10 ? '0' : '') + day + '/' + d.getFullYear();
        $('#currentDate')[0].innerText = ConvertDateDDMMYYYY(output);
        $('#btnGetDate').attr('disabled', true);
        ShowProgress();
        var obj = {};
        obj.Id = $('#Processid')[0].innerText;
        obj.UserId = $('#hiddenUserId').val();
        obj.Operation = 'UpdateProcessData';
        obj.CompletedDate = output;
        obj.CompletedDateType = $('#ProcessDatetype')[0].innerText;
        obj.OcnNo = $('#txtOcnNo')[0].innerText;
        $.ajax({
            url: "/MerchantTna/MerchantTna/UpdateProcessDataMerchantTna",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "data": JSON.stringify(obj) }),
            type: "Post",
            success: function (result) {
                var a = result;
                if (result.ResponseCode == 100) {
                    toastr.success('Details updated successfully.');
                    HideProgress();
                    var OcnNo = $('#txtOcnNo')[0].innerText;
                    var TemplateName = $('#txtTemplateName')[0].innerText;
                    var Department = $('#txtDepartment')[0].innerText;
                    $('#modal-GetProcessData').modal({ backdrop: 'static', keyboard: false });
                    GetDataByDeptOnDate(OcnNo, TemplateName, Department);
                    ApproveRequest();
                    $('#modal-GetUpdateProcessData').modal('hide');

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
        })
    }
};
function GetStartworkDate(data, Type) {
    $('#txtsprocessColor').val('');
    $('#txtsprocessColor').val(data.style.backgroundColor);
    var CellId = data.id;
    var Process = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[CellId].innerText;
    var ProcessId = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 1].innerText;
    var ScheduleCommonDate = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 2].innerText;
    var CompletedCommonDate = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 4].innerText;
    var ProcessWay = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 6].innerText;
    var Request = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 7].innerText;
    var ApprovedDate = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 8].innerText;
    var startDate = data.innerText;
    GetMerchantCommentsById(ProcessId);
    $('#txtprocessname').val('');
    $('#txtprocessname').val(Process);
    var CheckDeptName = $('#txtDepartment')[0].innerText;
    $('#txtprocessId')[0].innerText = ProcessId;
    $('#txttype')[0].innerText = 'StartDate'
    if (ProcessWay == 'Two Way') {
        if (Request == '1') {
            if (ApprovedDate != '' || ApprovedDate == null) {
                toastr.error('This process already approved');
            }
            else {
                if (CheckDeptName == 'FO' || CheckDeptName == 'FF') {
                    $('#modal-ApprovalProcessData').modal({ backdrop: 'static', keyboard: false });
                    $('#btnApproved').attr('disabled', false);
                    $('#btnReject').attr('disabled', false);
                }
                if (CheckDeptName == 'T') {
                    $('#modal-RejectProcessData').modal({ backdrop: 'static', keyboard: false });
                    $('#btnTrimsApproved').attr('disabled', false);
                    $('#btnTrimsReject').attr('disabled', false);
                }
            }
        }
        else {
            if (CheckDeptName == 'MM' || CheckDeptName == 'M' || CheckDeptName == 'AM') {
                $('#modal-GetUpdateProcessData').modal({ backdrop: 'static', keyboard: false });
                bindUpdateProcessData(startDate, Process, ProcessId, ScheduleCommonDate, Type, CompletedCommonDate);
            }
            else {
                toastr.error('There is no request');
            }
        }
    }
    else {
        if (CheckDeptName == 'FO' || CheckDeptName == 'FF' || CheckDeptName == 'T') {
            toastr.error('You do not have permission to start this process');
        }
        else {
            $('#modal-GetUpdateProcessData').modal({ backdrop: 'static', keyboard: false });
            bindUpdateProcessData(startDate, Process, ProcessId, ScheduleCommonDate, Type, CompletedCommonDate);
        }
    }

}
function GetEndworkDate(data, Type) {
    $('#txtsprocessColor').val('');
    $('#txtsprocessColor').val(data.style.backgroundColor);
    var CellId = data.id;
    var Process = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[CellId].innerText;
    var ProcessId = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 1].innerText;
    var ScheduleCommonDate = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 3].innerText;
    var CompletedCommonDate = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 5].innerText;
    var ProcessWay = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 6].innerText;
    var Request = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 7].innerText;
    var ApprovedDate = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 8].innerText;
    var ProcessStatus = data.parentElement.parentElement.parentElement.parentElement.children[0].children[0].children[0].cells[parseFloat(CellId) + 9].innerText;
    var endDate = data.innerText;
    GetMerchantCommentsById(ProcessId);
    var CheckDeptName = $('#txtDepartment')[0].innerText;
    $('#txtprocessId')[0].innerText = ProcessId;
    $('#txttype')[0].innerText = 'EndDate'
    if (CheckDeptName == 'FO' || CheckDeptName == 'FF' || CheckDeptName == 'T') {
        toastr.error('You do not have permission to end this process');
    }       
    else {
        if (ProcessStatus == 1) {
            $('#modal-GetUpdateProcessData').modal({ backdrop: 'static', keyboard: false });
            bindUpdateProcessData(endDate, Process, ProcessId, ScheduleCommonDate, Type, CompletedCommonDate);
        }
        else {
            toastr.error('This process is not approved yet');
        }
    }
}
function CommentsModal(data) {
    $('#modal-Comments').modal('toggle');
    $('#txtcomments').val('');
    $('#txtprocessId')[0].innerText = data.parentElement.parentElement.children[0].innerText;
    $('#txttype')[0].innerText = data.parentElement.parentElement.children[5].innerText;
    GetCommentsById();
}
function DeleteMerchantTna(OcnNo, TemplateId) {

    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'Delete';
    obj.OcnNo = OcnNo;
    obj.TemplateId = TemplateId;
    $.ajax({
        url: "/MerchantTna/MerchantTna/DeleteDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                toastr.success('Details removed successfully.');
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
function bindUpdateProcessData(StartDate, ProcessName, ProcessId, ScheduleCommonDate, Type, CompletedCommonDate) {
    $('#tblFabricUpdateProcessDetails thead').html('');
    $('#tblFabricUpdateProcessDetails tbody').html('');
    var Processtype = '';
    if (Type == 'StartDate') {
        Processtype = 'Start';
    }
    else {
        Processtype = 'End';
    }
    if (StartDate != '') {

        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>ProcessId  </th>";
        thead += "<th  tabindex='0' aria-controls='tblFabricUpdateProcessDetails' rowspan='1' colspan='1'  aria-label='ProcessName: activate to sort column descending'>Process Name</th>";
        thead += "<th  tabindex='0' aria-controls='tblFabricUpdateProcessDetails' rowspan='1' colspan='1'  aria-label='StartDate: activate to sort column descending'>" + Processtype + " Date</th>";
        thead += "<th  tabindex='0' aria-controls='tblFabricUpdateProcessDetails' rowspan='1' colspan='1'  aria-label='StartDone: activate to sort column descending'>" + Processtype + " Done</th>";
        thead += "<th  tabindex='0' aria-controls='tblFabricUpdateProcessDetails' rowspan='1' colspan='1'  aria-label='ActualStartDate: activate to sort column descending'>Actual " + Processtype + " Date</th>";
        thead += "<th style='display:none'>Type  </th>";
        thead += "<th  tabindex='0' aria-controls='tblFabricUpdateProcessDetails' rowspan='1' colspan='1'  aria-label='Comments: activate to sort column descending'>Comments</th>";
        thead += "</tr>";
        $('#tblFabricUpdateProcessDetails thead').append(thead);
        var row = "<tr role='row'>";
        row += "<td style='display:none' id='Processid'>" + ProcessId + "</td>";
        //row += "<td class='sorting_1' id='MerchantTnaId" + result.listclsMerchantTnaDetailsModel[i].Id + "' style='display:none'>" + result.listclsMerchantTnaDetailsModel[i].Id + "</td>";
        row += "<td>" + ProcessName + "</button></td>";
        row += "<td>" + StartDate + "</button></td>";
        row += "<td><button type='button' class='btn btn-secondary' id='btnGetDate' onclick=GetCurrentDate(this)>" + Processtype + "</button></td>";
        row += "<td id='currentDate'></td>";
        row += "<td style='display:none' id='ProcessDatetype'>" + Type + "</td>";
        row += "<td><button type='button' class='btn btn-secondary' id='comments' onclick=CommentsModal(this)>+</button></td>";
        row += "</tr>";
        $('#tblFabricUpdateProcessDetails').append(row);
        if (CompletedCommonDate != "" && Type == 'StartDate') {
            $('#currentDate')[0].innerText = ConvertDateDDMMYYYY(CompletedCommonDate);
            $('#btnGetDate').attr('disabled', true);
        }
        else if (CompletedCommonDate != "" && Type == 'EndDate') {
            $('#currentDate')[0].innerText = ConvertDateDDMMYYYY(CompletedCommonDate);
            $('#btnGetDate').attr('disabled', true);
        }
        else {
            $('#btnGetDate').attr('disabled', false);

        }
        HideProgress();
    }
    else {
        HideProgress();
    }
}
function bindGetDataByDeptData(result) {
    $('#tblFabricProcessDetails thead').html('');
    $('#tblFabricProcessDetails tbody').html('');
    if (result.listclsMerchantTnaDetailsModel.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th  tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Sr No: activate to sort column descending'></th>";
        for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
            thead += "<th tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='ProcessName: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].ProcessName + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].Id + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].ScheduleStartDate + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].ScheduleEndDate + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].CompletedStartDate + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].CompletedEndDate + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].ProcessWay + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].Request + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].ApprovedDate + "</th>";
            thead += "<th style='display:none' tabindex='0' aria-controls='tblFabricProcessDetails' rowspan='1' colspan='1'  aria-label='Processid: activate to sort column descending'>" + result.listclsMerchantTnaDetailsModel[i].ProcessStatus + "</th>";
        }
        thead += "</tr>";
        $('#tblFabricProcessDetails thead').append(thead);
        var row = "<tr role='row'>";
        row += "<td style='display:none'></td>";
        // row += "<td class='sorting_1' id='MerchantTnaId" + result.listclsMerchantTnaDetailsModel[i].Id + "' style='display:none'>" + result.listclsMerchantTnaDetailsModel[i].Id + "</td>";
        row += "<td><b>StartDate</b></td>";
        var k = 0;
        var Color = "";
        for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
            if (i == 0) {
                k = 2;
            }
            else if (i == 1) {
                k = 12;
            }
            else {
                k = parseFloat(k) + 10;
            }
            var Color = "";
            var TextColor = "";
            if (new Date(result.listclsMerchantTnaDetailsModel[i].ScheduleStartDate) >= new Date(result.listclsMerchantTnaDetailsModel[i].CompletedStartDate) && result.listclsMerchantTnaDetailsModel[i].CompletedStartStatus == 'Completed') {
                Color = 'Green';
            }
            if (new Date(result.listclsMerchantTnaDetailsModel[i].CompletedStartDate) > new Date(result.listclsMerchantTnaDetailsModel[i].ScheduleStartDate) && result.listclsMerchantTnaDetailsModel[i].CompletedStartStatus == 'Completed') {
                Color = 'Orange';
            }
            if (new Date(result.listclsMerchantTnaDetailsModel[i].ScheduleStartDate) < new Date(GetCurrentDateFormat()) && result.listclsMerchantTnaDetailsModel[i].CompletedStartStatus == '') {
                Color = 'Red';
            }
            if (Color != "") {
                TextColor = "white";
            }
            else {
                TextColor = "black";
            }
            row += "<td style='color:" + TextColor + ";background-color:" + Color + "' id='" + k + "' onclick=GetStartworkDate(this,'StartDate')>" + ConvertDateDDMMYYYY(result.listclsMerchantTnaDetailsModel[i].ScheduleStartDate) + "</td>";
        }
        row += "</tr>";
        $('#tblFabricProcessDetails').append(row);
        var row = "<tr role='row'>";
        // row += "<td class='sorting_1' id='MerchantTnaId" + result.listclsMerchantTnaDetailsModel[i].Id + "' style='display:none'>" + result.listclsMerchantTnaDetailsModel[i].Id + "</td>";
        row += "<td><b>EndDate</b></td>";
        for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
            if (i == 0) {
                k = 2;
            }
            else if (i == 1) {
                k = 12;
            }
            else {
                k = parseFloat(k) + 10;
            }
            var Color = "";
            var TextColor = "";
            if (new Date(result.listclsMerchantTnaDetailsModel[i].ScheduleEndDate) >= new Date(result.listclsMerchantTnaDetailsModel[i].CompletedEndDate) && result.listclsMerchantTnaDetailsModel[i].CompletedEndStatus == 'Completed') {
                Color = 'Green';
            }
            if (new Date(result.listclsMerchantTnaDetailsModel[i].CompletedEndDate) > new Date(result.listclsMerchantTnaDetailsModel[i].ScheduleEndDate) && result.listclsMerchantTnaDetailsModel[i].CompletedEndStatus == 'Completed') {
                Color = 'Orange';
            }
            if (new Date(result.listclsMerchantTnaDetailsModel[i].ScheduleEndDate) < new Date(GetCurrentDateFormat()) && result.listclsMerchantTnaDetailsModel[i].CompletedEndStatus == '') {
                Color = 'Red';
            }
            if (Color != "") {
                TextColor = "white";
            }
            else {
                TextColor = "black";
            }
            row += "<td style='color:" + TextColor + ";background-color:" + Color + "' id='" + k + "'onclick=GetProcessCompletedStatus(this,'EndDate')>" + ConvertDateDDMMYYYY(result.listclsMerchantTnaDetailsModel[i].ScheduleEndDate) + "</td>";
        }
        row += "</tr>";
        $('#tblFabricProcessDetails').append(row);

        HideProgress();

    }
    else {
        HideProgress();
    }
}
function bindData(result) {
    $('#tblMerchantTnaData thead').html('');
    $('#tblMerchantTnaData tbody').html('');
    if (result.listclsMerchantTnaDetailsModel.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Ocn No: activate to sort column descending'> Ocn No </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Buyer : activate to sort column descending'> Buyer </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Style : activate to sort column descending'> Style </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Style Desc : activate to sort column descending'> Style Desc </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='MM : activate to sort column descending'> MM </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='M : activate to sort column descending'> M </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='AM : activate to sort column descending'> AM </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='FO : activate to sort column descending'> FM </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='FF : activate to sort column descending'> FSI </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='T : activate to sort column descending'> TM </th>";
        thead += "</tr>";
        $('#tblMerchantTnaData thead').append(thead);
        var MMcolor = '';
        var AMcolor = '';
        var Mcolor = '';
        var MMTextcolor = "black";
        var AMTextcolor = "black";
        var MTextcolor = "black";
        var FOcolor = '';
        var FFcolor = '';
        var Tcolor = '';
        var FOTextcolor = "black";
        var FFTextcolor = "black";
        var TTextcolor = "black";
        for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
            var row = "<tr role='row'>";
            row += "<td class='sorting_1' id='MerchantTnaId" + result.listclsMerchantTnaDetailsModel[i].Id + "' style='display:none'>" + result.listclsMerchantTnaDetailsModel[i].Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result.listclsMerchantTnaDetailsModel[i].OcnNo + "</td>";
            row += "<td>" + result.listclsMerchantTnaDetailsModel[i].BuyerName + "</td>";
            row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Style + "</td>";
            row += "<td>" + result.listclsMerchantTnaDetailsModel[i].StyleDesc + "</td>";
            for (var j = 0; j < result.listColor.length; j++) {
                if (result.listclsMerchantTnaDetailsModel[i].Id == result.listColor[j].OcnNo && "MM" == result.listColor[j].DepartmentName) {
                    if (result.listColor[j].ColorStatus == 'Red') {
                        MMcolor = "Red";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Orange') {
                        MMcolor = "Orange";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Green') {
                        MMcolor = "Green";
                        break;
                    }
                    else {
                        MMcolor = "White";
                    }
                }
            }
            for (var j = 0; j < result.listColor.length; j++) {
                if (result.listclsMerchantTnaDetailsModel[i].Id == result.listColor[j].OcnNo && "AM" == result.listColor[j].DepartmentName) {
                    if (result.listColor[j].ColorStatus == 'Red') {
                        AMcolor = "Red";
                        break;

                    }
                    else if (result.listColor[j].ColorStatus == 'Orange') {
                        AMcolor = "Orange";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Green') {
                        AMcolor = "Green";
                        break;
                    }
                    else {
                        AMcolor = "White";
                    }
                }
            }
            for (var j = 0; j < result.listColor.length; j++) {
                if (result.listclsMerchantTnaDetailsModel[i].Id == result.listColor[j].OcnNo && "M" == result.listColor[j].DepartmentName) {
                    if (result.listColor[j].ColorStatus == 'Red') {
                        Mcolor = "Red";
                        break;

                    }
                    else if (result.listColor[j].ColorStatus == 'Orange') {
                        Mcolor = "Orange";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Green') {
                        Mcolor = "Green";
                        break;
                    }
                    else {
                        Mcolor = "White";
                    }
                }
            }

            for (var j = 0; j < result.listColor.length; j++) {
                if (result.listclsMerchantTnaDetailsModel[i].Id == result.listColor[j].OcnNo && "FO" == result.listColor[j].DepartmentName) {
                    if (result.listColor[j].ColorStatus == 'Red') {
                        FOcolor = "Red";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Orange') {
                        FOcolor = "Orange";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Green') {
                        FOcolor = "Green";
                        break;
                    }
                    else {
                        FOcolor = "White";
                    }
                }
            }
            for (var j = 0; j < result.listColor.length; j++) {
                if (result.listclsMerchantTnaDetailsModel[i].Id == result.listColor[j].OcnNo && "FF" == result.listColor[j].DepartmentName) {
                    if (result.listColor[j].ColorStatus == 'Red') {
                        FFcolor = "Red";
                        break;

                    }
                    else if (result.listColor[j].ColorStatus == 'Orange') {
                        FFcolor = "Orange";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Green') {
                        FFcolor = "Green";
                        break;
                    }
                    else {
                        FFcolor = "White";
                    }
                }
            }
            for (var j = 0; j < result.listColor.length; j++) {
                if (result.listclsMerchantTnaDetailsModel[i].Id == result.listColor[j].OcnNo && "T" == result.listColor[j].DepartmentName) {
                    if (result.listColor[j].ColorStatus == 'Red') {
                        Tcolor = "Red";
                        break;

                    }
                    else if (result.listColor[j].ColorStatus == 'Orange') {
                        Tcolor = "Orange";
                        break;
                    }
                    else if (result.listColor[j].ColorStatus == 'Green') {
                        Tcolor = "Green";
                        break;
                    }
                    else {
                        Tcolor = "White";
                    }
                }
            }
            if (MMTextcolor == "Green" || MMTextcolor == "Orange" || MMTextcolor == "Red") {
                MMTextcolor = "white";
            }
            if (MTextcolor == "Green" || MTextcolor == "Orange" || MTextcolor == "Red") {
                MTextcolor = "white";
            }
            if (AMTextcolor == "Green" || AMTextcolor == "Orange" || AMTextcolor == "Red") {
                AMTextcolor = "white";
            }
            if (FOTextcolor == "Green" || FOTextcolor == "Orange" || FOTextcolor == "Red") {
                FOTextcolor = "white";
            }
            if (FFTextcolor == "Green" || FFTextcolor == "Orange" || FFTextcolor == "Red") {
                FFTextcolor = "white";
            }
            if (TTextcolor == "Green" || TTextcolor == "Orange" || TTextcolor == "Red") {
                TTextcolor = "white";
            }
            row += "<td class='MM' style='color:" + MMTextcolor + ";cursor:pointer;font-weight:bold;background-color:" + MMcolor + "' id='btnGetData'  onclick='GetDataByDept(" + result.listclsMerchantTnaDetailsModel[i].Id + "," + result.listclsMerchantTnaDetailsModel[i].TemplateName + ",this)'>MM</td>";
            row += "<td class='M' style='color:" + MTextcolor + ";cursor:pointer;font-weight:bold;background-color:" + Mcolor + "' id='btnGetData' onclick='GetDataByDept(" + result.listclsMerchantTnaDetailsModel[i].Id + "," + result.listclsMerchantTnaDetailsModel[i].TemplateName + ",this)'>M</td>";
            row += "<td class='AM' style='color:" + AMTextcolor + ";cursor:pointer;font-weight:bold;background-color:" + AMcolor + "' id='btnGetData' onclick='GetDataByDept(" + result.listclsMerchantTnaDetailsModel[i].Id + "," + result.listclsMerchantTnaDetailsModel[i].TemplateName + ",this)'>AM</td>";
            row += "<td class='FO' style='color:" + FOTextcolor + ";cursor:pointer;font-weight:bold;background-color:" + FOcolor + "' id='btnGetData'  onclick='GetDataByDept(" + result.listclsMerchantTnaDetailsModel[i].Id + "," + result.listclsMerchantTnaDetailsModel[i].TemplateName + ",this)'>FM</td>";
            row += "<td class='FF' style='color:" + FFTextcolor + ";cursor:pointer;font-weight:bold;background-color:" + FFcolor + "' id='btnGetData' onclick='GetDataByDept(" + result.listclsMerchantTnaDetailsModel[i].Id + "," + result.listclsMerchantTnaDetailsModel[i].TemplateName + ",this)'>FSI</td>";
            row += "<td class='T' style='color:" + TTextcolor + ";cursor:pointer;font-weight:bold;background-color:" + Tcolor + "' id='btnGetData' onclick='GetDataByDept(" + result.listclsMerchantTnaDetailsModel[i].Id + "," + result.listclsMerchantTnaDetailsModel[i].TemplateName + ",this)'>TM</td>";
            row += "</tr>";
            $('#tblMerchantTnaDataBody').append(row);
        }
        HideProgress();
        $('#tblMerchantTnaData').DataTable();
    }
    else {
        HideProgress();
    }
}

function bindTemplate(result) {

    $('#tblMerchantTnaDetails thead').html('');
    $('#tblMerchantTnaDetails tbody').html('');
    if (result.clsGetFabricTemplateData.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'></th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Process Name: activate to sort column descending'> Process Name </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Start Day : activate to sort column descending'> Start Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='End Day : activate to sort column descending'> End Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleStartDate : activate to sort column descending'> ScheduleStartDate </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTnaData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleEndDate : activate to sort column descending'> ScheduleEndDate </th>";
        thead += "<th style='display:none'> BuyerId </th>";
        thead += "<th style='display:none'> OcnNo </th>";
        thead += "<th style='display:none'> TemplateId </th>";
        thead += "<th style='display:none'> DepartmentName </th>";
        thead += "</tr>";
        $('#tblMerchantTnaDetails thead').append(thead);
        var EndDate;
        for (var i = 0; i < result.clsGetFabricTemplateData.length; i++) {
            var row = "<tr role='row'>";
            row += "<td><input type='checkbox' id='chkbox" + i + "' checked='checked'></td>";
            row += "<td class='sorting_1' id='MerchantTnaId" + result.clsGetFabricTemplateData[i].Id + "' style='display:none'>" + result.clsGetFabricTemplateData[i].Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result.clsGetFabricTemplateData[i].ProcessName + "</td>";
            row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtStartDay" + i + "' value='" + result.clsGetFabricTemplateData[i].StartDay + "'></td>";
            row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtEndDay" + i + "' value='" + result.clsGetFabricTemplateData[i].EndDay + "'></td>";
            row += "<td>" + result.listclsMerchantTnaDetailsModel[0].ScheduleStartDate + "</td>";
            //var startDate = result.listclsMerchantTnaDetailsModel[0].ScheduleStartDate;
            //var SplitDate = startDate.split("-");
            //var date = SplitDate[0];
            //var month = SplitDate[1];
            //var year = SplitDate[2];
            row += "<td>" + result.listclsMerchantTnaDetailsModel[0].ScheduleStartDate + "</td>";
            row += "<td style='display:none'>" + result.listclsMerchantTnaDetailsModel[0].BuyerId + "</td>";
            row += "<td style='display:none'>" + $('#ddlOcn option:selected').val() + "</td>";
            row += "<td style='display:none'>" + $('#ddlTemplate option:selected').val() + "</td>";
            row += "<td style='display:none'>" + result.clsGetFabricTemplateData[i].DepartmentName + "</td>";
            row += "</tr>";
            //if (result.clsGetFabricTemplateData.length - 1 == i)
            //{

            //    row = "<tr role='row'>";               
            //    row += "<td colspan='12'><button type='button' class='btn btn-secondary' id='btnAddRow' onclick='Addrow()'>Add New Process</button></td>";
            //    row += "</tr>";

            //}
            $('#tblMerchantTnaDetails').append(row);
        }

        HideProgress();

    }
    else {
        HideProgress();
    }
}

function GetCommentsById() {
    ShowProgress();
    var obj = {};
    obj.Id = $('#txtprocessId')[0].innerText;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCommentsById';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetCommentsDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $('#tblPreviousCommentsDetails tbody').html('');
                for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'General') {
                        var row = "<tr role='row'>";
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                        if (result.listclsMerchantTnaDetailsModel[i].Attachment != "") {
                            row += "<td><a href='/Docs/Comments/" + result.listclsMerchantTnaDetailsModel[i].Attachment + "' download><i class='fa fa-paperclip' style='float:center'></i></a></td>"
                        }
                        else {
                            row += "<td></td>";
                        }
                        row += "</tr>";
                        $('#tblPreviousCommentsDetails tbody').append(row);
                    }
                }
                HideProgress();
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

function GetRejectCommentsById() {
    ShowProgress();
    var obj = {};
    obj.Id = $('#txtprocessId')[0].innerText;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCommentsById';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetCommentsDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $('#tblPreviousRejectCommentsDetails tbody').html('');
                for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'Rejected') {
                        var row = "<tr role='row'>";
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                        if (result.listclsMerchantTnaDetailsModel[i].Attachment != "") {
                            row += "<td><a href='/Docs/Comments/" + result.listclsMerchantTnaDetailsModel[i].Attachment + "' download><i class='fa fa-paperclip' style='float:center'></i></a></td>"
                        }
                        else {
                            row += "<td></td>";
                        }
                        row += "</tr>";
                        $('#tblPreviousRejectCommentsDetails tbody').append(row);
                    }
                }
                HideProgress();
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
function GetApprovedCommentsById() {
    ShowProgress();
    var obj = {};
    obj.Id = $('#txtprocessId')[0].innerText;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCommentsById';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetCommentsDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $('#tblPreviousCommentsDetails tbody').html('');
                for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'Approved') {
                        var row = "<tr role='row'>";
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                        if (result.listclsMerchantTnaDetailsModel[i].Attachment != "") {
                            row += "<td><a href='/Docs/Comments/" + result.listclsMerchantTnaDetailsModel[i].Attachment + "' download><i class='fa fa-paperclip' style='float:center'></i></a></td>"
                        }
                        else {
                            row += "<td></td>";
                        }
                        row += "</tr>";
                        $('#tblPreviousCommentsDetails tbody').append(row);
                    }
                }
                HideProgress();
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

function GetRejectTrimsCommentsById() {
    ShowProgress();
    var obj = {};
    obj.Id = $('#txtprocessId')[0].innerText;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCommentsById';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetCommentsDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $('#tblPreviousRejectTrimsCommentsDetails tbody').html('');
                for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'Rejected') {
                        var row = "<tr role='row'>";
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                        if (result.listclsMerchantTnaDetailsModel[i].Attachment != "") {
                            row += "<td><a href='/Docs/Comments/" + result.listclsMerchantTnaDetailsModel[i].Attachment + "' download><i class='fa fa-paperclip' style='float:center'></i></a></td>"
                        }
                        else {
                            row += "<td></td>";
                        }
                        row += "</tr>";
                        $('#tblPreviousRejectTrimsCommentsDetails tbody').append(row);
                    }
                }
                HideProgress();
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
function GetApprovedTrimsCommentsById() {
    ShowProgress();
    var obj = {};
    obj.Id = $('#txtprocessId')[0].innerText;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCommentsById';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetCommentsDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $('#tblPreviousApproveTrimsCommentsDetails tbody').html('');
                for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'Approved') {
                        var row = "<tr role='row'>";
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                        if (result.listclsMerchantTnaDetailsModel[i].Attachment != "") {
                            row += "<td><a href='/Docs/Comments/" + result.listclsMerchantTnaDetailsModel[i].Attachment + "' download><i class='fa fa-paperclip' style='float:center'></i></a></td>"
                        }
                        else {
                            row += "<td></td>";
                        }
                        row += "</tr>";
                        $('#tblPreviousApproveTrimsCommentsDetails tbody').append(row);
                    }
                }
                HideProgress();
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

function GetMerchantCommentsById(ProcessId) {
    ShowProgress();
    var obj = {};
    obj.Id = ProcessId;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetCommentsById';
    $.ajax({
        url: "/MerchantTna/MerchantTna/GetCommentsDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $('#tblPreviousCommentsDetail tbody').html('');
                for (var i = 0; i < result.listclsMerchantTnaDetailsModel.length; i++) {
                    var row = "<tr role='row'>";
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'General') {
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                    }
                    else {
                        row += "<td></td>";
                    }
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'Approved') {
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                    }
                    else {
                        row += "<td></td>";
                    }
                    if (result.listclsMerchantTnaDetailsModel[i].CommentsType == 'Rejected') {
                        row += "<td>" + result.listclsMerchantTnaDetailsModel[i].Comments + "</td>";
                    }
                    else {
                        row += "<td></td>";
                    }
                    if (result.listclsMerchantTnaDetailsModel[i].Attachment != "") {
                        row += "<td><a href='/Docs/Comments/" + result.listclsMerchantTnaDetailsModel[i].Attachment + "' download><i class='fa fa-paperclip' style='float:center'></i></a></td>"
                    }
                    else {
                        row += "<td></td>";
                    }
                    row += "</tr>";
                    $('#tblPreviousCommentsDetail tbody').append(row);
                }
                HideProgress();
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
$('#btnReject').click(function () {
    $('#modal-RejectComments').modal({ backdrop: 'static', keyboard: false });
    $('#modal-RejectComments').modal('toggle');
    GetRejectCommentsById();
})
$('#btnApproved').click(function () {
    $('#modal-ApproveComments').modal({ backdrop: 'static', keyboard: false });
    $('#modal-ApproveComments').modal('toggle');
    GetApprovedTrimsCommentsById();
})
$('#btnTrimsReject').click(function () {
    $('#modal-RejectTrimsComments').modal({ backdrop: 'static', keyboard: false });
    $('#modal-RejectTrimsComments').modal('toggle');
    GetRejectTrimsCommentsById();
})
$('#btnTrimsApproved').click(function () {
    $('#modal-ApproveTrimsComments').modal({ backdrop: 'static', keyboard: false });
    $('#modal-ApproveTrimsComments').modal('toggle');
    GetRejectCommentsById();
})
function ApproveProcess(data) {
    var ProcessColor = $('#txtsprocessColor').val();
    $('#btnApproved').attr('disabled', true);
    $('#btnReject').attr('disabled', true);
    //if (ProcessColor == 'red') {
    //    toastr.error('Please add comments before active process.');
    //}
    //else {
    //var d = new Date();
    //var month = d.getMonth() + 1;
    //var day = d.getDate();
    //var output =
    //    (month < 10 ? '0' : '') + month + '/' +
    //    (day < 10 ? '0' : '') + day + '/' + d.getFullYear();
    //$('#currentDate')[0].innerText = ConvertDateDDMMYYYY(output);
    //$('#btnGetDate').attr('disabled', true);
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'ApproveProcessStatus';
    //obj.CompletedDate = output;
    //obj.CompletedDateType = $('#ProcessDatetype')[0].innerText;
    obj.ProcessName = $('#txtprocessname').val();
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    obj.TemplateName = $('#txtTemplateName')[0].innerText;
    $.ajax({
        url: "/MerchantTna/MerchantTna/ApproveStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                toastr.success('Process approved successfully.');
                HideProgress();
                var OcnNo = $('#txtOcnNo')[0].innerText;
                var TemplateName = $('#txtTemplateName')[0].innerText;
                var Department = $('#txtDepartment')[0].innerText;
                $('#modal-GetProcessData').modal({ backdrop: 'static', keyboard: false });
                GetDataByDeptOnDate(OcnNo, TemplateName, Department);

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
    })
    //}
};

function ApproveTrimsProcess(data) {
    var ProcessColor = $('#txtsprocessColor').val();
    $('#btnTrimsApproved').attr('disabled', true);
    $('#btnTrimsReject').attr('disabled', true);
    //if (ProcessColor == 'red') {
    //    toastr.error('Please add comments before active process.');
    //}
    //else {
    //var d = new Date();
    //var month = d.getMonth() + 1;
    //var day = d.getDate();
    //var output =
    //    (month < 10 ? '0' : '') + month + '/' +
    //    (day < 10 ? '0' : '') + day + '/' + d.getFullYear();
    //$('#currentDate')[0].innerText = ConvertDateDDMMYYYY(output);
    //$('#btnGetDate').attr('disabled', true);
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'ApproveTrimsProcessStatus';
    //obj.CompletedDate = output;
    //obj.CompletedDateType = $('#ProcessDatetype')[0].innerText;
    obj.ProcessName = $('#txtprocessname').val();
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    obj.TemplateName = $('#txtTemplateName')[0].innerText;
    $.ajax({
        url: "/MerchantTna/MerchantTna/ApproveTrimsProcess",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                toastr.success('Process approved successfully.');
                HideProgress();
                var OcnNo = $('#txtOcnNo')[0].innerText;
                var TemplateName = $('#txtTemplateName')[0].innerText;
                var Department = $('#txtDepartment')[0].innerText;
                $('#modal-GetProcessData').modal({ backdrop: 'static', keyboard: false });
                GetDataByDeptOnDate(OcnNo, TemplateName, Department);

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
    })
    //}
};
function CloseModel() {
    $('#modal-ApprovalProcessData').modal("hide");
}
function RejectProcess(data) {
    var ProcessColor = $('#txtsprocessColor').val();
    $('#btnApproved').attr('disabled', true);
    $('#btnReject').attr('disabled', true);
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'RejectProcessStatus';
    obj.ProcessName = $('#txtprocessname').val();
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    obj.TemplateName = $('#txtTemplateName')[0].innerText;
    $.ajax({
        url: "/MerchantTna/MerchantTna/RejectStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                toastr.success('Process rejected successfully.');
                HideProgress();
                var OcnNo = $('#txtOcnNo')[0].innerText;
                var TemplateName = $('#txtTemplateName')[0].innerText;
                var Department = $('#txtDepartment')[0].innerText;
                $('#modal-GetProcessData').modal({ backdrop: 'static', keyboard: false });
                GetDataByDeptOnDate(OcnNo, TemplateName, Department);

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
    })
    //}
};
function RejectTrimsProcess(data) {
    var ProcessColor = $('#txtsprocessColor').val();
    $('#btnTrimsApproved').attr('disabled', true);
    $('#btnTrimsReject').attr('disabled', true);
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'RejectTrimsProcess';
    obj.ProcessName = $('#txtprocessname').val();
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    obj.TemplateName = $('#txtTemplateName')[0].innerText;
    $.ajax({
        url: "/MerchantTna/MerchantTna/RejectTrimsProcess",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                toastr.success('Process rejected successfully.');
                HideProgress();
                var OcnNo = $('#txtOcnNo')[0].innerText;
                var TemplateName = $('#txtTemplateName')[0].innerText;
                var Department = $('#txtDepartment')[0].innerText;
                $('#modal-GetProcessData').modal({ backdrop: 'static', keyboard: false });
                GetDataByDeptOnDate(OcnNo, TemplateName, Department);

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
    })
    //}
};
function ApproveRequest(data) {
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'ApproveRequest';
    obj.ProcessName = $('#txtprocessname').val();
    obj.OcnNo = $('#txtOcnNo')[0].innerText;
    obj.TemplateName = $('#txtTemplateName')[0].innerText;
    $.ajax({
        url: "/MerchantTna/MerchantTna/ApproveRequest",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                toastr.success('Details updated successfully.');
                HideProgress();
                var OcnNo = $('#txtOcnNo')[0].innerText;
                var TemplateName = $('#txtTemplateName')[0].innerText;
                var Department = $('#txtDepartment')[0].innerText;
                $('#modal-GetProcessData').modal({ backdrop: 'static', keyboard: false });
                //GetDataByDeptOnDate(OcnNo, TemplateName, Department);
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
    })
    //}
};
