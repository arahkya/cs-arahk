using System.Text.RegularExpressions;
using Arahk.Domain.Core.ValueObject;

namespace Arahk.Domain.Membership.ValueObjects;

public partial class PersonNameValueObject(string value) : BaseValueObject<string>(value)
{
    public override bool IsValid()
    {
        var isValid = ValidationRegexPattern().IsMatch(base.Value);

        return isValid;
    }

    [GeneratedRegex(@"^[a-zA-Z]{2,20}$")]
    private static partial Regex ValidationRegexPattern();
}