(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .directive('app', app);

    function app() {
        var directive = {
            templateUrl: '/Scripts/dialogs-app/app.html',
            controller: AppController,
            controllerAs: 'appCtrl'
        };
        return directive;
    }

    AppController.$inject = ['messagesService'];
    function AppController (messagesService) {
        var self = this;

        self.openedDialogId = null;
        self.isDialogOpened = function () {
            return self.openedDialogId !== null;
        }

        self.openDialog = function (dialog) {
            messagesService.dialogId = dialog.id;
            self.openedDialogId = dialog.id;
        }
    }
})();