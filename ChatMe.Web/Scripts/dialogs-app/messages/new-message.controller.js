(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .controller('NewMessageController', NewMessageController);

    NewMessageController.$inject = ['messagesService'];
    function NewMessageController(messagesService) {
        var self = this;
        self.body = "";
        self.isSendBtnActive = function () {
            return self.body != "";
        }

        self.send = function () {
            if (self.body === "") {
                return;
            }

            var newMesage = {
                body: self.body
            };

            messagesService.sendMessage(newMesage);
        }

        activate();

        ////////////////

        function activate() { }
    }
})();