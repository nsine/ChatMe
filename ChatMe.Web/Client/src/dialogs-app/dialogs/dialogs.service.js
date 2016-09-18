/* @ngInject */
export default function dialogsService(openedDialogService, dialogsApi, messagesService, messagesApi, $rootScope, Hub) {
    var self = this;
    var loadSize = 0;
    var loadedDialogs = 0;

    self.dialogs = [];

    self.loadDialogs = loadDialogs;
    self.newDialog = newDialog;
    self.sendMessage = sendMessage;

    self.hub = new Hub('chatHub', {
        listeners: {
            addMessage: function (messageData) {
                var message = messagesApi.parseRawData(messageData);

                // If message in existing dialog then just add it
                // else create new dialog
                var dialog = self.dialogs.find(d => d.id == message.dialogId);
                if (dialog !== null) {
                    messagesService.messages.unshift(message);
                    $rootScope.$apply();
                } else {
                    var newDialog = dialogsApi.getById(message.dialogId);
                    self.dialogs.unshift(newDialog);
                    $rootScope.$apply();
                }
            },

            notifyOnline: function (userId, status) {
                console.log(`${userId} is online: ${status}`);
                self.dialogs.forEach(function(dialog) {
                    if (dialog.authorId == userId) {
                        dialog.isAuthorOnline = status;
                    }
                }, this);
            }
        },

        methods: ['send']
    });

    ////////////////
    function sendMessage(message) {
        self.hub.send(openedDialogService.id, message);
    }

    function loadDialogs() {
        dialogsApi
            .getDialogs(self.loadedDialogs, loadSize)
            .then(function (data) {
                self.dialogs = self.dialogs.concat(data);
                loadedDialogs += data.length
            });
    }

    function newDialog(post) {
        return dialogsApi
            .newDialog(post)
            .then(function () {
                self.dialogs = [];
                loadDialogs();
                $rootScope.$apply();
            })
    }
}