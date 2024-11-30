namespace Arahk.Application.User.CreateAccessToken;

public class UserCreateAccessTokenRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}