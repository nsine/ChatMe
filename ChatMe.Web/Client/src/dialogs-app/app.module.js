import app from './app.directive';
import dialogsApi from './dialogs/dialogs-api.service';
import dialogsService from './dialogs/dialogs.service';
import openedDialogService from './dialogs/opened-dialog.service';
import DialogsListController from './dialogs/dialogs-list.controller';

import messagesApi from './messages/messages-api.service';
import messagesService from './messages/messages.service';
import reverseFilter from './messages/reverse.filter';
import MessagesListController from './messages/messages-list.controller';
import NewMessageController from './messages/new-message.controller';

angular.module('dialogsApp', ['SignalR', 'luegg.directives']);

angular.module('dialogsApp')
    .value('dialogsApiPath', '/api/dialogs')
    .value('messagesApiPath', '/api/messages/')
    .value("initInfo", {
        dialogId: null,
        userId: null
    })
    .directive('app', app)
    .service('dialogsApi', dialogsApi)
    .service('dialogsService', dialogsService)
    .service('openedDialogService', openedDialogService)
    .controller('DialogsListController', DialogsListController)
    .service('messagesApi', messagesApi)
    .service('messagesService', messagesService)
    .filter('reverse', reverseFilter)
    .controller('MessagesListController', MessagesListController)
    .controller('NewMessageController', NewMessageController);