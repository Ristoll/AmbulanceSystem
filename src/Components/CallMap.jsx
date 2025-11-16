import { useEffect, useRef, useState } from "react";
import { MapContainer, TileLayer, Marker, Popup, useMap } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-routing-machine/dist/leaflet-routing-machine.css";
import "leaflet-routing-machine";

function Routing({ start, end, onRouteFound }) {
  const map = useMap();
  const routingRef = useRef(null);

  useEffect(() => {
    if (!map) return;

    if (!routingRef.current) {
      routingRef.current = L.Routing.control({
        waypoints: [],
        addWaypoints: false,
        draggableWaypoints: false,
        showAlternatives: false,
        routeWhileDragging: false,
        lineOptions: {
          styles: [{ color: "#e11d48", weight: 6 }],
        },
      }).addTo(map);
    }

    // урааа просто оновлюємо маршрут
    if (start && end) {
      routingRef.current.setWaypoints([
        L.latLng(start[0], start[1]),
        L.latLng(end[0], end[1]),
      ]);
    }

  }, [map, start, end]);

  return null;
}

export default function CallMap({ startAddress, endAddress }) {
  const mapRef = useRef();
  const [startPos, setStartPos] = useState(null);
  const [endPos, setEndPos] = useState(null);

  const geocode = async (address) => {
    if (!address) return null;

    const res = await fetch(
      `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(address)}&limit=1&countrycodes=ua`
    );
    const data = await res.json();

    if (data.length === 0) return null;

    return [parseFloat(data[0].lat), parseFloat(data[0].lon)];
  };

  useEffect(() => {
    const load = async () => {
      const s = await geocode(startAddress);
      const e = await geocode(endAddress);

      if (s) setStartPos(s);
      if (e) setEndPos(e);
    };

    load();
  }, [startAddress, endAddress]);

  return (
    <div style={{ width: "100%", height: "320px" }}>
      <MapContainer
        center={startPos || [50.45, 30.52]}
        zoom={13}
        style={{ height: "100%", width: "100%" }}
        whenCreated={(map) => (mapRef.current = map)}
      >
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

        {startPos && (
          <Marker position={startPos}>
            <Popup>Адреса виклику: {startAddress}</Popup>
          </Marker>
        )}

        {endPos && (
          <Marker position={endPos}>
            <Popup>Лікарня: {endAddress}</Popup>
          </Marker>
        )}

        {startPos && endPos && <Routing start={startPos} end={endPos} />}
      </MapContainer>
    </div>
  );
}
