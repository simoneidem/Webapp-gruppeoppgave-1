function bestillReise() {
    const reise = {
        type: $("#type").val(),
        strekning: $("#strekning").val(),
        tid: $("#tid").val(),
        antall: $("#antall").val(),
        billett: $("#billett").val(),
        transport: $("#transport").val()
    }

    const url = "Reise/Bestille";
    $.post(url, reise, function () {
        window.location.href = 'index.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere")
    });
};