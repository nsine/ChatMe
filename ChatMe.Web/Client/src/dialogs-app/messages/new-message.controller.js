/* @ngInject */
export default function NewMessageController(messagesService) {
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