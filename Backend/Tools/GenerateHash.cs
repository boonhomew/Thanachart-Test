using BCrypt.Net;

// Generate BCrypt hash for password: lms147@MD
string password = "lms147@MD";
string hash = BCrypt.HashPassword(password);

Console.WriteLine("=".PadRight(60, '='));
Console.WriteLine("BCrypt Password Hash Generator");
Console.WriteLine("=".PadRight(60, '='));
Console.WriteLine($"Password: {password}");
Console.WriteLine($"BCrypt Hash: {hash}");
Console.WriteLine("=".PadRight(60, '='));
