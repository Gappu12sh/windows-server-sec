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

    if ($('#hdnPageLoadOption').val() == 'ViewTrimsTemplateByOcnDetails') {
        GetDetails();
        
    }
    if ($('#hdnPageLoadOption').val() == 'LoadEditTrimsTemplateByOcn') {
        if ($('#hiddenTrimsTemplateByOcnId').val() != '') {
            bindMasters();            
            EditTrimsTemplateByOcn($('#hiddenTrimsTemplateByOcnId').val());
        }
    }

});
$('#btnAddTrimsTemplateByOcn').click(function () {
    $('#btnSave').css('display', 'none');
    $('#btnUpdate').css('display', 'none');
    $('#header')[0].innerText = "Add TrimsTemplateByOcn";
    Refresh();
    bindMasters();
    GetOcnList();
    $('#modal-lg').modal('toggle');
    $("#ddlTemplate").prop('disabled', false);
    $("#ddlOcn").prop('disabled', false);
});
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
    debugger;
    e.preventDefault();
    var url = "/TrimsTemplateByOcn/TrimsTemplateByOcn/AddEditDeleteTrimsTemplateByOcn";
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'Add';
    var TrimsTemplateData = $('#tblTrimsTemplateByOcnDetails tbody tr:has(td)').map(function () {
        var $td = $('td', this);
        var checkbox = $('.chk', this);
        if (checkbox[0].checked == true) {
            return {
                ProcessName: $td[3].innerText,
                StartDay: $td[4].children[0].value,
                EndDay: $td[5].children[0].value,
                ScheduleStartDate: ConvertDateFormat($td[6].innerText),
                ScheduleEndDate: ConvertDateFormat($td[7].innerText),
                BuyerId: $td[8].innerText,
                OcnNo: $td[9].innerText,
                TemplateId: $td[10].innerText,
                DepartmentName: $td[11].innerText,
                ProcessWay: $td[12].innerText,
            }
        }
    }).get();
        ShowProgress();
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "data": JSON.stringify(obj), "templatedetails": JSON.stringify(TrimsTemplateData) }),
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

function Refresh() {
    $('#ddlOcn').val('0');
    $('#ddlTemplate').val('0');
    $('#tblTrimsTemplateByOcnDetails thead').html('');
    $('#tblTrimsTemplateByOcnDetails tbody').html('');
}

function GetDetails() {
    debugger;
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'View';
    $.ajax({
        url: "/TrimsTemplateByOcn/TrimsTemplateByOcn/GetDetails",
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

function ViewTrimsTemplateByOcn(OcnNo, TemplateId) {
    $('#modal-view').modal('toggle');
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'ViewTemplate';
    obj.OcnNo = OcnNo;
    obj.TemplateId = TemplateId;
    $.ajax({
        url: "/TrimsTemplateByOcn/TrimsTemplateByOcn/GetViewDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                bindViewTemplate(result);
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
    debugger;
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetOcnList';
    $.ajax({
        url: "/TrimsTemplateByOcn/TrimsTemplateByOcn/GetOcnList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                $("#ddlOcn").html('');
                $("#ddlOcn").append('<option value=0>-Select-</option>');
                for (var i = 0; i < result.clsGetOcnList.length; i++)
                {
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
function GetTemplateData()
{
    debugger;
    ShowProgress();
    var obj = {};
    obj.Id = $("#ddlTemplate").val();
    obj.OcnNo = $("#ddlOcn").val();
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'GetTemplateData';
    $.ajax({
        url: "/TrimsTemplateByOcn/TrimsTemplateByOcn/GetTemplateData",
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

function DeleteTrimsTemplateByOcn(OcnNo,TemplateId) {
    
        ShowProgress();
        var obj = {};
        obj.Id = '0';
        obj.UserId = $('#hiddenUserId').val();
        obj.Operation = 'Delete';
        obj.OcnNo = OcnNo;
        obj.TemplateId = TemplateId;
        $.ajax({
            url: "/TrimsTemplateByOcn/TrimsTemplateByOcn/DeleteDetails",
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

function bindData(result) {
    $('#tblTrimsTemplateByOcnData thead').html('');
    $('#tblTrimsTemplateByOcnData tbody').html('');
    if (result.listclsTrimsTemplateByOcnDetailsModel.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Buyer: activate to sort column descending'> Buyer </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Template : activate to sort column descending'> Template </th>";       
        thead += "<th> Edit </th>";
        thead += "<th> Delete </th>";
        thead += "</tr>";
        $('#tblTrimsTemplateByOcnData thead').append(thead);
        for (var i = 0; i < result.listclsTrimsTemplateByOcnDetailsModel.length; i++) {
            var row = "<tr role='row'>";
            row += "<td class='sorting_1' id='TrimsTemplateByOcnId" + result.listclsTrimsTemplateByOcnDetailsModel[i].Id + "' style='display:none'>" + result.listclsTrimsTemplateByOcnDetailsModel[i].Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].BuyerName + "</td>";          
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].TemplateName + "</td>";
            //row += "<td><a href='/Size/EditSize/Index?id=" + result.listclsSizeDetailsModel[i].Id + "'><img src='../Images/edit.png' style='width:15px; height:15px'/></a></td>";            
            row += "<td><img onclick=EditTrimsTemplateByOcn('" + result.listclsTrimsTemplateByOcnDetailsModel[i].OcnNo.toString() + "'," + result.listclsTrimsTemplateByOcnDetailsModel[i].TemplateId + ") src='/Images/edit.png' style='width:20px; height:20px'/></td>";
            row += "<td><img onclick=DeleteTrimsTemplateByOcn('" + result.listclsTrimsTemplateByOcnDetailsModel[i].OcnNo.toString() + "'," + result.listclsTrimsTemplateByOcnDetailsModel[i].TemplateId + ") src='/Images/delete.png' style='width:20px; height:20px'/></td>";
            row += "</tr>";
            $('#tblTrimsTemplateByOcnDataBody').append(row);
        }
        HideProgress();
        $('#tblTrimsTemplateByOcnData').DataTable();
    }
    else {
        HideProgress();
    }
}

function bindTemplate(result) {
   
    $('#tblTrimsTemplateByOcnDetails thead').html('');
    $('#tblTrimsTemplateByOcnDetails tbody').html('');
    if (result.clsGetTrimsTemplateData.length > 0) {
        $('#txtBoDate')[0].innerHTML = result.listclsTrimsTemplateByOcnDetailsModel[0].ScheduleStartDate;
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'></th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Process Name: activate to sort column descending'> Process Name </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Start Day : activate to sort column descending'> Start Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='End Day : activate to sort column descending'> End Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleStartDate : activate to sort column descending'> ScheduleStartDate </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblTrimsTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleEndDate : activate to sort column descending'> ScheduleEndDate </th>";       
        thead += "<th style='display:none'> BuyerId </th>";
        thead += "<th style='display:none'> OcnNo </th>";
        thead += "<th style='display:none'> TemplateId </th>";
        thead += "<th style='display:none'> DepartmentName </th>";
        thead += "<th style='display:none'> ProcessWay </th>";
        thead += "</tr>";
        $('#tblTrimsTemplateByOcnDetails thead').append(thead);
        var EndDate;
        for (var i = 0; i < result.clsGetTrimsTemplateData.length; i++) {
            var row = "<tr role='row'>";
            row += "<td><input class='chk' type='checkbox' id='chkbox" + i + "' checked='checked'></td>";
            row += "<td class='sorting_1' id='TrimsTemplateByOcnId" + result.clsGetTrimsTemplateData[i].Id + "' style='display:none'>" + result.clsGetTrimsTemplateData[i].Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";           
            row += "<td>" + result.clsGetTrimsTemplateData[i].ProcessName + "</td>";
            row += "<td><input type='text' class='form-control' name='Start' placeholder='Enter ...' id='txtStartDay" + i + "' onchange='ChangeDate(this)' value='" + result.clsGetTrimsTemplateData[i].StartDay + "'></td>";
            row += "<td><input type='text' class='form-control' name='End' placeholder='Enter ...' id='txtEndDay" + i + "' onchange='ChangeDate(this)' value='" + result.clsGetTrimsTemplateData[i].EndDay + "'></td>";
            // Date function to calculate date difference //
            var StartDate;
            var SDate = ConvertDateFormat(result.listclsTrimsTemplateByOcnDetailsModel[0].ScheduleStartDate);
            var startdate = new Date(SDate),
            days = parseInt(result.clsGetTrimsTemplateData[i].StartDay, 10);
            if (!isNaN(startdate.getTime())) {
                startdate.setDate(startdate.getDate() + days);
                var yyyy = startdate.getFullYear().toString();
                var mm = (startdate.getMonth() + 1).toString();
                var dd = startdate.getDate().toString();
                StartDate = (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy;
            } else {
                alert("Invalid Date");
            }
            var EndDate;
            var EDate = ConvertDateFormat(result.listclsTrimsTemplateByOcnDetailsModel[0].ScheduleStartDate);
            var enddate = new Date(EDate),
            days = parseInt(result.clsGetTrimsTemplateData[i].EndDay, 10);
            if (!isNaN(enddate.getTime())) {
                enddate.setDate(enddate.getDate() + days);
                var yyyy = enddate.getFullYear().toString();
                var mm = (enddate.getMonth() + 1).toString();
                var dd = enddate.getDate().toString();
                EndDate = (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy;
            } else {
                alert("Invalid Date");
            }
            row += "<td id='startdate" + i + "'>" + StartDate + "</td>";
            row += "<td id='enddate" + i + "'>" + EndDate + "</td>";
            row += "<td style='display:none'>" + result.listclsTrimsTemplateByOcnDetailsModel[0].BuyerId + "</td>";
            row += "<td style='display:none'>" + $('#ddlOcn option:selected').val() + "</td>";
            row += "<td style='display:none'>" + $('#ddlTemplate option:selected').val() + "</td>";
            row += "<td style='display:none'>" + result.clsGetTrimsTemplateData[i].DepartmentName + "</td>";
            row += "<td style='display:none'>" + result.clsGetTrimsTemplateData[i].ProcessWay + "</td>";
            row += "</tr>";
            $('#tblTrimsTemplateByOcnDetails').append(row);
        }
        HideProgress();
       
    }
    else {
        HideProgress();
    }
}
function bindViewTemplate(result) {
    $("#ddlviewOcn").append('<option>' + result.listclsTrimsTemplateByOcnDetailsModel[0].OcnNo + '</option>');
    $("#ddlviewTemplate").append('<option>' + result.listclsTrimsTemplateByOcnDetailsModel[0].TemplateName + '</option>');
    $('#txtviewbuyer').val(result.listclsTrimsTemplateByOcnDetailsModel[0].BuyerName);
    $('#tblviewTrimsTemplateByOcnDetails thead').html('');
    $('#tblviewTrimsTemplateByOcnDetails tbody').html('');
    if (result.listclsTrimsTemplateByOcnDetailsModel.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblviewTrimsTemplateByOcnDetails' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblviewTrimsTemplateByOcnDetails' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Process Name: activate to sort column descending'> Process Name </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblviewTrimsTemplateByOcnDetails' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Start Day : activate to sort column descending'> Start Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblviewTrimsTemplateByOcnDetails' rowspan='1' colspan='1' aria-sort='ascending' aria-label='End Day : activate to sort column descending'> End Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblviewTrimsTemplateByOcnDetails' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleStartDate : activate to sort column descending'> ScheduleStartDate </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblviewTrimsTemplateByOcnDetails' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleEndDate : activate to sort column descending'> ScheduleEndDate </th>";
        thead += "</tr>";
        $('#tblviewTrimsTemplateByOcnDetails thead').append(thead);
        var EndDate;
        for (var i = 0; i < result.listclsTrimsTemplateByOcnDetailsModel.length; i++) {
            var row = "<tr role='row'>";
            row += "<td class='sorting_1' id='MerchantTemplateByOcnId" + result.listclsTrimsTemplateByOcnDetailsModel[i].Id + "' style='display:none'>" + result.listclsTrimsTemplateByOcnDetailsModel[i].Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].ProcessName + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].StartDay + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].EndDay + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].ScheduleStartDate + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].ScheduleEndDate + "</td>";
            row += "</tr>";
            $('#tblviewTrimsTemplateByOcnDetails').append(row);
        }
        HideProgress();

    }
    else {
        HideProgress();
    }
}



function EditTrimsTemplateByOcn(OcnNo, TemplateId) {
    debugger;
    ShowProgress();
    var obj = {};
    obj.Id = '0';
    obj.UserId = $('#hiddenUserId').val();
    obj.OcnNo = OcnNo;
    obj.TemplateId = TemplateId;
    obj.Operation = 'Edit';
    $.ajax({
        url: "/TrimsTemplateByOcn/TrimsTemplateByOcn/GetEditDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj) }),
        type: "Post",
        success: function (result) {
            var a = result;
            if (result.ResponseCode == 100) {
                bindMasters();
                $('#btnSave').css('display', 'none');
                $('#btnUpdate').css('display', 'none');
                $('#header')[0].innerText = "Edit Template";
                $('#modal-lg').modal('toggle');
                bindEditTemplate(result);
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
function bindEditTemplate(result) {
    $('#tblTrimsTemplateByOcnDetails thead').html('');
    $('#tblTrimsTemplateByOcnDetails tbody').html('');
    $("#ddlOcn").append('<option value=' + result.listclsTrimsTemplateByOcnDetailsModel[0].BoOcnId + '>' + result.listclsTrimsTemplateByOcnDetailsModel[0].BoOcnNo + '</option>');
    //$("#ddlOcn").append('<option>' + result.listclsTrimsTemplateByOcnDetailsModel[0].OcnNo + '</option>');
    $("#ddlOcn").val(result.listclsTrimsTemplateByOcnDetailsModel[0].OcnNo).attr('selected', 'selected');
    $("#ddlTemplate").val(result.listclsTrimsTemplateByOcnDetailsModel[0].TemplateId).attr('selected', 'selected');
    $("#ddlTemplate").prop('disabled', true);
    $("#ddlOcn").prop('disabled', true);
    if (result.listclsTrimsTemplateByOcnDetailsModel.length > 0) {
        var thead = "<tr role='row'>";
        thead += "<th style='display:none'>  </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'></th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Sr No: activate to sort column descending'> Sr.No. </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Process Name: activate to sort column descending'> Process Name </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='Start Day : activate to sort column descending'> Start Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='End Day : activate to sort column descending'> End Day </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleStartDate : activate to sort column descending'> ScheduleStartDate </th>";
        thead += "<th class='sorting_asc' tabindex='0' aria-controls='tblMerchantTemplateByOcnData' rowspan='1' colspan='1' aria-sort='ascending' aria-label='ScheduleEndDate : activate to sort column descending'> ScheduleEndDate </th>";
        thead += "<th style='display:none'> BuyerId </th>";
        thead += "<th style='display:none'> OcnNo </th>";
        thead += "<th style='display:none'> TemplateId </th>";
        thead += "<th style='display:none'> DepartmentName </th>";
        thead += "</tr>";
        $('#tblTrimsTemplateByOcnDetails thead').append(thead);

        for (var i = 0; i < result.listclsTrimsTemplateByOcnDetailsModel.length; i++) {           
            $('#txtBoDate')[0].innerHTML = result.listclsTrimsTemplateByOcnDetailsModel[0].BoDate;
            var row = "<tr role='row'>";
            if (result.listclsTrimsTemplateByOcnDetailsModel[i].CompletedStartStatus == "Completed") {
                row += "<td><input class='chk' type='checkbox' disabled id='chkbox" + i + "' checked='checked'></td>";
            }
            else {
                if (result.listclsTrimsTemplateByOcnDetailsModel[i].IsActive == 0) {
                    row += "<td><input class='chk' type='checkbox' onchange='UpdateActiveProcess(this)' id='chkbox" + i + "'></td>";
                }
                else {
                    row += "<td><input class='chk' type='checkbox' onchange='UpdateActiveProcess(this)' id='chkbox" + i + "' checked='checked'></td>";
                }
            }
            row += "<td class='sorting_1' id='MerchantTemplateByOcnId" + result.listclsTrimsTemplateByOcnDetailsModel[i].Id + "' style='display:none'>" + result.listclsTrimsTemplateByOcnDetailsModel[i].Id + "</td>";
            row += "<td>" + (parseInt(i) + parseInt(1)) + "</td>";
            row += "<td>" + result.listclsTrimsTemplateByOcnDetailsModel[i].ProcessName + "</td>";
            if (result.listclsTrimsTemplateByOcnDetailsModel[i].CompletedStartStatus == "Completed") {
                row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtStartDay" + i + "' disabled value='" + result.listclsTrimsTemplateByOcnDetailsModel[i].StartDay + "'></td>";
            }
            else {
                if (result.listclsTrimsTemplateByOcnDetailsModel[i].IsActive == 0) {
                    row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtStartDay" + i + "' onchange='UpdateStartProcess(this)' disabled value='" + result.listclsTrimsTemplateByOcnDetailsModel[i].StartDay + "'></td>";
                }
                else
                {
                    row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtStartDay" + i + "' onchange='UpdateStartProcess(this)' value='" + result.listclsTrimsTemplateByOcnDetailsModel[i].StartDay + "'></td>";
                }
            }
            if (result.listclsTrimsTemplateByOcnDetailsModel[i].CompletedStartStatus == "Completed") {
                row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtEndDay" + i + "' disabled value='" + result.listclsTrimsTemplateByOcnDetailsModel[i].EndDay + "'></td>";
            }
            else {
                if (result.listclsTrimsTemplateByOcnDetailsModel[i].IsActive == 0) {
                    row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtEndDay" + i + "' onchange='UpdateEndProcess(this)' disabled value='" + result.listclsTrimsTemplateByOcnDetailsModel[i].EndDay + "'></td>";
                }
                else
                {
                    row += "<td><input type='text' class='form-control' placeholder='Enter ...' id='txtEndDay" + i + "' onchange='UpdateEndProcess(this)' value='" + result.listclsTrimsTemplateByOcnDetailsModel[i].EndDay + "'></td>";
                }
            }
            row += "<td id='startdate" + i + "'>" + result.listclsTrimsTemplateByOcnDetailsModel[i].ScheduleStartDate + "</td>";
            row += "<td id='enddate" + i + "'>" + result.listclsTrimsTemplateByOcnDetailsModel[i].ScheduleEndDate + "</td>";
            row += "<td style='display:none'>" + result.listclsTrimsTemplateByOcnDetailsModel[0].BuyerId + "</td>";
            row += "<td style='display:none'>" + $('#ddlOcn option:selected').val() + "</td>";
            row += "<td style='display:none'>" + $('#ddlTemplate option:selected').val() + "</td>";
            row += "<td style='display:none'>" + result.listclsTrimsTemplateByOcnDetailsModel[i].DepartmentName + "</td>";
            row += "</tr>";
            $('#tblTrimsTemplateByOcnDetails').append(row);
        }
        HideProgress();

    }
    else {
        HideProgress();
    }
}

function UpdateStartProcess(data) {
    var id = data.id.replace('txtStartDay', '');
    var tr = $('#tblTrimsTemplateByOcnDetails tbody tr');
    var $tr = tr[id];
    var Id = $tr.cells[1].innerText;
    var ocnno = $("#ddlOcn").val();
    var templateid = $("#ddlTemplate").val();
    var StartDay = $tr.cells[4].firstChild.value;
    var startDateId = $tr.cells[6].id;
    var StartDate;
    var SDate = ConvertDateFormat($('#txtBoDate')[0].innerText);
    var startdate = new Date(SDate),
    days = parseInt(StartDay, 10);
    if (!isNaN(startdate.getTime())) {
        startdate.setDate(startdate.getDate() + days);
        var yyyy = startdate.getFullYear().toString();
        var mm = (startdate.getMonth() + 1).toString();
        var dd = startdate.getDate().toString();
        StartDate = (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy;
    } else {
        alert("Invalid Date");
    }
    if (StartDay > 0) {

        $('#' + startDateId)[0].innerHTML = StartDate;
        var MerchantTemplateData = "";
        var url = "/TrimsTemplateByOcn/TrimsTemplateByOcn/AddEditDeleteTrimsTemplateByOcn";
        var obj = {};
        obj.Id = Id;
        obj.OcnNo = ocnno;
        obj.TemplateId = templateid;
        obj.UserId = $('#hiddenUserId').val();
        obj.Operation = 'UpdateStartProcess';
        obj.StartDay = StartDay;
        obj.ScheduleStartDate = ConvertDateFormat(StartDate);
        ShowProgress();
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "data": JSON.stringify(obj), "templatedetails": JSON.stringify(MerchantTemplateData) }),
            type: "Post",
            success: function (result) {
                toastr.success(result.ResponseMessage);
                $('#modal-lg').modal({ backdrop: 'static', keyboard: false });
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
        toastr.error("Enter Start days");
    }
}

function UpdateEndProcess(data) {
    var id = data.id.replace('txtEndDay', '');
    var tr = $('#tblTrimsTemplateByOcnDetails tbody tr');
    var $tr = tr[id];
    var Id = $tr.cells[1].innerText;
    var ocnno = $("#ddlOcn").val();
    var templateid = $("#ddlTemplate").val();
    var EndDay = $tr.cells[5].firstChild.value;
    var endDateId = $tr.cells[7].id;
    var StartDate;
    var SDate = ConvertDateFormat($('#txtBoDate')[0].innerText);
    var startdate = new Date(SDate),
    days = parseInt(EndDay, 10);
    if (!isNaN(startdate.getTime())) {
        startdate.setDate(startdate.getDate() + days);
        var yyyy = startdate.getFullYear().toString();
        var mm = (startdate.getMonth() + 1).toString();
        var dd = startdate.getDate().toString();
        StartDate = (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy;
    } else {
        alert("Invalid Date");
    }
    var MerchantTemplateData = "";
    if (EndDay > 0) {

        $('#' + endDateId)[0].innerHTML = StartDate;
        var url = "/TrimsTemplateByOcn/TrimsTemplateByOcn/AddEditDeleteTrimsTemplateByOcn";
        var obj = {};
        obj.Id = Id;
        obj.OcnNo = ocnno;
        obj.TemplateId = templateid;
        obj.UserId = $('#hiddenUserId').val();
        obj.Operation = 'UpdateEndProcess';
        obj.EndDay = EndDay;
        obj.ScheduleEndDate = ConvertDateFormat(StartDate);
        ShowProgress();
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "data": JSON.stringify(obj), "templatedetails": JSON.stringify(MerchantTemplateData) }),
            type: "Post",
            success: function (result) {
                toastr.success(result.ResponseMessage);
                $('#modal-lg').modal({ backdrop: 'static', keyboard: false });
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
        toastr.error("Enter End day");
    }
}

function UpdateActiveProcess(data) {
    var id = data.id.replace('chkbox', '');
    var tr = $('#tblTrimsTemplateByOcnDetails tbody tr');
    var $tr = tr[id];
    var Id = $tr.cells[1].innerText;
    var ocnno = $("#ddlOcn").val();
    var templateid = $("#ddlTemplate").val();
    var startDayId = $tr.cells[4].firstChild.id;
    var endDayId = $tr.cells[5].firstChild.id;
    var MerchantTemplateData = "";
    var isactive = '';
    if (data.checked == true) {
        isactive = 1;
        $('#' + startDayId).prop('disabled', false);
        $('#' + endDayId).prop('disabled', false);
    }
    else {
        isactive = 0;
        $('#' + startDayId).prop('disabled', true);
        $('#' + endDayId).prop('disabled', true);
    }

    var url = "/TrimsTemplateByOcn/TrimsTemplateByOcn/AddEditDeleteTrimsTemplateByOcn";
    var obj = {};
    obj.Id = Id;
    obj.OcnNo = ocnno;
    obj.TemplateId = templateid;
    obj.UserId = $('#hiddenUserId').val();
    obj.Operation = 'UpdateActiveProcess';
    obj.IsActive = isactive;

    ShowProgress();
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "data": JSON.stringify(obj), "templatedetails": JSON.stringify(MerchantTemplateData) }),
        type: "Post",
        success: function (result) {
            toastr.success(result.ResponseMessage);
            $('#modal-lg').modal({ backdrop: 'static', keyboard: false });
            HideProgress();
        },
        error: function (msg) {
            toastr.error(msg);
            HideProgress();
            return false;
        }
    });
}

function ChangeDate(data) {
    var type = data.attributes.name.value;
    var tr = $('#tblTrimsTemplateByOcnDetails tbody tr');

    var id = '';
    var Days = '';
    var DateId = '';
    if (type == 'Start') {
        id = data.id.replace('txtStartDay', '');
        var $tr = tr[id];
        Days = $tr.cells[4].firstChild.value;
        DateId = $tr.cells[6].id;
    }
    else {
        id = data.id.replace('txtEndDay', '');
        var $tr = tr[id];
        Days = $tr.cells[5].firstChild.value;
        DateId = $tr.cells[7].id;
    }
    var StartDate;
    var SDate = ConvertDateFormat($('#txtBoDate')[0].innerText);
    var startdate = new Date(SDate),
    days = parseInt(Days, 10);
    if (!isNaN(startdate.getTime())) {
        startdate.setDate(startdate.getDate() + days);
        var yyyy = startdate.getFullYear().toString();
        var mm = (startdate.getMonth() + 1).toString();
        var dd = startdate.getDate().toString();
        StartDate = (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy;
    } else {
        alert("Invalid Date");
    }
    $('#' + DateId)[0].innerHTML = StartDate;

}