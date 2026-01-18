import { useEffect, useRef, useState } from "react";
import { MapContainer, TileLayer, Marker, Popup, useMap } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-routing-machine/dist/leaflet-routing-machine.css";
import "leaflet-routing-machine";

// фікс для маркерів з сайту
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png",
  iconUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png",
});

function Routing({ start, end, onRouteFound }) {
  const map = useMap();
  const routingRef = useRef(null);

  useEffect(() => {
    if (!map || !start || !end) return;

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

    routingRef.current.setWaypoints([
      L.latLng(start[0], start[1]),
      L.latLng(end[0], end[1]),
    ]);

    const handleRoutesFound = (e) => {
      const route = e.routes[0];
      const distance = route.summary.totalDistance; // метри
      const duration = route.summary.totalTime; // секунди

      console.log("ROUTE FOUND:", distance, duration);
      if (onRouteFound) {
        onRouteFound({ distance, duration });
      }
    };

    // ставимо слухач
    routingRef.current.on("routesfound", handleRoutesFound);

    return () => {
      // знімаємо слухач перед наступним effect
      routingRef.current.off("routesfound", handleRoutesFound);
    };
  }, [map, start, end, onRouteFound]);
}

export default function CallMap({ startAddress, endAddress, onRouteFound }) {
  const mapRef = useRef();
  const [startPos, setStartPos] = useState(null);
  const [endPos, setEndPos] = useState(null);

  const geocode = async (address) => {
    if (!address) return null;
    // перша спроба з обмеженням по країні (швидше і точніше для UA)
    let res = await fetch(
      `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(
        address
      )}&limit=1&countrycodes=ua`
    );
    let data = await res.json();

    // якщо нічого не знайдено, пробуємо ще раз без обмеження по країні
    if (!data || data.length === 0) {
      console.log(
        "[CallMap] geocode: no result with countrycodes=ua, retrying without country filter for",
        address
      );
      res = await fetch(
        `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(
          address
        )}&limit=1`
      );
      data = await res.json();
    }

    if (!data || data.length === 0) return null;

    return [parseFloat(data[0].lat), parseFloat(data[0].lon)];
  };

  useEffect(() => {
    const load = async () => {
      console.log(
        "[CallMap] geocode startAddress=",
        startAddress,
        "endAddress=",
        endAddress
      );
      const s = await geocode(startAddress);
      const e = await geocode(endAddress);
      console.log("[CallMap] geocode results s=", s, " e=", e);

      if (s) setStartPos(s);
      else setStartPos(null);
      if (e) setEndPos(e);
      else setEndPos(null);
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
        <TileLayer url="https://{s}.tile.openstreetmap.fr/osmfr/{z}/{x}/{y}.png" />

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

        {startPos && endPos && (
          <Routing start={startPos} end={endPos} onRouteFound={onRouteFound} />
        )}
      </MapContainer>
    </div>
  );
}
