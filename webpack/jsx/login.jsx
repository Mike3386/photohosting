import React, {Component} from 'react'
import ReactReadux from 'react-redux'
import Redux from 'redux'

const EAction = {
    FORM_AUTH_LOGIN_UPDATE    : "FORM_AUTH_LOGIN_UPDATE",
    FORM_AUTH_PASSWORD_UPDATE : "FORM_AUTH_PASSWORD_UPDATE",
    FORM_AUTH_RESET           : "FORM_AUTH_RESET",
    FORM_AUTH_AUTOFILL        : "FORM_AUTH_AUTOFILL"
};

function reducer(state = {login:"", password:""}, action){
    switch(action.type){
        case EAction.FORM_AUTH_LOGIN_UPDATE:
            return {
                ...state,
                login : action.login
            };
        case EAction.FORM_AUTH_PASSWORD_UPDATE:
            return {
                ...state,
                password : action.password
            };
        case EAction.FORM_AUTH_RESET:
            return {
                ...state,
                login : "",
                password : ""
            };
        case EAction.FORM_AUTH_AUTOFILL:
            return {
                ...state,
                login : action.login,
                password : action.password
            };
        default:
            return state;
    }
}

function loginUpdate(event) {
    return {
        type : EAction.FORM_AUTH_LOGIN_UPDATE,
        login : event.target.value
    };
}

function passwordUpdate(event) {
    return {
        type : EAction.FORM_AUTH_PASSWORD_UPDATE,
        password : event.target.value
    };
}

function reset() {
    return {
        type : EAction.FORM_AUTH_RESET
    };
}

function tryAutoFill() {
    if(cookies && (cookies.login !== undefined) && (cookies.password !== undefined)) {
        return {
            type : EAction.FORM_AUTH_AUTOFILL,
            login : cookies.login,
            password : cookies.password
        };
    } else {
        return {};
    }
}

function submit() {
    return function(dispatch, getState) {
        const state = getState();
        dispatch(reset());
        request('/auth/', {send: {
            login : state.login,
            password : state.password
        }}).then(function() {
            router.push('/');
        }).catch(function() {
            window.alert("Auth failed")
        });
    }
}

let  connect = ReactRedux.connect;
let bindActionCreators = Redux.bindActionCreators;

const FormAuthController = connect(
    state => ({
        login : state.login,
        password : state.password
    }),
    dispatch => bindActionCreators({
        loginUpdate,
        passwordUpdate,
        reset,
        tryAutoFill,
        submit
    }, dispatch)
)(FormAuthView);

class Login extends Component{
    constructor(props){
        super(props);


    }
    componentWillMount() {
        this.props.tryAutoFill();
    }
    render() {
        return (
            <div>
                <input
                    type = "text"
                    value = {this.props.login}
                    onChange = {this.props.loginUpdate}
                />
                <input
                    type = "password"
                    value = {this.props.password}
                    onChange = {this.props.passwordUpdate}
                />
                <button onClick = {this.props.submit}>
                    Submit
                </button>
            </div>
        );
    }


}

export default Login;