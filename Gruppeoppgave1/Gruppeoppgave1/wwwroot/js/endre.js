$(function () {

    const id = window.location.search.substring(1);
    const url = "Reise/HentEn?" + id;
    $.get(url, function (reise) {
        $("#id").val(reise.id);
        $("#type").val(reise.type);
        $("#strekning").val(reise.strekning);
        $("#tid").val(reise.tid);
        $("#antall").val(reise.antall);
        $("#billett").val(reise.billett);
        $("#transport").val(reise.transport);
        
    });
});

function endreReise() {
    const reise = {
        id: $("#id").val(),
        type: $("#type").val(),
        strekning: $("#strekning").val(),
        tid: $("#tid").val(),
        antall: $("#antall").val(),
        billett: $("#billett").val(),
        transport: $("#transport").val()
    };
    $.post("Reise/Endre", reise, function () {
        window.location.href = 'index.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere")
    });
}

