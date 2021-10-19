$(function () {
    hentAlleReiser();
});

function hentAlleReiser() {
    $.get("reise/hentAlle", function (reiser) {
        formaterReiser(reiser);
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}

function formaterReiser(reiser) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Type</th><th>Strekning</th><th>Utreise</th><th>Innreise</th><th>Voksen</th><th>Honnør</th><th>Barn</th><th>Student</th><th>Bil</th><th>Motorsykkel</th><th>Sykkel</th><th>Tid</th><th>Båt ID</th><th>Pris</th><th></th><th></th>" +
        "</tr>";
    for (let reise of reiser) {
        ut += "<tr>" +
            "<td>" + reise.type + "</td>" +
            "<td>" + reise.strekning + "</td>" +
            "<td>" + reise.dato + "</td>" +
            "<td>" + reise.innreise + "</td>" +
            "<td>" + reise.voksen + "</td>" +
            "<td>" + reise.honnor + "</td>" +
            "<td>" + reise.barn + "</td>" +
            "<td>" + reise.student + "</td>" +
            "<td>" + reise.bil + "</td>" +
            "<td>" + reise.motorsykkel + "</td>" +
            "<td>" + reise.sykkel + "</td>" +
            "<td>" + reise.tid + "</td>" +
            "<td>" + reise.reiseid + "</td>" +
            "<td>" + reise.pris + "</td>" +
            "<td> <a class='btn btn-primary' href='endre.html?id="+reise.id+"'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettReise("+reise.id+")'>Slett</button</td>"+
            "</tr>";
    }
    ut += "</table>";
    $("#reisene").html(ut);
}

function slettReise(id) {
    const url = "Reise/Slett?id="+id;
    $.get(url, function () {
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
}