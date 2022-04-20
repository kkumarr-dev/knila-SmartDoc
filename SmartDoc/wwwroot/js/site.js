function bindsidepartial(url, width) {
    debugger;
    showLoader()
    $.ajax({
        url: url,
        type: 'GET',
        /*dataType: '',*/
        success: function (data) {
            debugger;
            hideLoader()
            $(`#partialContent`).html(data);
            $(`#webAppPatial`).modal('show');
            $('.modal-dialog').css('max-width', `${width}px`)
            $('table').DataTable();
            $('select').selectpicker();
            $('.date-picker').datepicker({
                format: 'dd M yyyy',
            });
            $('.time-picker').timepicker({ 'timeFormat': 'h:i A' });
        },
        error: function (request, error) {
            debugger;
            if (request.status == 401) {
            }
            hideLoader()
        }
    });
}
function bindpartial(url, selector,hider) {
    debugger;
    showLoader()
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            debugger;
            hideLoader()
            $(hider).hide();
            $(selector).show();
            $(selector).html(data);
            $('table').DataTable();
            $('select').selectpicker();
            $('.date-picker').datepicker({
                format: 'dd M yyyy',
            });
            $('.time-picker').timepicker({ 'timeFormat': 'h:i A' });
        },
        error: function (request, error) {
            debugger;
            hideLoader()
            if (request.status == 401) {
            }
        }
    });
}
function showLoader() {
    $('#sdoc-loader').addClass('is-active')
}
function hideLoader() {
    $('#sdoc-loader').removeClass('is-active')
}
function backBotton(hideId, showId) {
    $(`${hideId}`).html('');
    $(`${hideId}`).hide();
    $(`${showId}`).show();
}


jQuery.validator.addMethod(
    "validDOB",
    function (value, element) {
        var from = value.split(" "); // DD MM YYYY
        // var from = value.split("/"); // DD/MM/YYYY

        var day = from[0];
        var month = from[1];
        var year = from[2];
        var age = 18;

        var mydate = new Date();
        mydate.setFullYear(year, month - 1, day);

        var currdate = new Date();
        var setDate = new Date();

        setDate.setFullYear(mydate.getFullYear() + age, month - 1, day);

        if ((currdate - setDate) > 0) {
            return true;
        } else {
            return false;
        }
    },
    "Sorry, you must be 18 years of age to apply"
);