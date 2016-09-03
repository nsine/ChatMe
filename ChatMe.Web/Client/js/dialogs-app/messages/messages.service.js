(function() {
'use strict';

    angular
        .module('dialogsApp')
        .service('messagesService', messagesService);

    messagesService.$inject = ['messagesApi', 'dialogsService', 'openedDialogService', '$rootScope', 'Hub'];
    function messagesService(messagesApi, dialogsService, openedDialogService, $rootScope, Hub) {
        var self = this;

        var loadedMessagesCount = 0;
        var chunkSize = 0;

        self.messages = [];

        self.openDialog = openDialog;
        self.loadNextMessages = loadNextMessages;
        self.sendMessage = sendMessage;

        $rootScope.$watch(function () {
            return openedDialogService.id;
        }, function () {
            openDialog();
        });

        var hub = new Hub('chatHub', {
            listeners: {
                addMessage: function (messageData) {
                    var message = messagesApi.parseRawData(messageData);
                    self.messages.unshift(message);
                    $rootScope.$apply();
                }
            },

            methods: ['send']
        })

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
            hub.send(openedDialogService.id, message);
        }
    }
})();