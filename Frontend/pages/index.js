import Image from "next/image";

export default function Home() {
  return (
    <>
      <h1>หน้าแรก</h1>
      <div style={{ display: "flex", justifyContent: "center" }}>
        <Image src="/184290_n.jpg" alt="Logo" width={500} height={500} className="image" />
      </div>
    </>
  );
}
