function validerType(type) {
    const regexp = /^[a-zA-ZæøåÆØÅÀ-ú\.\ \-/]{2,25}$/;
    const ok = regexp.test(type);
    if (!ok) {
        $("#feilType").html("Typen reise må være alt fra 2 til 20 bokstaver")
        return false;
    }
    else {
        $("#feilType").html("");
        return true;
    }
}

function validerStrekning(strekning) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,30}$/;
    const ok = regexp.test(strekning);
    if (!ok) {
        $("#feilStrekning").html("Strekningen må være alt fra 2 til 20 bokstaver")
        return false;
    }
    else {
        $("#feilStrekning").html("");
        return true;
    }
}

function validerTid(tid) {
    const regexp = /^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$/;
    const ok = regexp.test(tid);
    if (!ok) {
        $("#feilTid").html("Tid må være alt fra 2 til 20 bokstaver")
        return false;
    }
    else {
        $("#feilTid").html("");
        return true;
    }
}

function validerVoksen(voksen) {
    const regexp = /^([0-9]|1[0-5])$/;
    const ok = regexp.test(voksen);
    if (!ok) {
        $("#feilVoksen").html("Voksen må være et tall fra 0 til og med 15")
        return false;
    }
    else {
        $("#feilVoksen").html("");
        return true;
    }
}

function validerHonnor(honnor) {
    const regexp = /^([0-9]|1[0-5])$/;
    const ok = regexp.test(honnor);
    if (!ok) {
        $("#feilHonnor").html("Honnør må være et tall fra 0 til og med 15")
        return false;
    }
    else {
        $("#feilHonnor").html("");
        return true;
    }
}

function validerBarn(barn) {
    const regexp = /^([0-9]|1[0-5])$/;
    const ok = regexp.test(barn);
    if (!ok) {
        $("#feilBarn").html("Barn må være et tall fra 0 til og med 15")
        return false;
    }
    else {
        $("#feilBarn").html("");
        return true;
    }
}

function validerStudent(student) {
    const regexp = /^([0-9]|1[0-5])$/;
    const ok = regexp.test(student);
    if (!ok) {
        $("#feilStudent").html("Student må være et tall fra 0 til og med 15")
        return false;
    }
    else {
        $("#feilStudent").html("");
        return true;
    }
}

function validerBil(bil) {
    const regexp = /^([0-9]|1[0-5])$/;
    const ok = regexp.test(bil);
    if (!ok) {
        $("#feilBil").html("Bil må være et tall fra 0 til og med 9")
        return false;
    }
    else {
        $("#feilBil").html("");
        return true;
    }
}

function validerMotorsykkel(motorsykkel) {
    const regexp = /^([0-9]|1[0-5])$/;
    const ok = regexp.test(motorsykkel);
    if (!ok) {
        $("#feilMotorsykkel").html("Motorsykkel må være et tall fra 0 til og med 9")
        return false;
    }
    else {
        $("#feilMotorsykkel").html("");
        return true;
    }
}

function validerSykkel(sykkel) {
    const regexp = /[0 - 9]/;
    const ok = regexp.test(sykkel);
    if (!ok) {
        $("#feilSykkel").html("Sykkel må være et tall fra 0 til og med 9")
        return false;
    }
    else {
        $("#feilSykkel").html("");
        return true;
    }
}