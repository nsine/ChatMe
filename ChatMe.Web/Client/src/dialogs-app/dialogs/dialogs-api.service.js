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
                        authorId: rawDialog.AuthorId,
                        avatarUrl: rawDialog.AvatarUrl,
                        isAuthorOnline: rawDialog.IsAuthorOnline,
                        lastMessageSnippet: rawDialog.LastMessageSnippet
                    };

                    return dialog;
                })
            });
    }

    function getById(id) {
        var path = dialogsApiPath + count;
        return $http.get(path)
            .then(function (response) {
                var rawDialog = response.data;
                return {
                    id: rawDialog.Id,
                    author: rawDialog.Author,
                    authorId: rawDialog.AuthorId,
                    avatarUrl: rawDialog.AvatarUrl,
                    isAuthorOnline: rawDialog.IsAuthorOnline,
                    lastMessageSnippet: rawDialog.LastMessageSnippet
                };
            });
    }

    function newDialog(dialog) {
        var path = dialogsApiPath + "/new/" + userInfo.id;
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