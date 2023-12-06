import { useState, useEffect, useRef } from 'react';
import { useLocation } from 'react-router-dom';
import "leaflet/dist/leaflet.css";
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';

export const DevicePosition = () => {
    const location = useLocation();

    const [devices, setDevices] = useState([]);
    const [devicesPosition, setDevicesPosition] = useState([]);

    const mapRef = useRef(null);

    const fetchData = async () => {
        const response = await fetch("http://localhost:5094/api/Device/user/" + location.state.email, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
        });
        const data = await response.json();
        setDevices(data);

        // Clear devicesPosition array before re-fetching positions
        setDevicesPosition([]);

        const positionDataPromises = data.map(async (device) => {
            const response = await fetch("http://localhost:5094/api/DevicePacket/position/" + device.id, {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                }
            });
            const positionData = await response.json();
            return positionData;
        });

        const allPositionData = await Promise.all(positionDataPromises);
        setDevicesPosition(allPositionData);

        // Update map center based on the first device's position
        if (allPositionData.length > 0 && mapRef.current) {
            mapRef.current.setView([allPositionData[0].latitude, allPositionData[0].longitude], mapRef.current.getZoom());
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    // Re-fetch device positions every 5 seconds
    useEffect(() => {
        const intervalId = setInterval(() => {
            fetchData(); // Call fetchData to refresh device positions
        }, 5000);

        // Cleanup interval on component unmount
        return () => clearInterval(intervalId);
    }, [devices]);

    return (
        <MapContainer center={[0, 0]} zoom={13} ref={mapRef}>
            <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
            />

            {devicesPosition.map((position, index) => (
                <Marker key={index} position={[position.latitude, position.longitude]}>
                    <Popup>
                        Hello {index}
                    </Popup>
                </Marker>
            ))}
        </MapContainer>
    );
}

export default DevicePosition;


