function getOption() {
    var retur = document.getElementById('type').value;
    if (retur == "Tur/retur" || retur == "Innenriks - Tur/retur") {
        document.getElementById('innre').style.display = "block";
    }
    else {
        document.getElementById('innre').style.display = "none";
    }
    validerType(this.value);
}
function showAll() {
    document.getElementById('ifPress1').style.display = 'block';
    document.getElementById('ifPress2').style.display = 'block';
    document.getElementById('ifPress3').style.display = 'block';
    document.getElementById('ifPress4').style.display = 'block';
    document.getElementById('ifPress5').style.display = 'block';
    document.getElementById('ifPress6').style.display = 'block';
    document.getElementById('ifPress7').style.display = 'block';
    document.getElementById('innre').style.display = "none";

    document.getElementById('ifno1').style.display = 'none';
    document.getElementById('ifno2').style.display = 'none';
    document.getElementById('ifno3').style.display = 'none';
    document.getElementById('innre').style.display = "none";

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
    document.getElementById('innre').style.display = "none";

    document.getElementById('button1').style.display = 'none';
    document.getElementById('button2').style.display = 'none';

    document.getElementById('alt').style.display = 'block';

    document.getElementById('card1').innerHTML = $("#strekning").val();
    document.getElementById('card2').innerHTML = $("#strekning").val();
    document.getElementById('card3').innerHTML = $("#strekning").val();

    pris();

}

function pris() {
    var voksen = $("#voksen").val() * 150;
    var honnor = $("#honnor").val() * 125;
    var barn = $("#barn").val() * 50;
    var student = $("#student").val() * 100;
    var sumBillett = voksen + honnor + barn + student;

    var bil = $("#bil").val() * 200;
    var motorsykkel = $("#motorsykkel").val() * 100;
    var sykkel = $("#sykkel").val() * 50;
    var sumTransport = bil + motorsykkel + sykkel;

    var sum = sumBillett + sumTransport;
    return sum;
}

function checkButton() {
    document.getElementById('tid1').onclick = function () {
        var tid1 = "12:00";
        var reiseid1 = "Z0321";
        document.getElementById("tid").value = tid1;
        document.getElementById("reiseid").value = reiseid1;
        document.getElementById("pris").value = pris();
        validerOgBestill();
    };
    document.getElementById('tid2').onclick = function () {
        var tid2 = "13:00";
        var reiseid2 = "A5123";
        document.getElementById("tid").value = tid2;
        document.getElementById("reiseid").value = reiseid2;
        document.getElementById("pris").value = pris();
        validerOgBestill();
    };
    document.getElementById('tid3').onclick = function () {
        var tid3 = "14:00";
        var reiseid3 = "C9543";
        document.getElementById("tid").value = tid3;
        document.getElementById("reiseid").value = reiseid3;
        document.getElementById("pris").value = pris();
        validerOgBestill();
    };
}


function validerOgBestill() {
    const typeOK = validerType($("#type").val());
    const strekningOK = validerStrekning($("#strekning").val());
    const datoOK = validerDato($("#dato").val());
    const tidOK = validerTid($("#tid").val());
    const voksenOK = validerVoksen($("#voksen").val());
    const honnorOK = validerHonnor($("#honnor").val());
    const barnOK = validerBarn($("#barn").val());
    const studentOK = validerStudent($("#student").val());
    const bilOK = validerBil($("#bil").val());
    const motorsykkelOK = validerMotorsykkel($("#motorsykkel").val());
    const sykkelOK = validerSykkel($("#sykkel").val());
    const reiseidOK = validerReiseid($("#reiseid").val());
    const prisOK = validerPris($("#pris").val());
    const innreiseOK = validerInnreise($("#innreise").val());
    if (typeOK && strekningOK && datoOK && tidOK && voksenOK && honnorOK && barnOK && studentOK && bilOK && motorsykkelOK && sykkelOK && reiseidOK && prisOK && innreiseOK) {
        bestillReise();
    }
}

function bestillReise() {
    const reise = {
        type: $("#type").val(),
        strekning: $("#strekning").val(),
        dato: $("#dato").val(),
        innreise: $("#innreise").val(),
        tid: $("#tid").val(),
        reiseid: $("#reiseid").val(),
        pris: $("#pris").val(),
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
        .fail(function (feil) {
            if (feil.status == 401) {  // ikke logget inn, redirect til loggInn.html
                window.location.href = 'loggInn.html';
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
};