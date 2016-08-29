(function() {
    'use strict';

    angular.module('dialogsApp', []);

    angular.module('dialogsApp')
        .constant('dialogsApiPath', '/api/dialogs')
        .constant('messagesApiPath', '/api/messages/');
})();