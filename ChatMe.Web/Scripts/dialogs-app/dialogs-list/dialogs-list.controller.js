(function() {
'use strict';

    angular
        .module('dialogsApp')
        .controller('DialogsListController', DialogsListController);

    DialogsListController.$inject = ['dialogsService', 'openedDialogService', 'dialogId'];
    function DialogsListController(dialogsService, openedDialogService, dialogId) {
        var self = this;

        self.dialogsService = dialogsService;

        self.openDialog = function (dialog) {
            openedDialogService.id = dialog.id;

            dialogsService.dialogs.forEach(function (d) {
                d.selected = false;
            });

            dialog.selected = true;
        }

        self.isDialogOpened = function () {
            return openedDialogService.id !== null;
        }

        activate();

        ////////////////

        function activate() {
            console.log(openedDialogService.id);
            if (dialogId.toString() === '') {
                openedDialogService.id = null;
            } else {
                openedDialogService.id = +dialogId;
            }

            dialogsService.loadDialogs();
        }
    }
})();