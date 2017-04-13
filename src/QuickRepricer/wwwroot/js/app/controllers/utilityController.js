var UtilityController = function () {
    var toast = function (info) {
        Materialize.toast(info, 5000, 'green rounded');
    };
    return {
        toast: toast
    };
}();
