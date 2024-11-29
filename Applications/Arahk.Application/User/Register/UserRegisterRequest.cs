namespace Arahk.Application.User.Register;

public class UserRegisterRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
}