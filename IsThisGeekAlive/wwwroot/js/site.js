(function () {
    var selectManualLogin = $('#SelectManualLogin').val() == "True";

    if (selectManualLogin) {
        $('#manualLoginDiv').show();
    }

    $('#howDoesThisWorkBtn').click(function () {
        $('.buttonDivs').hide();
        $('#SelectManualLogin').val('False');

        $('#howDoesThisWorkDiv').show();               
    });

    $('#manualLoginBtn').click(function () {
        $('.buttonDivs').hide();

        $('#SelectManualLogin').val('True');
        $('#manualLoginDiv').show();               
    });

    $(document).ready(function () {
        var utcOffset = new Date().getTimezoneOffset();

        $('#ClientUtcOffset').val(utcOffset);
    });
})();