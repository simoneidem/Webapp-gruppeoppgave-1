﻿function showAll() {
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

function dontShow() {
    document.getElementById('ifPress1').style.display = 'none';
    document.getElementById('ifPress2').style.display = 'none';
    document.getElementById('ifPress3').style.display = 'none';
    document.getElementById('ifPress4').style.display = 'none';
    document.getElementById('ifPress5').style.display = 'none';
    document.getElementById('ifPress6').style.display = 'none';
    document.getElementById('ifPress7').style.display = 'none';

    document.getElementById('ifno1').style.display = 'none';
    document.getElementById('ifno2').style.display = 'none';
    document.getElementById('ifno3').style.display = 'none';

    document.getElementById('button1').style.display = 'none';
    document.getElementById('button2').style.display = 'none';

    document.getElementById('alt').style.display = 'block';

    document.getElementById('card1').innerHTML = $("#strekning").val();
    document.getElementById('card2').innerHTML = $("#strekning").val();
    document.getElementById('card3').innerHTML = $("#strekning").val();

}

$(function () {

    const id = window.location.search.substring(1);
    const url = "Reise/HentEn?" + id;
    $.get(url, function (reise) {
        $("#id").val(reise.id);
        $("#type").val(reise.type);
        $("#strekning").val(reise.strekning);
        $("#dato").val(reise.dato);
        $("#voksen").val(reise.voksen);
        $("#honnor").val(reise.honnor);
        $("#barn").val(reise.barn);
        $("#student").val(reise.student);
        $("#bil").val(reise.bil);
        $("#motorsykkel").val(reise.motorsykkel);
        $("#sykkel").val(reise.sykkel);
        
    });
});

function checkButton() {
    document.getElementById('tid1').onclick = function () {
        var tid1 = "12:00";
        var reiseid1 = "Z0321";
        document.getElementById("tid").value = tid1;
        document.getElementById("reiseid").value = reiseid1;
        validerOgEndre();
    };
    document.getElementById('tid2').onclick = function () {
        var tid2 = "13:00";
        var reiseid2 = "A5123";
        document.getElementById("tid").value = tid2;
        document.getElementById("reiseid").value = reiseid2;
        validerOgEndre()
    };
    document.getElementById('tid3').onclick = function () {
        var tid3 = "14:00";
        var reiseid3 = "C9543";
        document.getElementById("tid").value = tid3;
        document.getElementById("reiseid").value = reiseid3;
        validerOgEndre();
    };
}

function validerOgEndre() {
    const typeOK = validerType($("#type").val());
    const strekningOK = validerStrekning($("#strekning").val());
    const datoOK = validerDato($("#dato").val());
    const tidOK = true;
    const voksenOK = validerVoksen($("#voksen").val());
    const honnorOK = validerHonnor($("#honnor").val());
    const barnOK = validerBarn($("#barn").val());
    const studentOK = validerStudent($("#student").val());
    const bilOK = validerBil($("#bil").val());
    const motorsykkelOK = validerMotorsykkel($("#motorsykkel").val());
    const sykkelOK = validerSykkel($("#sykkel").val());
    const reiseOK = true;
    if (typeOK && strekningOK && datoOK && tidOK && voksenOK && honnorOK && barnOK && studentOK && bilOK && motorsykkelOK && sykkelOK && reiseOK) {
        endreReise();
    }
}

function endreReise() {
    const reise = {
        id: $("#id").val(),
        type: $("#type").val(),
        strekning: $("#strekning").val(),
        dato: $("#dato").val(),
        tid: $("#tid").val(),
        reiseid: $("#reiseid").val(),
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

