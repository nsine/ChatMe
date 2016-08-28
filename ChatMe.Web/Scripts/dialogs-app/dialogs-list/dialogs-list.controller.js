(function() {
'use strict';

    angular
        .module('dialogsApp')
        .controller('DialogsListController', DialogsListController);

    DialogsListController.$inject = ['dialogsService'];
    function DialogsListController(dialogsService) {
        var self = this;

        self.dialogsService = dialogsService;

        self.openDialog = function (dialog) {
            console.log("Opened" + dialog.id);
        }

        activate();

        ////////////////

        function activate() {
            dialogsService.loadDialogs();
        }
    }
})();