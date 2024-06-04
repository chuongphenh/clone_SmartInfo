function getQueryParam(name) {
    var url = window.location.href;
    var arrUrlPath = url.split("?");
    if (arrUrlPath.length > 1) {
        var arrParam = arrUrlPath[1].split("&");
        for (var i = 0; i < arrParam.length; i++) {
            arrParamPath = arrParam[i].split("=");
            if (name == arrParamPath[0]) {
                return arrParamPath[1];
            }
        }
    }

    return null;
}