// <![CDATA[
(function ($) {
    $.bootstrapModalAjaxForm = function (options) {
        var defaults = {
            renderModalPartialViewUrl: null,
            renderModalPartialViewData: null,
            postUrl: '/',
            loginUrl: '/identity/login',
            beforePostHandler: null,
            completeHandler: null,
            errorHandler: null
        };
        options = $.extend(defaults, options);

        var validateForm = function (form) {
            //فعال سازی دستی اعتبار سنجی جی‌کوئری
            var val = form.validate();
            val.form();
            return val.valid();
        };

        var enableBootstrapStyleValidation = function () {
            $.validator.setDefaults({
                ignore: "", // for hidden tabs and also textarea's
                errorElement: 'span',
                errorPlacement: function (error, element) {
                    error.addClass('invalid-feedback');
                    element.closest('.form-group').append(error);
                },
                highlight: function (element, errorClass, validClass) {
                    if (element.type === 'radio') {
                        this.findByName(element.name).addClass(errorClass).removeClass(validClass);
                    } else {
                        $(element).addClass(errorClass).removeClass(validClass);
                        $(element).addClass('is-invalid').removeClass('is-valid');
                        $(element).closest('.form-group').find('.input-group-text, label').removeClass('text-success').addClass('text-danger');
                    }
                    $(element).trigger('highlited');
                },
                unhighlight: function (element, errorClass, validClass) {
                    if (element.type === 'radio') {
                        this.findByName(element.name).removeClass(errorClass).addClass(validClass);
                    } else {
                        $(element).removeClass(errorClass).addClass(validClass);
                        $(element).removeClass('is-invalid').addClass('is-valid');
                        $(element).closest('.form-group').find('.input-group-text, label').removeClass('text-danger').addClass('text-success');
                    }
                    $(element).trigger('unhighlited');
                }
            });
        };

        var enablePostbackValidation = function () {
            $('form').each(function () {
                $(this).find('div.form-group').each(function () {
                    if ($(this).find('span.field-validation-error').length > 0) {
                        $(this).addClass('is-invalid');
                    }
                });
            });
        };

        var processAjaxForm = function (dialog) {
            $('form', dialog).submit(function (e) {
                e.preventDefault();

                if (!validateForm($(this))) {
                    //اگر فرم اعتبار سنجی نشده، اطلاعات آن ارسال نشود
                    return false;
                }

                //در اینجا می‌توان مثلا دکمه‌ای را غیرفعال کرد
                if (options.beforePostHandler)
                    options.beforePostHandler();

                //اطلاعات نباید کش شوند
                $.ajaxSetup({ cache: false });
                $.ajax({
                    url: options.postUrl,
                    type: "POST",
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#dialogDiv').modal('hide');
                            if (options.completeHandler)
                                options.completeHandler();
                        } else {
                            $('#dialogContent').html(result);

                            $.validator.unobtrusive.parse("#dialogContent");
                            enablePostbackValidation();
                            processAjaxForm('#dialogContent');

                            if (options.errorHandler)
                                options.errorHandler();
                        }
                    }
                });
                return false;
            });
        };

        var mainContainer = "<div id='dialogDiv' class='modal fade'><div id='dialogContent'></div></div>";
        enableBootstrapStyleValidation(); //اعمال نکات خاص بوت استرپ جهت اعتبارسنجی یکپارچه با آن
        $.ajaxSetup({ cache: false });
        $.ajax({
            type: "POST",
            url: options.renderModalPartialViewUrl,
            data: options.renderModalPartialViewData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function (xhr, status) {
                var data = xhr.responseText;
                if (xhr.status === 403 || xhr.status === 401) {
                    //در حالت لاگین نبودن شخص اجرا می‌شود
                    var loginLocation = xhr.getResponseHeader('Location');
                    if (loginLocation) {
                        window.location = loginLocation;
                    }
                    else {
                        window.location = options.loginUrl;
                    }
                }
                else if (status === 'error' || !data) {
                    if (options.errorHandler)
                        options.errorHandler();
                }
                else {
                    var dialogContainer = "#dialogDiv";
                    $(dialogContainer).remove();
                    $(mainContainer).appendTo('body');

                    $('#dialogContent').html(data); // دریافت پویای اطلاعات مودال دیالوگ
                    $.validator.unobtrusive.parse("#dialogContent"); // فعال سازی اعتبارسنجی فرمی که با ایجکس بارگذاری شده
                    enablePostbackValidation();
                    // و سپس نمایش آن به صورت مودال
                    $('#dialogDiv').modal({
                        backdrop: 'static', //با کلیک کاربر روی صفحه، صفحه مودال بسته نمی‌شود
                        keyboard: true
                    }, 'show');
                    // تحت نظر قرار دادن این فرم اضافه شده
                    processAjaxForm('#dialogContent');
                }
            }
        });
    };
})(jQuery);
// ]]>