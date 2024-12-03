namespace Arahk.Application.Usecases.User.ChangePassword;

public class UserChangePasswordRequest
{
    public required string Username { get; set; }
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}