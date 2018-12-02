const path = require('path');

module.exports = {
    entry: './assets/js/application.js',
    output: {
        path: path.resolve(__dirname, 'wwwroot/assets'),
        filename: 'application.js'
    },
    resolve: {
        modules: [path.resolve(__dirname, 'assets'), 'node_modules']
    },
    module: {
        rules: [
            {
                test: /\.elm$/,
                exclude: [/elm-stuff/, /node_modules/],
                use: {
                    loader: 'elm-webpack-loader',
                    options: {}
                }
            },
            {
                test: /\.css$/,
                use: [
                    { loader: "style-loader" },
                    { loader: "css-loader" }
                ]
            },
            {
                test: /\.scss$|\.sass$/,
                use: [
                    { loader: "style-loader" },
                    { loader: "css-loader" },
                    {
                        loader: 'postcss-loader', // Run post css actions
                        options: {
                            plugins: function () { // post css plugins, can be exported to postcss.config.js
                                return [
                                    require('precss'),
                                    require('autoprefixer')
                                ];
                            }
                        }
                    },
                    { loader: "sass-loader" }
                ]
            }
        ]
    }
};