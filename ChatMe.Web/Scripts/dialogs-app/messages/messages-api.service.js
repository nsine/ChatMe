(function() {
'use strict';

    angular
        .module('dialogsApp')
        .service('messagesApi', messagesApi);

    messagesApi.$inject = ['$http','messagesApiPath', 'initInfo'];
    function messagesApi($http, messagesApiPath, initInfo) {
        this.get = get;
        this.create = create;
        this.parseRawData = parseRawData;

        ////////////////

        function get(dialogId, startIndex, count) {
            var path = messagesApiPath + dialogId + "?startIndex=" + startIndex +
                "&count=" + count;
            return $http.get(path)
                .then(function (response) {
                    return response.data.map(parseRawData)
                });
        }

        function create(dialogId, message) {
            var path = messagesApiPath + dialogId;
            var messageDto = {
                Body: message.body
            };

            return $http.post(path, messageDto);
        }

        function parseRawData(rawData) {
            return {
                id: rawData.Id,
                body: rawData.Body,
                time: new Date(parseInt(rawData.Time.replace("/Date(", "").replace(")/",""), 10)),
                avatarUrl: rawData.AuthorAvatarUrl,
                author: rawData.Author,
                authorId: rawData.AuthorId,
                isMy: rawData.AuthorId == initInfo.userId
            };
        }
    }
})();