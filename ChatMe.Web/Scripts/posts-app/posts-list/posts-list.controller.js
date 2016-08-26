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

        activate();

        ////////////////

        function activate() {
            postsService.loadPosts();
        }
    }
})();