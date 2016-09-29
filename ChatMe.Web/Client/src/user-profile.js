require('./static/js/push-messages');
require('./posts-app/app.module.js');

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length,c.length);
        }
    }
    return "";
}

function changeFollow(isFollowingNow) {
    var headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return fetch('/api/activity/follow', {
        method: 'post',
        headers: headers,
        body: JSON.stringify({
            UserId: getCookie('currentUserId'),
            FollowingUserId: getCookie('userPageId')
        })
    });
}

$('.follow-btn').on('click', function (e) {
    e.preventDefault();
    var $button = $(this);
    if ($button.hasClass('following')) {
        changeFollow(true).then(function () {
            $button.removeClass('following').removeClass('w3-red');
            $button.addClass('w3-blue');
            $button.html('Follow <i class="fa fa-eye"></i>');
        });
    } else {
        changeFollow(true).then(function () {
            $button.removeClass('w3-blue');
            $button.addClass('w3-red').addClass('following');
            $button.html('Unfollow <i class="fa fa-eye-slash"></i>');
        })
    }
});