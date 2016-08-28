(function() {
    'use strict';

    angular
        .module('dialogsApp')
        .service('dialogsApi', dialogsApi);

    dialogsApi.$inject = ['$http', 'apiPath'];
    function dialogsApi($http, apiPath) {
        var self = this;

        self.getDialogs = getDialogs;
        self.newDialog = newDialog;
        self.deleteDialog = deleteDialog;

        ////////////////
        function getDialogs(startIndex, count) {
            var path = apiPath + '?startIndex=' +
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
            var path = apiPath + userInfo.id;
            var dialogDto = {
                UserIds: dialog.userIds
            }
            return $http.post(path, dialogDto);
        }

        function deleteDialog(id) {
            var path = apiPath + id;
            return $http.delete(path);
        }
    }
})();