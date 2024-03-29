$(function () {
    $("#wizard").steps({
        headerTag: "h4",
        bodyTag: "section",
        transitionEffect: "fade",
        enableAllSteps: true,
        transitionEffectSpeed: 500,

        onStepChanging: function (event, currentIndex, newIndex) {
            var $inputs = $('#wizard').find('.content').eq(currentIndex).find('.required');
            var isValid = true;

            // Check if any required input is empty
            $inputs.each(function () {
                if ($(this).val() === '') {
                    isValid = false;
                    // Show error message and add red border
                    $(this).addClass('invalid-input');
                    $('#' + $(this).attr('id') + 'Error').text('هذا الحقل مطلوب');
                    return false; // Break the loop if any input is empty
                } else {
                    // Remove error message and red border if input is not empty
                    $(this).removeClass('invalid-input');
                    $('#' + $(this).attr('id') + 'Error').text('');
                }
            });

            // If any required input is empty, prevent moving to the next step
            if (!isValid) {
                return false;
            }
            // Update UI based on the current step
            if (newIndex === 1) {
                $('.steps ul').addClass('step-2');
            } else {
                $('.steps ul').removeClass('step-2');
            }
            if (newIndex === 2) {
                $('.steps ul').addClass('step-3');
            } else {
                $('.steps ul').removeClass('step-3');
            }
            if (newIndex === 3) {
                $('.actions ul').addClass('step-last');
            } else {
                $('.actions li:first-child a').removeClass('d-none');
                $('.actions ul').removeClass('step-last');
            }
            return true;
        },

        onFinished: function (event, currentIndex) {
            // Redirect to the search person page
            window.location.href = 'Index';
        },

        labels: {
            finish: "ارسل",
            next: "التالي",
            previous: "السابق"
        },
       

    });
    // Custom Steps Jquery Steps
    $('.actions li:first-child a').addClass('d-none');
    $('.wizard > .steps li a').click(function () {
        $(this).parent().addClass('checked');
        $(this).parent().prevAll().addClass('checked');
        $(this).parent().nextAll().removeClass('checked');
    });

    // Custom Button Jquery Steps
    $('.forward').click(function () {
        $("#wizard").steps('next');
    });

    $('.backward').click(function () {
        $("#wizard").steps('previous');
    });

    // Checkbox
    $('.checkbox-circle label').click(function () {
        $('.checkbox-circle label').removeClass('active');
        $(this).addClass('active');
    });
});
