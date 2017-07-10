const React = require('react');
const ReactDOM = require ('react-dom');


class Hello extends React.Component {
    render() {
        return <h1>Привет, React.JS.</h1>;
    }
}
ReactDOM.render(
  <Hello />,
  document.getElementById("content")
);