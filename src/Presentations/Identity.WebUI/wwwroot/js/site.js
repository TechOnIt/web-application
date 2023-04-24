const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

$('.password-control button').click(function () {
    debugger
    // Hide
    if ($(this).parent().find('input[type=password]').attr('type') == 'password') {
        $(this).parent().find('input[type=password]').attr('type', 'text')
        $(this).parent().find('i').addClass('fa-eye')
        $(this).parent().find('i').removeClass('fa-eye-slash')
    }
    // Show
    else {
        $(this).parent().find('input[type=text]').attr('type', 'password')
        $(this).parent().find('i').addClass('fa-eye-slash')
        $(this).parent().find('i').removeClass('fa-eye')
    }
});