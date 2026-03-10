import Styles from "@/styles/Home.module.css";
import Head from "next/head";
import Link from "next/link";
import { useEffect, useState } from "react";
import ProductCard from "@/components/ProductCard";

const API_BASE = process.env.NEXT_PUBLIC_API_BASE || "http://localhost:5000";

export default function ProductsPage() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchProducts() {
      try {
        setLoading(true);
        const res = await fetch(`${API_BASE}/api/products`);
        if (!res.ok) throw new Error("Failed to load products");
        const data = await res.json();

        console.log("API Result:", data);

        setProducts(data || []);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    }
    fetchProducts();
  }, []);

  return (
    <>
      <Head>
        <title>สินค้า</title>
        <meta name="description" content="รายการสินค้า" />
      </Head>

      <div className={Styles.container}>
        <header
          style={{
            display: "flex",
            justifyContent: "space-between",
            width: "100%",
          }}
        >
          <h1 className={Styles.title}>รายการสินค้า</h1>
        </header>

        {loading && <p>กำลังโหลดสินค้า...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}

        <div
          style={{
            display: "grid",
            gridTemplateColumns: "repeat(auto-fit, minmax(220px, 1fr))",
            gap: 16,
            width: "100%",
          }}
        >
          {products.map((p) => (
            <ProductCard
              key={p.id || p.productId}
              product={p}
              apiBase={API_BASE}
            />
          ))}
        </div>
      </div>
    </>
  );
}
