(function() {
    'use strict';

    angular.module('postsApp', []);

    angular.module('postsApp')
        .constant('apiPath', '/api/posts/')
        .constant('likePath', '/api/activity/like');
})();