// require('bootstrap-notify');

var dialogsPath = '/messages?dialogId=';

// Configure SignalR
var chatHub = $.connection.chatHub;
chatHub.client.addMessage = function (messageData) {
    var message = parseRawData(messageData);
    $.notify({
        type: 'success',
        closable: false,
        message: makeMessageString(message),
        url: dialogsPath + message.dialogId
    });
    console.log(messageData);
}

$.connection.hub.start();

function makeMessageString(message) {
    return `New message from ${message.author}:\n${message.body}`;
}

function parseRawData(rawData) {
    var result = {
        body: rawData.Body,
        avatarUrl: rawData.AuthorAvatarUrl,
        author: rawData.Author,
        dialogId: rawData.DialogId
    };

    if (isNaN(Date.parse(rawData.Time))) {
        result.time =new Date(parseInt(rawData.Time.replace("/Date(", "").replace(")/",""), 10));
    } else {
        result.time = new Date(Date.parse(rawData.Time));
    }

    return result;
}