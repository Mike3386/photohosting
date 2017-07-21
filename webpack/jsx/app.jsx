import  React from 'react'
import ReactDOM from 'react-dom'
import App from './main.jsx'
import { BrowserRouter } from 'react-router-dom'

require("../css/main.css");

ReactDOM.render(
    (
        <BrowserRouter>
            <App />
        </BrowserRouter>
    ),
    document.getElementById("content")
);

/*
import axios from 'axios';


 class Hello extends React.Component {
 constructor(props) {
 super(props);

 this.state = {users:[]}
 }

 render(){
 if(this.state.users.length === 0)
 return (
 <h1>No users</h1>
 );
 else return (
 <ul>
 {this.state.users.map((user)=> (<li> {user} </li>))}
 </ul>
 )
 }

 async componentDidMount() {
 let res = await axios({
 method: 'get',
 url: '/api/users',
 responseType:'json'
 });
 const users = res.data.map(obj => obj);
 this.setState({ users });
 }
 }
 */