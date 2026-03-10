import { useEffect, useState } from "react";
import Head from "next/head";
import Cart from "@/components/Cart";
import { useRouter } from "next/navigation";

const API_BASE = process.env.NEXT_PUBLIC_API_BASE || "http://localhost:5000";

export default function CartPage() {
  const [cart, setCart] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const router = useRouter();

  async function fetchCart() {
    try {
      setLoading(true);
      const res = await fetch(`${API_BASE}/api/cart`);
      if (!res.ok) throw new Error("ไม่สามารถดึงตะกร้าได้");
      const data = await res.json();
      setCart(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchCart();
  }, []);

  async function handleCheckout() {
    if (!confirm("ยืนยันการชำระเงิน?")) return;
    try {
      const res = await fetch(`${API_BASE}/api/order/checkout`, {
        method: "POST",
      });
      if (!res.ok) {
        const txt = await res.text();
        throw new Error(txt || "Checkout failed");
      }
      alert("ชำระเงินสำเร็จ");
      fetchCart();
      router.push("/products/");
    } catch (err) {
      alert(err.message || "Error");
    }
  }

  return (
    <>
      <Head>
        <title>ตะกร้าสินค้า</title>
      </Head>
      <div style={{ padding: 16 }}>
        <h1>ตะกร้าสินค้า</h1>
        {loading && <p>กำลังโหลด...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}
        {!loading && cart && (
          <>
            <Cart cart={cart} apiBase={API_BASE} onChange={fetchCart} />
            <div style={{ marginTop: 16 }}>
              <button onClick={handleCheckout}>ชำระเงิน (Check out)</button>
            </div>
          </>
        )}
      </div>
    </>
  );
}
