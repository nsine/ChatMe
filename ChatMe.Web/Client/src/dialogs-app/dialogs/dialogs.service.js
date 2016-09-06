/* @ngInject */
export default function dialogsService(dialogsApi, $rootScope) {
    var self = this;
    var loadSize = 0;
    var loadedDialogs = 0;

    self.dialogs = [];

    self.loadDialogs = loadDialogs;
    self.newDialog = newDialog;

    ////////////////
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