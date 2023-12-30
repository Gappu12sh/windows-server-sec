
function ErrorCodes(errorCode) {
    switch (errorCode) {
        case "E005":
            return "Record already exist";
        case "E006":
            return "Sorry, You don't have permission!";
    }
}

function SuccessMessage() {
    return "Saved successfully";
}
function UpdateMessage() {
    return "Updated successfully";
}
function NoRecordMessage() {
    return "No record found";
}
function DeleteMessage() {
    return "Record deleted successfully";
}
function IdBlank() {
    return "No details found for this record.";
}
function IsModuleSelected() {
    return "Please allow module.";
}
function NotPermission() {
    return "You don't have permission";
}

