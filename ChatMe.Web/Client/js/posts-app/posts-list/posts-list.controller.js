(function() {
    'use strict';

    angular
        .module('postsApp')
        .controller('PostsListController', PostsListController);

    PostsListController.$inject = ['$http', 'postsService'];
    function PostsListController($http, postsService) {
        var self = this;

        var loadSize = 10;

        self.postsService = postsService;

        self.prettyTime = function (post) {
            return moment(post.time).fromNow();
            // return post.date;
        };

        self.changeLike = function (post) {
            postsService.changeLike(post);
        }

        activate();

        ////////////////

        function activate() {
            postsService.loadPosts();
        }
    }
})();