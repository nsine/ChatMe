(function() {
'use strict';

    angular
        .module('dialogsApp')
        .controller('MessagesListController', MessagesListController);

    MessagesListController.$inject = ['messagesService', '$location', '$anchorScroll'];
    function MessagesListController(messagesService, $location, $anchorScroll) {
        var self = this;

        self.messagesService = messagesService;

        activate();

        self.getMessageClass = function (msg) {
            if (msg.isMy) {
                return 'self';
            } else {
                return 'other';
            }
        }

        self.prettyTime = function (time) {
            if (isToday(time)) {
                return moment(time).format('LTS');
            } else if (isThisYear(time)) {
                return moment(time).format('DD MMM, hh:mm')
            } else {
                return moment(time).format('lll')
            }
        }

        ////////////////

        function activate() {
            var chat = $('.messages-list');
            chat.scrollTop(chat[0].scrollHeight);
        }

        function isToday(time) {
            var TODAY = moment().clone().startOf('day');
            return moment(time).isSame(TODAY, 'd')
        }

        function isThisYear(time) {
            return moment(time).isSame(moment(), 'year');
        }
    }
})();