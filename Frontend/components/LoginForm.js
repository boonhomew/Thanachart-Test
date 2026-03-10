import { useState } from "react";
import { useRouter } from "next/router";

const API_BASE = process.env.NEXT_PUBLIC_API_BASE || "http://localhost:5000";

export default function LoginForm() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const router = useRouter();

  async function handleSubmit(e) {
    e.preventDefault();
    setError(null);
    try {
      setLoading(true);
      const res = await fetch(`${API_BASE}/api/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
      });
      if (!res.ok) {
        const txt = await res.text();
        throw new Error(txt || "Login failed");
      }
      const data = await res.json();
      const token = data.token || data.Token || data.accessToken || data.access_token;
      if (!token) throw new Error("ไม่มี token กลับมา");
      localStorage.setItem("token", token);
      localStorage.setItem("user", JSON.stringify({ username: data.username || data.Username || username }));
      router.push("/");
    } catch (err) {
      setError(err.message || "เกิดข้อผิดพลาด");
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: 420 }}>
      <div style={{ marginBottom: 8 }}>
        <label>Username</label>
        <input value={username} onChange={(e) => setUsername(e.target.value)} required style={{ width: "100%" }} />
      </div>
      <div style={{ marginBottom: 8 }}>
        <label>Password</label>
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required style={{ width: "100%" }} />
      </div>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <div>
        <button type="submit" disabled={loading}>{loading ? "กำลังเข้าสู่ระบบ..." : "เข้าสู่ระบบ"}</button>
      </div>
    </form>
  );
}
