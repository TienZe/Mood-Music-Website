/*var emailLogin = document.querySelector("#email-login");*/
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

function ToggleBar()
{
    Down.classList.toggle("hide");
}

if (BarDown) {
    BarDown.addEventListener("click", ToggleBar);
}













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