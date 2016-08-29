(function() {
'use strict';

    angular
        .module('dialogsApp')
        .service('messagesApi', messagesApi);

    messagesApi.$inject = ['$http','messagesApiPath', 'dialogId'];
    function messagesApi($http, messagesApiPath, dialogId) {
        this.get = get;
        this.create = create;

        ////////////////

        function get(dialogId, startIndex, count) {
            var path = messagesApiPath + dialogId + "?startIndex=" + startIndex +
                "&count=" + count;
            return $http.get(path)
                .then(function (response) {
                    return response.data.map(function (rawMessage) {
                        var message = {
                            id: rawMessage.Id,
                            body: rawMessage.Body,
                            time: new Date(parseInt(rawMessage.Time.replace("/Date(", "").replace(")/",""), 10)),
                            avatarUrl: rawMessage.AuthorAvatarUrl,
                            author: rawMessage.Author,
                            isMy: rawMessage.IsMy
                        };

                        return message;
                    })
                });
        }

        function create(dialogId, message) {
            var path = messagesApiPath + dialogId;
            var messageDto = {
                Body: message.body
            };

            return $http.post(path, messageDto);
        }
    }
})();