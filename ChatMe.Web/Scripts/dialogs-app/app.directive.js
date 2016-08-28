(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .directive('app', app);

    app.$inject = [];
    function app() {
        var directive = {
            templateUrl: '/Scripts/dialogs-app/app.html'
        };
        return directive;
    }
})();