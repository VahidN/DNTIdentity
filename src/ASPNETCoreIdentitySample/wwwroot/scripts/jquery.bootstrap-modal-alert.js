// <![CDATA[
(function ($) {
    $.bootstrapModalAlert = function (options) {
        var defaults = {
            caption: 'تائید عملیات',
            body: 'آیا عملیات درخواستی اجرا شود؟'
        };
        options = $.extend(defaults, options);

        var alertContainer = "#alertContainer";
        var html = '<div class="modal fade" id="alertContainer">' +
                   '<div class="modal-dialog"><div class="modal-content"><div class="modal-header">' +
                        '<a class="close" data-dismiss="modal">&times;</a>'
                        + '<h5>' + options.caption + '</h5></div>' +
                   '<div class="modal-body">'
                        + options.body + '</div></div></div></div></div>';

        $(alertContainer).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();

        $(alertContainer).remove();
        $(html).appendTo('body');
        $(alertContainer).modal('show');
    };
})(jQuery);
// ]]>