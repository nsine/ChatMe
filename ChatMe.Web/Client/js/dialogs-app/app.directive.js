(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .directive('app', app);

    function app() {
        var directive = {
            templateUrl: '/Client/js/dialogs-app/app.html'
        };
        return directive;
    }
})();