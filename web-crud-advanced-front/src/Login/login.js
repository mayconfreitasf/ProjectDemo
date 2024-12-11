
import './login.css';
import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';

function Login() {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();
    const navigate = useNavigate();
    /*set HTTPS=true; npm start  */

    const UserLogin = () => {
        const User = {
            Username: username,
            Password: password,
        };

        const apiUrl = 'https://localhost:7056/login';
        fetch(apiUrl, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'  
            },
            body: JSON.stringify(User)  
        })
        .then((response) => { return response.json(); })
        .then((data) => { 

            if (data.token) {

                localStorage.setItem("jwt_token", data.token);
                localStorage.setItem('loggedUser', JSON.stringify(data.user));
                navigate('/home');
            }
        })
        .catch((error) => { console.error('Erro:', error); });
    }

    return(
    <div className="page pageLogin">
        <div className="formLogin">
            <main role="main" className="pb-3">
                <form id="form-login" onSubmit={(e) => { e.preventDefault(); UserLogin();  }}>
                    <div className="form-group">
                        <label>User</label>
                        <input type="text" id="username" name="Username" className="form-control"
                            value={username} onChange={ (e)=> setUserName(e.target.value)}></input>
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" id="password" name="Password" className="form-control"
                            value={password} onChange={ (e)=> setPassword(e.target.value)}></input>
                    </div>
                    <hr />
                    <button type="submit" className="btn btn-primary">Login</button>
                    <hr />
                    <div className="form-group">
                        <label>Do not have account?</label>
                        
                        <Link to="/createUser">Cick here</Link>
                    </div>
                </form>
            </main>
        </div>
    </div>

    );
}

export default Login;