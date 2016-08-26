(function() {
    'use strict';

    angular
        .module('postsApp')
        .directive('app', app);

    app.$inject = [];
    function app() {
        var directive = {
            templateUrl: '/Scripts/posts-app/app.html'
        };
        return directive;
    }
})();