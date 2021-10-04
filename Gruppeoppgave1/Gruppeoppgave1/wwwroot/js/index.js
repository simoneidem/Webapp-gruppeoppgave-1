$(function () {
    hentAlleReiser();
});

function hentAlleReiser() {
    $.get("reise/hentAlle", function (reiser) {
        formaterReiser(reiser);
    });
}

function formaterReiser(reiser) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Type</th><th>Strekning</th><th>Dato</th><th>Voksen</th><th>Honnør</th><th>Barn</th><th>Student</th><th>Bil</th><th>Motorsykkel</th><th>Sykkel</th><th>Tid</th><th>Båt ID</th><th>Pris</th><th></th><th></th>" +
        "</tr>";
    for (let reise of reiser) {
        ut += "<tr>" +
            "<td>" + reise.type + "</td>" +
            "<td>" + reise.strekning + "</td>" +
            "<td>" + reise.dato + "</td>" +
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
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere")
        }
    });
};