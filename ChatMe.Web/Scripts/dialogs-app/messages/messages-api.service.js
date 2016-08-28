(function() {
'use strict';

    angular
        .module('dialogsApp')
        .messagesApi('messagesApi', messagesApi);

    messagesApi.$inject = ['$http'];
    function messagesApi($http) {
        this.exposedFn = exposedFn;

        ////////////////

        function exposedFn() { }
        }
})();