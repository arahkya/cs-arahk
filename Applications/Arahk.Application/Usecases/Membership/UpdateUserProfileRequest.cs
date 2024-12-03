using Arahk.Domain.Membership.ValueObjects;

namespace Arahk.Application.Usecases.Membership;

public class UpdateUserProfileRequest
{
    public string Username { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string PrimaryMobilePhone { get; set; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
}