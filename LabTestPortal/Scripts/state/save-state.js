function validateState(input) {
    var regex = new RegExp(/^[a-zA-Z]{2}$/);
    input.setAttribute('oninput', 'this.setCustomValidity(\'\')');
    if (!regex.test(input.value)) {
        input.setAttribute('oninvalid', 'this.setCustomValidity(\'Valid state code and must consist of 2 characters\')');
        input.setAttribute('oninput', 'validateState(this)');
        input.setAttribute('pattern', '^[a-zA-Z]{2}$');
    }
}