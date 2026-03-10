namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username);

        Guid? ValidateToken(string token);

    }
}
