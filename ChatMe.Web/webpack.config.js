/// <binding />
"use strict";

module.exports = {
    entry: {
        index: "./Client/src/index.js",
        allUsers: "./Client/src/all-users.js",
        dialogs: "./Client/src/dialogs.js",
        news: "./Client/src/news.js",
        settings: "./Client/src/settings.js",
        userProfile: "./Client/src/user-profile.js"
    },
    output: {
        filename: "./Client/dist/[name].js"
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: "babel-loader"
            },
            {
                test: /\.scss$/,
                loader: "style!css!sass"
            }
        ]
    },
    devtool: 'source-map'
};