(function() {
'use strict';

    angular
        .module('dialogsApp')
        .service('messagesService', messagesService);

    messagesService.$inject = ['messagesApi'];
    function messagesService(messagesApi) {
        var self = this;

        var startIndex = 0;
        var chunkSize = 50;

        self.messages = [];

        self.openDialog = openDialog;


        ////////////////

        function openDialog(id) { }
            self.messages = [];
            loadNextMessages(id)
        }

        function loadNextMessages(id) {
            var messages = messagesApi.get(id, startIndex, chunkSize);
        }
})();