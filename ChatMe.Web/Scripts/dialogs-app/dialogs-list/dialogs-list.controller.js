(function() {
'use strict';

    angular
        .module('dialogsApp')
        .controller('DialogsListController', DialogsListController);

    DialogsListController.$inject = ['dialogsService', 'openedDialogService', 'initInfo'];
    function DialogsListController(dialogsService, openedDialogService, initInfo) {
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
            if (initInfo.dialogId.toString() === '') {
                openedDialogService.id = null;
            } else {
                openedDialogService.id = +initInfo.dialogId;
            }

            dialogsService.loadDialogs();
        }
    }
})();