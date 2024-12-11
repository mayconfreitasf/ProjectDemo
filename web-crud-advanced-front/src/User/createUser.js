import './createUser.css';
import { Link } from 'react-router-dom';
import { useState } from 'react';

function CreateUser(){
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();

    

    const CreateUser = () => {
        const User = {
            Username: username,
            Password: password
        };

        setUserName('');
        setPassword('');
        console.log(JSON.stringify(User));
    
        const apiUrl = 'https://localhost:7056/register';
        fetch(apiUrl, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'  
            },
            body: JSON.stringify(User)  
        })
        .then((response) => { return response.json(); })
        .then((datas) => { console.log('Response:', datas); })
        .catch((error) => { console.error('Erro:', error); });
    }

    return(
        <div className="page pageRegister">
            <div className="formLogin">
                <main role="main" className="pb-3">
                    <form id="form-login" onSubmit={(e) => { e.preventDefault(); CreateUser();  }}>
                        <h4>New user</h4>
                        <div className="form-group">
                            <label>Username </label>
                            <input type="text" id="username" name="Username" className="form-control"
                            value={username} onChange={ (e)=> setUserName(e.target.value)}></input>
                            <span className="text-danger"></span>
                        </div>
                        <div className="form-group">
                            <label>Password</label>
                            <input type="password" id="password" name="Password" className="form-control"
                            value={password} onChange={ (e)=> setPassword(e.target.value)}></input>
                            <span className="text-danger"></span>
                        </div>
                        <button type="submit" className="btn btn-primary">Register</button>
                        <hr />
                        <div className="form-group">
                            <label>Already have an account?</label>
                            <Link to="/">Cick here</Link>
                        </div>
                    </form>
                </main>
            </div>
        </div>
    );
}

export default CreateUser;