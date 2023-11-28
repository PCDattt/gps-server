import { useLocation } from 'react-router-dom';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const UpdateProfile = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const [email] = useState(location.state.email)
    const [username, setUsername] = useState(location.state.username)
    const [name, setName] = useState(location.state.name)
    const [password, setPass] = useState('');
    const [confirmPass, setConfirmPass] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (password === confirmPass) {
            const user = { email, username, name, password };
            fetch('http://localhost:5094/api/User', {
                method: 'PUT',
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                },
                body: JSON.stringify(user)
            })
            navigate('/login')
        }
    }

    return (
        <div className="auth-form-container">
            <h2>Update Profile</h2>
            <form className="register-form" onSubmit={handleSubmit}>
                <label htmlFor="username">Username</label>
                <input value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Enter your username" id="username" name="username" />
                <label htmlFor="fullname">Full Name</label>
                <input value={name} onChange={(e) => setName(e.target.value)} placeholder="Enter your fullname" id="fullname" name="fullname" />
                <label htmlFor="email">Password</label>
                <input value={password} onChange={(e) => setPass(e.target.value)} type="password" placeholder="Enter your new password" id="password" name="password" />
                <label htmlFor="confirmPassword">Confirm Password</label>
                <input value={confirmPass} onChange={(e) => setConfirmPass(e.target.value)} type="password" placeholder="Confirm your password" id="confirmPassword" name="confirmPassword" />
                <button type="submit">Confirm Update</button>
            </form>
            <button className="link-btn" onClick={() => navigate(-1)}>Back</button>
        </div>
    );
}

export default UpdateProfile;