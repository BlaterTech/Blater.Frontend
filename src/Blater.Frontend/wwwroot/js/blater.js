//Clipboard
function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(function() {
        console.log('Texto copiado para a área de transferência.');
    }).catch(function(err) {
        console.error('Erro ao copiar o texto: ', err);
    });
}

//Cookie functions
function WriteCookie(name, value, days) {
    const date = new Date();
    date.setDate(date.getTime() + (days * 24 * 60 * 60 * 1000));
    let expires = "expires="+date.toUTCString();
    document.cookie = name + "=" + value + ";" + expires + ";path=/";
}

function ReadCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function DeleteCookie(name) {
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
}