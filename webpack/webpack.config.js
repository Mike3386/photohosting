module.exports = {
  entry: __dirname + '/jsx/app.jsx',
  output: {
    path: __dirname + "/../photohosting/wwwroot/js/",
    filename: 'bundle.js'
  },
  devtool: '#sourcemap',
  module: {
    loaders: [
      {
        test: /\.css$/,
        loader: 'style-loader!css-loader' },
      {
        test: /\.jsx?$/,
        exclude: /(node_modules)/,
        loaders: ['babel-loader']
      }
    ]
  }
}

