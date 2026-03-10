Console.WriteLine("============================================================");
Console.WriteLine("           BCrypt Password Hash Generator");
Console.WriteLine("============================================================");
Console.WriteLine();

// Generate hash for admin password: lms147@MD
string adminPassword = "lms147@MD";
string adminHash = BCrypt.Net.BCrypt.HashPassword(adminPassword);

Console.WriteLine($"Password: {adminPassword}");
Console.WriteLine($"BCrypt Hash: {adminHash}");
Console.WriteLine();

// Verify the hash works
bool isValid = BCrypt.Net.BCrypt.Verify(adminPassword, adminHash);
Console.WriteLine($"Verification: {(isValid ? "✓ SUCCESS" : "✗ FAILED")}");
Console.WriteLine();
Console.WriteLine("============================================================");
Console.WriteLine("Add this to your seed_data.sql:");
Console.WriteLine("============================================================");
Console.WriteLine();
Console.WriteLine($"INSERT INTO users (id, username, password_hash, email, full_name, created_at, created_by) VALUES");
Console.WriteLine($"('{Guid.NewGuid()}', 'admin', '{adminHash}', 'admin@thanachart.com', 'Administrator', NOW(), 'System');");
Console.WriteLine();
