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
        "<th>Strekning</th><th>Tid</th><th></th><th></th>" +
        "</tr>";
    for (let reise of reiser) {
        ut += "<tr>" +
            "<td>" + reise.strekning + "</td>" +
            "<td>" + reise.tid + "</td>" +
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