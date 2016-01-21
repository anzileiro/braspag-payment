/// <reference path="creditcardjs-v0.10.12.min.js" />
/// <reference path="https://code.jquery.com/jquery-2.1.1.min.js" />

var pay = {

    'validate': function () {

        var paymentIsValid = creditcardjs.isValid();

        var paymentForm = $(document).find('#payment-form');
        
        paymentForm.on('submit', function () {

            creditcardjs.onValidityChange(function (isValid) {
                if (isValid) {

                    console.log('valid');

                } else {

                    console.log('invalid');
                    return false;

                }
            });

        });

    }

};

$(function () {

    pay.validate();

});

