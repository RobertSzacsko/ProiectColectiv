﻿$(document).ready(function () {
    if ($('#general-modal').val() != null)
        $('#GeneralModal').modal('show');

    $("a#id_programare").click(function () {
        var id = this.nextElementSibling.getAttribute("value");
        $("form").attr("action", '/Medic/AddDiagnostic/' + id);
    });
});

JQuery("#AddM").click(function () {
    console.log("dadasdasdasd");
    //$("#forClone").clone().appendTo(this);
});