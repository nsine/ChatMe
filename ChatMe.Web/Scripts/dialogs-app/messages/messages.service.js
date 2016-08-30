(function() {
'use strict';

    angular
        .module('dialogsApp')
        .service('messagesService', messagesService);

    messagesService.$inject = ['messagesApi', 'dialogsService', 'openedDialogService', '$rootScope'];
    function messagesService(messagesApi, dialogsService, openedDialogService, $rootScope) {
        var self = this;

        var loadedMessagesCount = 0;
        var chunkSize = 50;

        self.messages = [];

        self.openDialog = openDialog;
        self.loadNextMessages = loadNextMessages;
        self.sendMessage = sendMessage;

        $rootScope.$watch(function () {
            return openedDialogService.id;
        }, function () {
            openDialog();
        });

        ////////////////

        function openDialog() {
            self.messages = [];
            loadedMessagesCount = 0;
            loadNextMessages();
        }

        function loadNextMessages() {
             messagesApi.get(openedDialogService.id, loadedMessagesCount, chunkSize)
                .then(function (data) {
                    console.log(data);
                    loadedMessagesCount += data.length;
                    self.messages = self.messages.concat(data);
                });
        }

        function sendMessage(message) {
            messagesApi.create(openedDialogService.id, message)
                .then(function (response) {
                    console.log(response);
                    var newMessage = messagesApi.get(openedDialogService.id, 0, 1);
                    if (newMessage !== null && newMessage.length == 1) {
                        messages.unshift(newMessage);
                        self.loadedMessagesCount++;
                    }
                });
        }
    }
})();