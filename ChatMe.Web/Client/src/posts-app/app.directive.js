let templateUrl = require('ngtemplate!html!./app.html');

/* @ngInject */
export default function app() {
    let directive = {
        templateUrl: templateUrl
    };
    return directive;
}