(function() {
    'use strict';

    angular
        .module('postsApp')
        .service('postsService', postsService);

    postsService.$inject = ['$http', 'postsApi'];
    function postsService($http, postsApi) {
        var self = this;
        var loadSize = 10;
        var loadedPosts = 0;

        self.posts = [];

        self.loadPosts = loadPosts;
        self.newPost = newPost;

        ////////////////
        function loadPosts() {
            postsApi.getPosts(self.loadedPosts, loadSize)
                .then(function (response) {
                    self.posts.concat(response.data);
                    self.loadedPosts += response.data.length
                });
        }

        function newPost(post) {
            return postsApi.newPost(post)
                .then(self.loadPosts());
        }
    }
})();