// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.table').DataTable({
        language: {
            searchPlaceholder: "Search here"
        },
        "lengthMenu": [[5, 10, 20, 50, 100 - 1], [5, 10, 20, 50, 100, "All"]],
        "pageLength": 5
    });
});