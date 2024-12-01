using System.Text.RegularExpressions;
using Arahk.Domain.Core.ValueObject;
using Arahk.Domain.Identity.Services;

namespace Arahk.Domain.Identity.ValueObjects;

public partial class PasswordValueObject(string value) : BaseValueObject<string>(value)
{
    private readonly string _password = value;
    public override bool IsValid()
    {
        return !string.IsNullOrEmpty(_password) && MyRegex().IsMatch(_password);
    }

    [GeneratedRegex(@"^(?=.*[a-zA-Z])(?=.*\W)[a-zA-Z0-9\W]{6,15}$")]
    private static partial Regex MyRegex();

    public void HashPassword()
    {
        base.value = PasswordHashService.HashPassword(_password);
    }
}