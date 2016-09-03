(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .service('openedDialogService', openedDialogService);

    function openedDialogService() {
        this.id = null;
    }
})();