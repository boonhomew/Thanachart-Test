import Navbar from "./navbar";
import Footer from "./footer";

export default function Layout({ children }) {
  return (
    <div className="page">
      <Navbar />
      <main className="content">{children}</main>
      <Footer />
    </div>
  );
}
