using Arahk.Domain.Core.ValueObject;

namespace Arahk.Domain.Membership.ValueObjects;

public class SalutationValueObject(string value) : BaseValueObject<string>(value)
{
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}