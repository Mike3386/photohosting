import React from 'react'
import Route from 'react-router-dom';
import Login from './login.jsx'

const Content = () => (
    <div className="container">
        <main className="content">
            <switch>
                <Route exact path="/" component={Login}/>
            </switch>
        </main>
    </div>
);

export default Content;