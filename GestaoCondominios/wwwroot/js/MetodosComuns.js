function ObterMeses(dados) {
    var labels = [];
    var tamanho = dados.length;
    var indice = 0; // para percorrer a lista

    while (indice < tamanho) {
        labels.push(dados[indice].meses);
        indice = indice + 1;
    }

    return labels;
}

function ObterValores(dados) {
    var valores = [];
    var tamanho = dados.length;
    var indice = 0;

    while (indice < tamanho) {
        valores.push(dados[indice].valores);
        indice = indice + 1;
    }

    return valores;
}