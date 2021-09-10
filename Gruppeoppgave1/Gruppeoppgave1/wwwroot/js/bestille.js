function bestillReise() {
    const reise = {
        strekning: $("#strekning").val(),
        tid: $("#tid").val()
    }

    const url = "Reise/Bestille";
    $.post(url, reise, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
};