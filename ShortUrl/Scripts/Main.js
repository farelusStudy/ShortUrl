function copyHrefById(elemId) {
    copyString(document.getElementById(elemId).href);
}

function copyString(string) {
    let tmp = document.createElement('INPUT');
    tmp.value = string;
    document.body.appendChild(tmp);
    tmp.select();
    document.execCommand('copy');
    document.body.removeChild(tmp);
    alert("Copied");
}


function isMainUrl(mainUrl) {
    let inp = document.getElementsByName('FullUrl');
    let ans = inp[0].includes(mainUrl);
    if (ans) {
        inp.value = "";
    }
    return ans;
}

function onCalcUrl(url) {
    let inp = document.getElementsByName('FullUrl');
    inp.value;
}

