/* @ngInject */
export default function postsService($http, postsApi, $rootScope) {
    var self = this;
    var loadSize = 0;
    var loadedPosts = 0;

    self.posts = [];

    self.loadPosts = loadPosts;
    self.newPost = newPost;
    self.changeLike = changeLike;

    ////////////////
    function loadPosts() {
        postsApi
            .getPosts(self.loadedPosts, loadSize)
            .then(function (data) {
                self.posts = self.posts.concat(data);
                loadedPosts += data.length;
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

    function changeLike(post) {
        postsApi.likePost(post.id)
            .then(function () {
                postsApi.getPost(post.id)
                    .then(function (data) {
                        post.likes = data.likes;
                        post.isLikedByMe = data.isLikedByMe;
                    });
            });
    }
}