/// <binding BeforeBuild='Run - Development' />
"use strict";

module.exports = {
    entry: {
        index: "./Client/src/index.js",
        postsApp: "./Client/src/posts-app.js",
        dialogsApp: "./Client/src/dialogs-app.js",
        settings: "./Client/src/settings.js",
        admin: "./Client/src/admin.js"
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