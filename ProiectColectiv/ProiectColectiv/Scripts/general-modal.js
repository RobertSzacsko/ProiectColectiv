$(document).ready(function () {
    if ($('#general-modal').val() != null)
        $('#GeneralModal').modal('show');

    $("a#id_programare").click(function () {
        var id = this.nextElementSibling.getAttribute("value");
        $("form").attr("action", '/Medic/AddDiagnostic/' + id);
    });

    $("#AddM").on("click", function () {

        var clone = $("#forClone:last").clone();
        clone.find("input").val("");
        clone.insertBefore($("#forClone")).fadeIn("slow");
    });
});