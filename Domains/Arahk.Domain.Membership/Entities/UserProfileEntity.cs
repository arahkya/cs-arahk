using Arahk.Domain.Core.Entity;
using Arahk.Domain.Membership.ValueObjects;

namespace Arahk.Domain.Membership.Entities;

public class UserProfileEntity(Guid id,
    Guid userIdentityId,
    SalutionValueObject? salutation,
    PersonNameValueObject firstname,
    PersonNameValueObject lastname,
    GenderValueObject gender,
    MobilePhoneValueObject primaryMobilePhone) 
    : BaseEntity<Guid>(id)
{
    public SalutionValueObject? Salutation { get; internal set; } = salutation;
    public PersonNameValueObject Firstname { get; internal set; } = firstname;
    public PersonNameValueObject Lastname { get; internal set; } = lastname;
    public GenderValueObject Gender { get; internal set; } = gender;
    public MobilePhoneValueObject PrimaryMobilePhone { get; internal set; } = primaryMobilePhone;
    public Guid UserIdentityId { get; init; } = userIdentityId;
    
    public override bool IsValid()
    {
        return (Salutation?.IsValid() ?? false) &&
               Firstname.IsValid() &&
               Lastname.IsValid() &&
               Gender.IsValid() &&
               PrimaryMobilePhone.IsValid() &&
               Guid.Empty != UserIdentityId;
    }
}