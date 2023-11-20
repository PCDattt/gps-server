import { useLocation } from 'react-router-dom';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Profile = () => {
    const location = useLocation();
    const [username, setUsername] = useState('')
    const [name, setName] = useState('')
    const [role, setRole] = useState('')
    const [avatarUri, setAvatarUri] = useState('')
    const navigate = useNavigate();


    fetch("http://localhost:5094/api/User/profile/" + location.state.email)
        .then(response => response.json())
        .then(data => { setUsername(data.username), setName(data.name), setRole(data.role), setAvatarUri(data.avatarUri) });

    const handleSubmit = (e) => {
        e.preventDefault();
        navigate('/UpdateProfile', { state: { email: location.state.email, username: username, name: name } })
    }

    return (
        <div className="auth-form-container">
            <h2>User Profile</h2>
            <table>
                <tbody>
                    <tr>
                        <td>Username</td>
                        <td>{username}</td>
                    </tr>
                    <tr>
                        <td>Email</td>
                        <td>{location.state.email}</td>
                    </tr>
                    <tr>
                        <td>Fullname</td>
                        <td>{name}</td>
                    </tr>
                    <tr>
                        <td>Role</td>
                        <td>{role}</td>
                    </tr>
                    <tr>
                        <td>Avatar</td>
                        <td>{avatarUri}</td>
                    </tr>
                </tbody>
            </table>
            <button type="submit" onClick={handleSubmit}>Update</button>
        </div>
    )
}

export default Profile;