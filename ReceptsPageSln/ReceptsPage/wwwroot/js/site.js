// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.dropdown-submenu a.test').on("click", function (e) {
        $(this).next('ul').toggle();
        e.stopPropagation();
        e.preventDefault();
    });
});

$(function () {
    $('#drop1').on('click', function () {

        $('.dropmenu2').removeClass('active');
        $('.dropmenu3').removeClass('active');
        $('.dropmenu1').toggleClass('active');
    });

    $('#drop2').on('click', function () {
        $('.dropmenu1').removeClass('active');
        $('.dropmenu3').removeClass('active');
        $('.dropmenu2').toggleClass('active');
    });

    $('#drop3').on('click', function () {
        $('.dropmenu1').removeClass('active');
        $('.dropmenu2').removeClass('active');
        $('.dropmenu3').toggleClass('active');
    });


    $('#button-t').on('click', function () {
        $('.dropmenu1').removeClass('active');
        $('.dropmenu2').removeClass('active');
        $('.dropmenu3').removeClass('active');

    });
});// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
