(function() {
    'use strict';

    angular
        .module('postsApp')
        .service('postsApi', postsService);

    postsService.$inject = ['$http', 'userInfo', 'apiPath'];
    function postsService($http, userInfo, apiPath) {
        var self = this;
        var loadSize = 10;

        self.getPosts = getPosts;
        self.newPost = newPost;
        self.changePost = changePost;
        self.deletePost = deletePost;

        ////////////////
        function getPosts(startIndex, count) {
            var path = apiPath + userInfo.id + '?startIndex=' +
                startIndex + '&count=' + count;
            return $http.get(path);
        }

        function newPost(post) {
            if (!userInfo.isOwner) {
                return null;
            }

            var path = apiPath + userInfo.id;
            return $http.post(path, post);
        }

        function changePost(post) {
            if (!userInfo.isOwner) {
                return null;
            }

            var path = apiPath + post.id;
            return $http.post(path, post);
        }

        function deletePost(id) {
            var path = apiPath + id;
            return $http.delete(path);
        }
    }
})();