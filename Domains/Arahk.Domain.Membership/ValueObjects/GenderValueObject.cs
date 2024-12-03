using Arahk.Domain.Core.ValueObject;

namespace Arahk.Domain.Membership.ValueObjects;

public enum Genders { Male, Female }

public class GenderValueObject(Genders value) : BaseValueObject<Genders>(value)
{
    public override bool IsValid()
    {
        return true;
    }
}