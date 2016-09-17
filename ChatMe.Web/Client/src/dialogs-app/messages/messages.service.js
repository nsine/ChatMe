/* @ngInject */
export default function messagesService(messagesApi, openedDialogService, $rootScope) {
    var self = this;

    var loadedMessagesCount = 0;
    var chunkSize = 0;

    self.messages = [];

    self.openDialog = openDialog;
    self.loadNextMessages = loadNextMessages;

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
}