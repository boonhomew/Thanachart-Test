import Head from "next/head";
import Link from "next/link";
import LoginForm from "@/components/LoginForm";

export default function LoginPage() {
  return (
    <>
      <Head>
        <title>เข้าสู่ระบบ</title>
      </Head>
      <div style={{ padding: 16 }}>
        <h1>เข้าสู่ระบบ</h1>
        <h4>Username : admin</h4>
        <h4>Password : lms147@MD</h4>
        <LoginForm />
      </div>
    </>
  );
}
