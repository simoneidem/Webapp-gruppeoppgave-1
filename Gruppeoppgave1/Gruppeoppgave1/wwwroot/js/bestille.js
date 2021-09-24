function showAll() {
    document.getElementById('ifPress1').style.display = 'block';
    document.getElementById('ifPress2').style.display = 'block';
    document.getElementById('ifPress3').style.display = 'block';
    document.getElementById('ifPress4').style.display = 'block';
    document.getElementById('ifPress5').style.display = 'block';
    document.getElementById('ifPress6').style.display = 'block';
    document.getElementById('ifPress7').style.display = 'block';

    document.getElementById('ifno1').style.display = 'none';
    document.getElementById('ifno2').style.display = 'none';
    document.getElementById('ifno3').style.display = 'none';

    document.getElementById('button1').style.display = 'none';
    document.getElementById('button2').style.display = 'block';
};
function bestillReise() {
    const reise = {
        type: $("#type").val(),
        strekning: $("#strekning").val(),
        tid: $("#tid").val(),
        voksen: $("#voksen").val(),
        honnor: $("#honnor").val(),
        barn: $("#barn").val(),
        student: $("#student").val(),
        bil: $("#bil").val(),
        motorsykkel: $("#motorsykkel").val(),
        sykkel: $("#sykkel").val()
    }

    const url = "Reise/Bestille";
    $.post(url, reise, function () {
        window.location.href = 'index.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere")
    });
};