/* @ngInject */
export default function NewMessageController(dialogsService) {
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

        dialogsService.sendMessage(newMesage);
    }

    activate();

    ////////////////

    function activate() { }
}