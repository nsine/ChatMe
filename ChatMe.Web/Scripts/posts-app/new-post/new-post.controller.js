(function() {
    'use strict';

    angular
        .module('postsApp')
        .controller('NewPostController', NewPostController);

    NewPostController.$inject = ['postsService'];
    function NewPostController(postsService) {
        var self = this;

        self.postBody = "";

        self.sendPost = function () {
            if (self.postBody !== "") {
                postsService
                    .newPost({
                        body: self.postBody
                    })
                    .then(function () {
                        self.postBody = "";
                    })
            }
        }
    }
})();