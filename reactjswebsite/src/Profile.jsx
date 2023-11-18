import { useLocation } from 'react-router-dom';
import { useState } from 'react';

export const Profile = () => {
    const location = useLocation();
    const [username, setUsername] = useState('')
    const [fullname, setFullname] = useState('')
    const [role, setRole] = useState('')
    const [avatarUri, setAvatarUri] = useState('')


    fetch("http://localhost:5094/api/User/profile/" + location.state.email)
        .then(response => response.json())
        .then(data => { setUsername(data.username), setFullname(data.name), setRole(data.role), setAvatarUri(data.avatarUri) })

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
                        <td>{fullname}</td>
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
        </div>
    )
}

export default Profile;