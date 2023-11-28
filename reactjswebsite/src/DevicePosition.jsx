import { useState } from 'react';
import { APIProvider, Map, AdvancedMarker, Pin, InfoWindow } from "@vis.gl/react-google-maps";

export const DevicePosition = () => {
    const position = [10.762622, 106.660172];

    return (
        <APIProvider>
            <div>React Google Maps</div>
        </APIProvider>
    );
}

export default DevicePosition;