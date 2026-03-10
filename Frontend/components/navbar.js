import Link from "next/link";
import Image from "next/image";
export default function Navbar() {
  return (
    <nav>
      <div>
        <Link href="/">
          <Image className="logo" src="/36_n.jpg" alt="logo" width={100} height={100} />
        </Link>
      </div>
      <ul>
        <li>
          <Link href="/">หน้าแรก</Link>
        </li>
        <li>
          <Link href="/cart">ตะกร้าสินค้า</Link>
        </li>
        <li>
          <Link href="/products/">สินค้า</Link>
        </li>
        <li>
          <Link href="/contact">ติดต่อเรา</Link>
        </li>
      </ul>
    </nav>
  );
}
