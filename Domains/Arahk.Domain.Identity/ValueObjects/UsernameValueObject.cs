using System.Text.RegularExpressions;
using Arahk.Domain.Core.Entity;
using Arahk.Domain.Core.ValueObject;

namespace Arahk.Domain.Identity.ValueObjects;

public partial class UsernameValueObject(string value) : BaseValueObject<string>(value ?? throw new ArgumentNullException(nameof(value)))
{
    public override bool IsValid()
    {
        return !string.IsNullOrEmpty(value) && MyRegex().IsMatch(value);
    }

    [GeneratedRegex(@"^(?=.*[a-zA-Z])[a-zA-Z0-9]{6,15}$")]
    private static partial Regex MyRegex();
}