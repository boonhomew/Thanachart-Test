import "@/styles/globals.css";
import Layout from "@/components/layout";
import AlertProvider from "@/components/Alert";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";

const VERIFY_URL =
  process.env.NEXT_PUBLIC_VERIFY_URL ||
  "http://localhost:5000/api/oauth2/verify-token";

export default function App({ Component, pageProps }) {
  const router = useRouter();
  const [checkingAuth, setCheckingAuth] = useState(true);

  useEffect(() => {
    let mounted = true;
    async function ensureAuth() {
      const pathname = router.pathname || "/";
      // allow public routes
      if (
        pathname.startsWith("/login") ||
        pathname.startsWith("/api") ||
        pathname.startsWith("/_next")
      ) {
        if (mounted) setCheckingAuth(false);
        return;
      }

      // read token from localStorage (client-side only)
      let token = null;
      try {
        token =
          localStorage.getItem("token") || localStorage.getItem("accessToken");
      } catch (e) {
        token = null;
      }

      if (!token) {
        router.replace(`/login?next=${encodeURIComponent(pathname)}`);
        if (mounted) setCheckingAuth(false);
        return;
      }

      try {
        const res = await fetch(VERIFY_URL, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ token }),
        });
        if (!res.ok) throw new Error("invalid");
        // treat any OK response as valid token
        if (mounted) setCheckingAuth(false);
      } catch (err) {
        try {
          localStorage.removeItem("token");
          localStorage.removeItem("accessToken");
        } catch (e) {}
        router.replace(`/login?next=${encodeURIComponent(pathname)}`);
        if (mounted) setCheckingAuth(false);
      }
    }

    ensureAuth();
    return () => {
      mounted = false;
    };
  }, [router]);

  if (checkingAuth) return null;

  return (
    <AlertProvider>
      <Layout>
        <Component {...pageProps} />
      </Layout>
    </AlertProvider>
  );
}
