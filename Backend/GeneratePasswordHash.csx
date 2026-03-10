// C# Script to generate BCrypt password hash
// Run with: dotnet script GeneratePasswordHash.csx


using BCrypt.Net;

string password = "lms147@MD";
string hash = BCrypt.HashPassword(password);

Console.WriteLine("Password: " + password);
Console.WriteLine("BCrypt Hash: " + hash);
Console.WriteLine("");
Console.WriteLine("Copy this hash to your seed_data.sql file");
