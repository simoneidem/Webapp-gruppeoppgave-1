$(function () {

    const id = window.location.search.substring(1);
    const url = "Reise/HentEn?" + id;
    $.get(url, function (reise) {
        $("#id").val(reise.id);
        $("#strekning").val(reise.strekning);
        $("#tid").val(reise.tid);
    });
});

function endreReise() {
    const reise = {
        id: $("#id").val(),
        strekning: $("#strekning").val(),
        tid: $("#tid").val()
    }
    $.post("Reise/Endre", reise, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}