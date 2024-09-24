$(document).ready(function () {
    $('#cover').on('change', function () {
        const file = this.files[0];
        if (file) {
            $('.cover-preview').attr('src', window.URL.createObjectURL(file)).removeClass('d-none');
        }
    });
});