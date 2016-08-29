(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .directive('app', app);

    function app() {
        var directive = {
            templateUrl: '/Scripts/dialogs-app/app.html'
        };
        return directive;
    }
})();