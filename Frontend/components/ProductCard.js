import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

export default function ProductCard({ product, apiBase }) {
  const [adding, setAdding] = useState(false);
  const stock = product.stockQuantity ?? 0;
  const router = useRouter();

  async function addToCart() {
    function sendAlert(message, type = "info") {
      try {
        window.dispatchEvent(
          new CustomEvent("app-alert", { detail: { message, type } }),
        );
      } catch (e) {
        // fallback
        alert(message);
      }
    }

    if (stock <= 0) {
      sendAlert("สินค้าหมดสต๊อก", "error");
      return;
    }
    try {
      setAdding(true);
      const res = await fetch(`${apiBase}/api/cart/add`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          productId: product.id || product.productId,
          quantity: 1,
        }),
      });
      if (!res.ok) {
        const txt = await res.text();
        throw new Error(txt || "ไม่สามารถเพิ่มสินค้าลงตะกร้าได้");
      }
      sendAlert("เพิ่มลงตะกร้าแล้ว", "success");
      router.push("/cart");
    } catch (err) {
      sendAlert(err.message || "Error", "error");
    } finally {
      setAdding(false);
    }
  }

  return (
    <div className="card">
      <h3 className="card-title">
        {product.name || product.productName || "ไม่ระบุชื่อ"}
      </h3>
      <p className="muted">รหัส: {product.productSku}</p>
      <p className="muted">
        ราคา: {(product.productPrice || 0).toLocaleString()}
      </p>
      <p className="muted">คงเหลือ: {stock}</p>
      <div className="card-actions">
        <button
          className="btn"
          onClick={addToCart}
          disabled={adding || stock <= 0}
        >
          {stock <= 0 ? "หมดสต๊อก" : adding ? "กำลังเพิ่ม..." : "เพิ่มลงตะกร้า"}
        </button>
        <Link
          className="btn-link"
          href={`/product/${product.id || product.productId}`}
        >
          รายละเอียด
        </Link>
      </div>
    </div>
  );
}
