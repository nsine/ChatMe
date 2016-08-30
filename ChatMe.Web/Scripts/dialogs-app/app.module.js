(function() {
    'use strict';

    angular.module('dialogsApp', ['SignalR']);

    angular.module('dialogsApp')
        .constant('dialogsApiPath', '/api/dialogs')
        .constant('messagesApiPath', '/api/messages/');
})();