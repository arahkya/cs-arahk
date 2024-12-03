using System.Text.RegularExpressions;
using Arahk.Domain.Core.ValueObject;

namespace Arahk.Domain.Membership.ValueObjects;

public partial class MobilePhoneValueObject(string value) : BaseValueObject<string>(value)
{
    public override bool IsValid()
    {
        var isValid = ValidationRegexPattern().IsMatch(base.Value);

        return isValid;
    }

    [GeneratedRegex(@"^(09|08|06)\d{8}$")]
    private static partial Regex ValidationRegexPattern();
}