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

$(function () {

    const id = window.location.search.substring(1);
    const url = "Reise/HentEn?" + id;
    $.get(url, function (reise) {
        $("#id").val(reise.id);
        $("#type").val(reise.type);
        $("#strekning").val(reise.strekning);
        $("#tid").val(reise.tid);
        $("#voksen").val(reise.voksen);
        $("#honnor").val(reise.honnor);
        $("#barn").val(reise.barn);
        $("#student").val(reise.student);
        $("#bil").val(reise.bil);
        $("#motorsykkel").val(reise.motorsykkel);
        $("#sykkel").val(reise.sykkel);
        
    });
});

function validerOgEndre() {
    const typeOK = validerType($("#type").val());
    const strekningOK = validerStrekning($("#strekning").val());
    const tidOK = validerTid($("#tid").val());
    const voksenOK = validerVoksen($("#voksen").val());
    const honnorOK = validerHonnor($("#honnor").val());
    const barnOK = validerBarn($("#barn").val());
    const studentOK = validerStudent($("#student").val());
    const bilOK = validerBil($("#bil").val());
    const motorsykkelOK = validerMotorsykkel($("#motorsykkel").val());
    const sykkelOK = validerSykkel($("#sykkel").val());
    if (typeOK && strekningOK && tidOK && voksenOK && honnorOK && barnOK && studentOK && bilOK && motorsykkelOK && sykkelOK) {
        endreReise();
    }
}

function endreReise() {
    const reise = {
        id: $("#id").val(),
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
    };
    $.post("Reise/Endre", reise, function () {
        window.location.href = 'index.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere")
    });
}

