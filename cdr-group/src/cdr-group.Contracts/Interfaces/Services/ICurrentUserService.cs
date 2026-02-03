namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Username { get; }
        bool IsAuthenticated { get; }
    }
}
