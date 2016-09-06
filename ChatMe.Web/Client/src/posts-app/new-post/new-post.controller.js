/* @ngInject */
export default function NewPostController(postsService, $timeout, userInfo) {
    var self = this;

    self.isMyPage = userInfo.isOwner;
    self.postBody = "";
    self.isSendBtnVisible = false;
    self.textRows = 1;

    self.sendPost = function () {
        if (self.postBody !== "") {
            postsService
                .newPost({
                    body: self.postBody
                })
                .then(function () {
                    self.postBody = "";
                });
        }
    }

    self.activateForm = function ()  {
        self.isSendBtnVisible = true;
        self.textRows = 10;
    }

    self.deactivateForm = function (event) {
        if (event.relatedTarget != null &&
            event.relatedTarget.id == 'send-post-btn') {

            return;
        }
        // $timeout(function () {
            self.isSendBtnVisible = false;
            self.textRows = 1;
        // }, 100);
    }
}