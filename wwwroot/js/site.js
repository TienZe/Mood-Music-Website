﻿/*var emailLogin = document.querySelector("#email-login");*/
/*var passwordLogin = document.querySelector("#password-login");*/

//var emailSignup = document.querySelector("#email");
//var passwordSignup = document.querySelector("#password");
//var confirmPassword = document.querySelector("#confirm-password");
//var gender = document.querySelectorAll('input[name="Gender"]');
//var date = document.querySelector("#date");
//var form = document.querySelector("form");

//function showError(input, message) {
//    let parent = input.parentElement;
//    let small = parent.querySelector("small");
//    parent.classList.add("error");
//    small.innerText = message;
//}

//function showSuccess(input) {
//    let parent = input.parentElement;
//    let small = parent.querySelector("small");
//    parent.classList.remove("error");
//    small.innerText = "";
//}

//function checkEmptyError(listInput) {
//    let hasError = false;
//    listInput.forEach((input) => {
//        input.value = input.value.trim();

//        if (!input.value) {
//            showError(input, "Không được để trống !!!");
//            hasError = true;
//        } else {
//            showSuccess(input);
//        }
//    });
//    return hasError;
//}

//function checkEmailError(input) {
//    const regexEmail = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
//    input.value = input.value.trim();

//    if (!regexEmail.test(input.value)) {
//        showError(input, "Email Invalid");
//        return true;
//    }

//    showSuccess(input);
//    return false;
//}

//function checkLengthError(input, min, max) {
//    input.value = input.value.trim();

//    if (input.value.length < min) {
//        showError(input, `Phải có ít nhất ${min} ký tự`);
//        return true;
//    }

//    if (input.value.length > max) {
//        showError(input, `Không được quá ${max} ký tự`);
//        return true;
//    }

//    showSuccess(input);
//    return false;
//}

//function checkMatchPasswordError(passwordInput, cfPassword) {
//    if (passwordInput.value !== cfPassword.value) {
//        showError(cfPassword, "Mật khẩu không trùng khớp");
//        return true;
//    }
//    return false;
//}

//function checkGenderError(genderInput) {
//    let isChecked = false;
//    genderInput.forEach((input) => {
//        if (input.checked) {
//            isChecked = true;
//        }
//    });

//    if (!isChecked) {
//        showError(genderInput[0], "Không được để trống");
//        return true;
//    }

//    showSuccess(genderInput[0]);
//    return false;
//}

//function checkDateError(dateInput) {
//    const dateValue = new Date(dateInput.value);

//    if (isNaN(dateValue.getTime())) {
//        showError(dateInput, "Ngày tháng năm không hợp lệ");
//        return true;
//    }

//    showSuccess(dateInput);
//    return false;
//}

//form.addEventListener("submit", function (e) {
//    e.preventDefault();
//    let isEmailLoginError = checkEmailError(email);
//    let isPasswordLoginLengthError = checkLengthError(password, 6, 16);

//    if (
//        !isEmailLoginError &&
//        !isPasswordLoginLengthError
//    ) {
//        form.submit();
//    }
//});

//form.addEventListener("submit", function (e) {
//    e.preventDefault();

//    let isEmptyError = checkEmptyError([date]);
//    let isEmailSignupError = checkEmailError(email);
//    let isPasswordSignupLengthError = checkLengthError(password, 6, 16);
//    let isConfirmPasswordLengthError = checkLengthError(confirmPassword, 6, 16);
//    let isMatchError = checkMatchPasswordError(passwordSignup, confirmPassword);
//    let isGenderError = checkGenderError(gender);
//    let isDateError = checkDateError(date);

//    if (
//        !isEmptyError &&
//        !isEmailSignupError &&
//        !isPasswordSignupLengthError &&
//        !isConfirmPasswordLengthError &&
//        !isMatchError &&
//        !isGenderError &&
//        !isDateError
//    ) {
//        form.submit();
//    }
//});

var BarDown = document.querySelector(".fa-bars");
var Down = document.querySelector(".bars-down");
var InputChange = document.querySelectorAll(".fa-pen");

var changePassword = document.getElementById("ChangePassword");
var newPasswordRow = document.querySelector(".Manage-Profile_Show_NewPassword");
var newPassword = document.getElementById("New_Password");
var changePassword2 = document.getElementById("ChangePassword2");
var confirmPasswordRow = document.querySelectorAll(".Manage-Profile_Show_NewPassword")[1];
var confirmPassword = document.getElementById("Confirm_Password");
var icon = document.querySelector(".iconChangePassword");

var ResourcesList = document.querySelector("#Resources-list")
var ResourcesSong = document.querySelector("#Resources-Song")
var ResourcesGenre = document.querySelector("#Resources-Genre")
var ResourcesEmotion = document.querySelector("#Resources-Emotion")


function ToggleBar()
{
    Down.classList.toggle("hide");
}

if (BarDown) {
    BarDown.addEventListener("click", ToggleBar);
}


function EnableInput(inputId) {
    document.getElementById(inputId).readOnly = false;
}

// Define the ToggleInput function to toggle the visibility of password input fields


// Loop through the InputChange array and assign an event handler function to each element in the array
InputChange.forEach(function (input) {
    input.addEventListener("click", function () {
        // Get the value of the data-input-id attribute of the icon
        var inputId = input.getAttribute("data-input-id");
        // Call the EnableInput function with inputId as the argument if the inputId is one of the specified values
        EnableInput(inputId);
            // Otherwise, call the ToggleInput function to toggle the visibility of the password input fields
    });
});

function toggleNewPassword() {
    changePassword.classList.toggle("hide");
    newPasswordRow.classList.toggle("hide");
    changePassword2.classList.toggle("hide");
    confirmPasswordRow.classList.toggle("hide");
}

if (icon) {
    icon.addEventListener("click", toggleNewPassword);
}


// Resource list

ResourcesList.addEventListener("click", function () {
    ResourcesSong.classList.toggle("hide");
    ResourcesGenre.classList.toggle("hide");
    ResourcesEmotion.classList.toggle("hide");
})

// Add new song - list genre and emotion

var ListGenre = document.querySelector("#List-Genre");
var ListEmotion = document.querySelector("#List-Emotion");
var GenreItem = document.querySelector("#Genre-Item");
var EmotionItem = document.querySelector("#Emotion-Item");


ListGenre.addEventListener("click", function () {
    GenreItem.classList.toggle("hide");
    EmotionItem.classList.add("hide");
})

ListEmotion.addEventListener("click", function () {
    EmotionItem.classList.toggle("hide");
    GenreItem.classList.add("hide");
})

// Add new song - click icon folfer

var fileInput = document.querySelector("input[type='file']");
var fileSelect = document.querySelector('#folder');
var filePath = document.querySelector("#Directory-path");

fileSelect.addEventListener("click", (event) => {
    event.preventDefault(); // ngăn chặn sự kiện mặc định của phần tử i
    fileInput.click(); // tự động gọi sự kiện của phần tử input file
});

fileInput.addEventListener('change', (event) => {
    const selectedFile = event.target.files[0];
    filePath.value = selectedFile.name; // hiển thị đường dẫn của tệp tin đã chọn lên phần tử input text
});

// Add new song - check box

var checkboxes = document.querySelectorAll("input[type='checkbox']");
var CategoryItem = document.querySelectorAll(".Category-item");

function addCategory(checkboxCategory) {
    var newCategory = document.querySelector("#" + checkboxCategory);
    newCategory.classList.toggle("hide");
}

checkboxes.forEach(function (checkbox) {
    checkbox.addEventListener("change", function () {
        var checkboxCategory = this.getAttribute("checkbox-Category-id");
        addCategory(checkboxCategory);
    });
});

// Add new song - click icon x

var iconX = document.querySelectorAll(".Category-Show-item i");

function RemoveCategory(iconCategory) {
    var removeCategory = document.querySelector("#" + iconCategory);
    removeCategory.classList.add("hide");

    // bỏ tick của checkbox tương ứng
    var checkbox = document.querySelector("input[checkbox-Category-id='" + iconCategory + "']");
    checkbox.checked = false;
}

iconX.forEach(function (icon) {
    icon.addEventListener("click", function () {
        var iconCategory = icon.getAttribute("icon-Category-id");
        RemoveCategory(iconCategory);
    })
})



// List song using - delete row

//var DeleteRow = document.querySelectorAll(".ListSongUsing-table-data-del-edit i");

//DeleteRow.forEach(function (button) {
//    button.addEventListener("click", function () {
//        // Xác định dòng chứa thẻ i được nhấn
//        var row = button.parentNode.parentNode; // lấy thẻ tr chứa thẻ i

//        // Xóa dòng
//        row.remove();
//    })
//})