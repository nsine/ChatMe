(function() {
'use strict';

    angular
        .module('dialogsApp')
        .controller('MessagesListController', MessagesListController);

    MessagesListController.$inject = ['messagesService'];
    function MessagesListController(messagesService) {
        var self = this;

        self.messagesService = messagesService;

        activate();

        ////////////////

        function activate() { }
    }
})();