import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPass] = useState('');
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        const user = { email, password };
        fetch('http://localhost:5094/api/User/login', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(user)
        }).then(response => response.text())
            .then(data => localStorage.setItem('token', data))
          .then(console.log(localStorage.getItem('token')))
          .then(() => { navigate('/profile', { state: { email: email} }) })
    }

    return (
        <div className="auth-form-container">
        <h2>Login</h2>
            <form className="login-form" onSubmit={handleSubmit}>
                <label htmlFor="email">Email</label>
                <input value={email} onChange={(e) => setEmail(e.target.value)} type={email} placeholder="Enter your email" id="email" name="email" />
                <label htmlFor="password">Password</label>
                <input value={password} onChange={(e) => setPass(e.target.value)} type="password" placeholder="Enter your password" id="password" name="password" />
                <button type="submit">Log in</button>
            </form>
            <button className="link-btn" onClick={() => navigate('/register')}>Dont have account? Register here</button>
        </div>
  )
}

export default Login;