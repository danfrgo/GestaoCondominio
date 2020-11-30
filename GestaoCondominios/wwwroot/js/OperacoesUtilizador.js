function AprovarUtilizador(utilizadorId, nome) {
    const url = "/Utilizadores/AprovarUtilizador";

    $.ajax({
        method: 'POST',
        url: url,
        data: { utilizadorId: utilizadorId }, // o utilizadorId tem que ser igual ao metodo AprovarUtilizador do controller 
        success: function (data) { // os dados vêm da função AprovarUtilizador
            if (data === true) {
                $("#" + utilizadorId).removeClass("purple darken-3").addClass("green darken-3").text("Aprovado");

                $("." + utilizadorId).children('a').remove();
                $("." + utilizadorId).append('<a class="btn-floating blue darken-4" href="Utilizadores/GerirUtilizadores?utilizadorId=' + utilizadorId + '&nome=' + nome + '" asp-controller="Utilizadores" asp-action="GerirUtilizador" asp-route-utilizadorId="' + utilizadorId + '" asp-route-nome="' + nome + '"><i class="material-icons">group</i></a>');

                M.toast({
                    html: "Utilizador aprovado",
                    classes: "green darken-3"
                });
            }

            else {
                M.toast({
                    html: "Não foi possível aprovar o utilizador"
                });
            }
        }

    });
}

function ReprovarUtilizadores(utilizadorId) {
    const url = "/Utilizadores/ReprovarUtilizador";

    $.ajax({
        method: 'POST',
        url: url,
        data: { utilizadorId: utilizadorId },
        success: function (data) {
            if (data === true) {
                $("#" + utilizadorId).removeClass("purple darken-3").addClass("orange darken-3").text("Reprovado");

                // $("." + utilizadorId).remove();

                M.toast({
                    html: "Utilizador reprovado",
                    classes: "orange darken-3"
                });
            }

            else {
                M.toast({
                    html: "Não foi possível aprovar o utilizador"
                });
            }
        }
    });
}