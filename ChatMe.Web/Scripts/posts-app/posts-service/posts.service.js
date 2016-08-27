(function() {
    'use strict';

    angular
        .module('postsApp')
        .service('postsService', postsService);

    postsService.$inject = ['$http', 'postsApi', '$rootScope'];
    function postsService($http, postsApi, $rootScope) {
        var self = this;
        var loadSize = 10;
        var loadedPosts = 0;

        self.posts = [];

        self.loadPosts = loadPosts;
        self.newPost = newPost;

        ////////////////
        function loadPosts() {
            postsApi
                .getPosts(self.loadedPosts, loadSize)
                .then(function (data) {
                    self.posts = self.posts.concat(data);
                    loadedPosts += data.length
                });
        }

        function newPost(post) {
            return postsApi
                .newPost(post)
                .then(function () {
                    self.posts = [];
                    loadPosts();
                    $rootScope.$apply();
                })
        }
    }
})();