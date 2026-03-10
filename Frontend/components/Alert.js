import { useEffect, useState } from "react";

export default function AlertProvider({ children }) {
  const [alerts, setAlerts] = useState([]);

  useEffect(() => {
    function onAlert(e) {
      const { message, type = "info", timeout = 4000 } = e.detail || {};
      const id = Date.now() + Math.random();
      setAlerts((s) => [...s, { id, message, type }]);
      if (timeout > 0) {
        setTimeout(() => {
          setAlerts((s) => s.filter((a) => a.id !== id));
        }, timeout);
      }
    }
    window.addEventListener("app-alert", onAlert);
    return () => window.removeEventListener("app-alert", onAlert);
  }, []);

  function dismiss(id) {
    setAlerts((s) => s.filter((a) => a.id !== id));
  }

  return (
    <>
      {children}
      <div className="alert-container" aria-live="polite">
        {alerts.map((a) => (
          <div key={a.id} className={`alert alert--${a.type}`}>
            <div className="alert-message">{a.message}</div>
            <button className="alert-close" onClick={() => dismiss(a.id)}>
              ×
            </button>
          </div>
        ))}
      </div>
    </>
  );
}
