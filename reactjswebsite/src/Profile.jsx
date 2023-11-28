import { useLocation } from 'react-router-dom';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Profile = () => {
    const location = useLocation();
    const [username, setUsername] = useState('')
    const [name, setName] = useState('')
    const [role, setRole] = useState('')
    const [avatarUri, setAvatarUri] = useState(null);
    const [imageSrc, setImageSrc] = useState(null);
    const navigate = useNavigate();

    fetch("http://localhost:5094/api/User/profile/" + location.state.email, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
        .then(response => response.json())
        .then(data => { setUsername(data.username), setName(data.name), setRole(data.role), setAvatarUri(data.avatarUri) });

    if (avatarUri !== null) {
        fetch("http://localhost:5094/api/User/avatar/" + avatarUri, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
        }).then(response => response.json())
            .then(data => setImageSrc('data:image/jpeg;base64,' + data.fileContents));
    }

    const handleUpdateProfile = (e) => {
        e.preventDefault();
        navigate('/UpdateProfile', { state: { email: location.state.email, username: username, name: name } })
    }

    const handleUpdateAvatar = (e) => {
        e.preventDefault();
        navigate('/UpdateAvatar', { state: { email: location.state.email} })
    }

    const handleLogout = (e) => {
        e.preventDefault();
        navigate('/login')
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
                </tbody>
            </table>
            {avatarUri && (
                <img src={imageSrc} alt="Avatar" style={{ maxWidth: '100%', maxHeight: '200px' }} />
            )}
            <br></br>
            <button type="submit" onClick={handleUpdateProfile}>Update Profile</button>
            <br></br>
            <button type="submit" onClick={handleUpdateAvatar}>Update Avatar</button>
            <br></br>
            <button type="submit" onClick={handleLogout}>Log out</button>
        </div>
    )
}

export default Profile;