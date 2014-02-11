$(function () {
    var username = $('#username'),
        password = $('#password');
    username.focusout(function () {
        if (username.val() === 'Username' || username.val() === '') {
            username.val('Username');
        } else {
            username.css({ 'color' : '#646464' });
        }
    }).focus(function () {
        if (username.val() === 'Username' || username.val() === '') {
            username.val('');
        }
    });

    password.focusout(function () {
        if (password.val() === 'Password' || password.val() === '') {
            password.val('Password');
        } else {
            password.css({ 'color' : '#646464' });
        }
    }).focus(function () {
        if (password.val() === 'Password' || password.val() === '') {
            password.val('');
        }
    });
});