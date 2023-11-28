import { useLocation } from 'react-router-dom';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const UpdateAvatar = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const [email] = useState(location.state.email)
    const [avatarFile, setAvatarFile] = useState(null);
    const [avatarUri, setAvatarUri] = useState(null);

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            setAvatarFile(file);

            const reader = new FileReader();
            reader.onload = () => {
                setAvatarUri(reader.result);
            };
            reader.readAsDataURL(file);
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('email', email)
        formData.append('file', avatarFile);
        fetch('http://localhost:5094/api/User/avatar', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
            body: formData
        }).then(() => { navigate('/profile', { state: { email: email } }) })
    }

    return (
        <div className="auth-form-container">
            <h2>Update Avatar</h2>
            <form className="register-form" onSubmit={handleSubmit}>
                <input type="file" accept="image/*" onChange={handleFileChange} />
                {avatarUri && (
                    <div>
                        <img src={avatarUri} alt="Avatar Preview" style={{ maxWidth: '100%', maxHeight: '200px' }} />
                    </div>
                )}
                <button type="submit">Save Avatar</button>
            </form>
            <button className="link-btn" onClick={() => navigate(-1)}>Back</button>
        </div>
    );
}

export default UpdateAvatar;