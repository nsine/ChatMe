let templateUrl = require('ngtemplate!html!./app.html');

/* @ngInject */
export default function app() {
    var directive = {
        templateUrl: templateUrl
    };
    return directive;
}