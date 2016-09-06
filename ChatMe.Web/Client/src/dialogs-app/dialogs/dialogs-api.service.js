/* @ngInject */
export default function dialogsApi($http, dialogsApiPath) {
    var self = this;

    self.getDialogs = getDialogs;
    self.newDialog = newDialog;
    self.deleteDialog = deleteDialog;

    ////////////////
    function getDialogs(startIndex, count) {
        var path = dialogsApiPath + '?startIndex=' +
            startIndex + '&count=' + count;
        return $http.get(path)
            .then(function (response) {
                return response.data.map(function (rawDialog) {
                    var dialog = {
                        id: rawDialog.Id,
                        author: rawDialog.Author,
                        avatarUrl: rawDialog.AvatarUrl,
                        lastMessageSnippet: rawDialog.LastMessageSnippet
                    };

                    return dialog;
                })
            });
    }

    function newDialog(dialog) {
        var path = dialogsApiPath + userInfo.id;
        var dialogDto = {
            UserIds: dialog.userIds
        }
        return $http.post(path, dialogDto);
    }

    function deleteDialog(id) {
        var path = dialogsApiPath + id;
        return $http.delete(path);
    }
}