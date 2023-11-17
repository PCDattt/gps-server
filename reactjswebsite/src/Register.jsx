import { useState } from 'react';
import { redirect } from 'react-router-dom';

export const Register = (props) => {
    const [email, setEmail] = useState('');
    const [password, setPass] = useState('');
    const [username, setUsername] = useState('');
    const [name, setFullname] = useState('');
    const [confirmPass, setConfirmPass] = useState('');
    const [role] = useState('user');

    const handleSubmit = (e) => {
        e.preventDefault();
        const user = { username, password, email, name, role };
        redirect('/login');
        fetch('http://localhost:5094/api/User', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(user)
        }).then(() => {redirect('/login')})
    }

    return (
        <div className="auth-form-container">
            <h2>Register</h2>
            <form className="register-form" onSubmit={handleSubmit}>
                <label htmlFor="username">Username</label>
                <input value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Enter your username" id="username" name="username" />
                <label htmlFor="fullname">Full Name</label>
                <input value={name} onChange={(e) => setFullname(e.target.value)} placeholder="Enter your fullname" id="fullname" name="fullname" />
                <label htmlFor="email">Email</label>
                <input value={email} onChange={(e) => setEmail(e.target.value)} type="email" placeholder="Enter your email" id="email" name="email" />
                <label htmlFor="password">Password</label>
                <input value={password} onChange={(e) => setPass(e.target.value)} type="password" placeholder="Enter your password" id="password" name="password" />
                <label htmlFor="confirmPassword">Confirm Password</label>
                <input value={confirmPass} onChange={(e) => setConfirmPass(e.target.value)} type="password" placeholder="Confirm your password" id="confirmPassword" name="confirmPassword" />
                <button type="submit">Register</button>
            </form>
            <button className="link-btn" onClick={() => props.onFormSwitch('login')}>Already have account? Login here</button>
        </div>
    )
}

export default Register;