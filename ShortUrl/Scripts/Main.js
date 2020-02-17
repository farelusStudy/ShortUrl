function copyById(elemId) {
    let tmp = document.createElement('INPUT');
    tmp.value = document.getElementById(elemId).href;
    document.body.appendChild(tmp);
    tmp.select();
    document.execCommand('copy');
    document.body.removeChild(tmp);
    alert("Copied");
}
