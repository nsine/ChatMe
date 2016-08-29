(function() {
'use strict';

    angular
        .module('dialogsApp')
        .service('messagesService', messagesService);

    messagesService.$inject = ['messagesApi'];
    function messagesService(messagesApi) {
        var self = this;

        var loadedMessagesCount = 0;
        var chunkSize = 50;

        self.messages = [];
        self.dialogId = null;

        self.openDialog = openDialog;
        self.loadNextMessages = loadNextMessages;
        self.sendMessage = sendMessage;

        ////////////////

        function openDialog() {
            self.messages = [];
            loadNextMessages();
        }

        function loadNextMessages() {
             messagesApi.get(self.dialogId, loadedMessagesCount, chunkSize)
                .then(function (data) {
                    loadedMessagesCount += data.length;
                    self.messages = self.messages.concat(data);
                });
        }

        function sendMessage(message) {
            messagesApi.create(self.dialogId, message)
                .then(function (response) {
                    console.log(response);
                    var newMessage = messagesApi.get(self.dialogId, 0, 1);
                    if (newMessage !== null && newMessage.length == 1) {
                        messages.unshift(newMessage);
                        self.loadedMessagesCount++;
                    }
                });
        }
    }
})();