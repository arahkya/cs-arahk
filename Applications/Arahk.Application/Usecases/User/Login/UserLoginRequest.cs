namespace Arahk.Application.Usecases.User.Login;

public class UserLoginRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}