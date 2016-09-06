import app from './app.directive';
import postsApi from './services/posts-api.service';
import postsService from './services/posts.service';
import NewPostController from './new-post/new-post.controller';
import PostsListController from './posts-list/posts-list.controller';

angular.module('postsApp', []);

angular.module('postsApp')
    .value('likePath', '/api/activity/like')
    .value('apiPath', null)
    .value('userInfo', {
        id: null,
        isOwner: null,
    })
    .directive('app', app)
    .service('postsApi', postsApi)
    .service('postsService', postsService)
    .controller('NewPostController', NewPostController)
    .controller('PostsListController', PostsListController);