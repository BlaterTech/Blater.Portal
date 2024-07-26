function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(function() {
        console.log('Texto copiado para a área de transferência.');
    }).catch(function(err) {
        console.error('Erro ao copiar o texto: ', err);
    });
}