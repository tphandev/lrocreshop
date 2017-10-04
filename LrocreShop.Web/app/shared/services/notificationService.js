(function (app) {

    app.service('notificationService', notificationService);

    function notificationService() {

        toastr.option = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onlick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 1000,
            "extendedTimeOut": 1000
        }

        function displaySuccess(message) {
            toastr.success(message);
        }
        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                });
            }
            else
                toastr.success(error);
        }
        function displayInfo(message) {
            toastr.info(message);
        }
        function displayWarning(message) {
            toastr.warning(message);
        }

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayInfo: displayInfo,
            displayWarning: displayWarning
        }
    };

})(angular.module('lrocreshop.common'));