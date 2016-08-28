(function() {
    'use strict';

    angular
        .module('postsApp')
        .service('postsApi', postsApi);

    postsApi.$inject = ['$http', 'userInfo', 'apiPath'];
    function postsApi($http, userInfo, apiPath) {
        var self = this;

        self.getPosts = getPosts;
        self.newPost = newPost;
        self.changePost = changePost;
        self.deletePost = deletePost;

        ////////////////
        function getPosts(startIndex, count) {
            var path = apiPath + userInfo.id + '?startIndex=' +
                startIndex + '&count=' + count;
            return $http.get(path)
                .then(function (response) {
                    return response.data.map(function (rawPost) {
                        var post = {
                            id: rawPost.Id,
                            body: rawPost.Body,
                            time: new Date(parseInt(rawPost.Time.replace("/Date(", "").replace(")/",""), 10)),
                            likes: rawPost.Likes,
                            avatarUrl: rawPost.AvatarUrl,
                            author: rawPost.Author,
                            authorLink: rawPost.AuthorLink
                        };

                        return post;
                    })
                });
        }

        function newPost(post) {
            if (!userInfo.isOwner) {
                return null;
            }

            var path = apiPath + userInfo.id;
            var postDto = {
                Body: post.body
            }
            return $http.post(path, postDto);
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