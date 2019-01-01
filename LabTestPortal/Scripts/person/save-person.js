function validateForm(btn, formId) {
    var form = document.getElementById(formId);
    var inputs = form.getElementsByTagName("input");
    var countIndex = 0;
    btn.type = 'submit';

    while (countIndex < inputs.length) {
        if (inputs[countIndex].type === "date") {
            var dob = new Date(inputs[countIndex].value);
            var dateToday = new Date();
            if (dob > dateToday) {
                inputs[countIndex].setAttribute('oninvalid', 'this.setCustomValidity(\'Date of birth must be earlier or equal to date today.\')');
                inputs[countIndex].setAttribute('oninput', 'this.setCustomValidity(\'\')');
            }
        }
        countIndex++;
    }

    if (form.checkValidity()) {
        btn.type = 'submit';
        form.submit();
    } else {
        //for chrome and firefox
        if (typeof form.reportValidity !== 'undefined') {
            form.reportValidity();
        }
        //for IE
        else {
            btn.click();
        }
    }
}

function validateName(input) {
    var regex = new RegExp(/^[a-zA-Z\s]{1,50}$/);
    if (!regex.test(input.value)) {
        input.setAttribute('oninvalid', 'this.setCustomValidity(\'Valid name and minimum of 1 character and maximum of 50\')');
        input.setAttribute('oninput', 'validateName(this); this.setCustomValidity(\'\');');
        input.setAttribute('pattern', '^[a-zA-Z\\s]{1,50}$');
    }
    else {
        input.setAttribute('oninput', 'validateName(this)');
    }
}