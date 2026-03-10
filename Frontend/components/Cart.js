import { useState } from "react";

export default function Cart({ cart, apiBase, onChange }) {
  const [loading, setLoading] = useState(false);

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

  async function updateItem(itemId, qty) {
    try {
      setLoading(true);
      const res = await fetch(`${apiBase}/api/cart/update`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ cartItemId: itemId, quantity: qty }),
      });
      if (!res.ok) throw new Error("ไม่สามารถอัพเดตจำนวนสินค้าได้");
      sendAlert("อัพเดตจำนวนสินค้าแล้ว", "success");
      onChange();
    } catch (err) {
      sendAlert(err.message || "Error", "error");
    } finally {
      setLoading(false);
    }
  }

  async function removeItem(itemId) {
    try {
      setLoading(true);
      const res = await fetch(`${apiBase}/api/cart/${itemId}`, {
        method: "DELETE",
      });
      if (!res.ok) throw new Error("ไม่สามารถลบสินค้าได้");
      sendAlert("ลบสินค้าแล้ว", "info");
      onChange();
    } catch (err) {
      sendAlert(err.message || "Error", "error");
    } finally {
      setLoading(false);
    }
  }

  async function clearCart() {
    if (!confirm("ยืนยันการล้างตะกร้าทั้งหมด?")) return;
    try {
      setLoading(true);
      const res = await fetch(`${apiBase}/api/cart/clear`, {
        method: "DELETE",
      });
      if (!res.ok) throw new Error("ไม่สามารถล้างตะกร้าได้");
      sendAlert("ล้างตะกร้าเรียบร้อยแล้ว", "info");
      onChange();
    } catch (err) {
      sendAlert(err.message || "Error", "error");
    } finally {
      setLoading(false);
    }
  }

  if (!cart || !cart.items) return <p className="muted">ตะกร้าว่าง</p>;

  const total = cart.items.reduce(
    (s, it) => s + (it.unitPrice || it.price || 0) * (it.quantity || 0),
    0,
  );

  return (
    <div className="cart">
      <h2>ตะกร้าสินค้า</h2>
      <div className="cart-table-wrap">
        <table className="cart-table">
          <thead>
            <tr>
              <th className="cart-col-item">สินค้า</th>
              <th className="cart-col-price">ราคา</th>
              <th className="cart-col-qty">จำนวน</th>
              <th className="cart-col-total">รวม</th>
              <th className="cart-col-action"></th>
            </tr>
          </thead>
          <tbody>
            {cart.items.map((it) => (
              <tr key={it.id}>
                <td className="cart-item">{it.productName || it.name}</td>
                <td className="cart-num">
                  {(it.unitPrice || it.price || 0).toLocaleString()}
                </td>
                <td className="cart-qty">
                  <button
                    className="btn small"
                    onClick={() =>
                      updateItem(it.id, Math.max(1, it.quantity - 1))
                    }
                    disabled={loading}
                  >
                    -
                  </button>
                  <span className="qty-value">{it.quantity}</span>
                  <button
                    className="btn small"
                    onClick={() => updateItem(it.id, it.quantity + 1)}
                    disabled={loading}
                  >
                    +
                  </button>
                </td>
                <td className="cart-num">
                  {(
                    (it.unitPrice || it.price || 0) * it.quantity || 0
                  ).toLocaleString()}
                </td>
                <td className="cart-action">
                  <button
                    className="btn btn-ghost"
                    onClick={() => removeItem(it.id)}
                    disabled={loading}
                  >
                    ลบ
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="cart-actions">
        <div>
          <button
            className="btn btn-danger"
            onClick={clearCart}
            disabled={loading}
          >
            ล้างตะกร้า
          </button>
        </div>
        <div className="cart-total">
          <strong>ยอดรวม: {total.toLocaleString()}</strong>
        </div>
      </div>
    </div>
  );
}
