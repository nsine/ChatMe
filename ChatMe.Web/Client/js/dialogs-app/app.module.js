(function() {
    'use strict';

    angular.module('dialogsApp', ['SignalR', 'luegg.directives']);

    angular.module('dialogsApp')
        .constant('dialogsApiPath', '/api/dialogs')
        .constant('messagesApiPath', '/api/messages/');
})();