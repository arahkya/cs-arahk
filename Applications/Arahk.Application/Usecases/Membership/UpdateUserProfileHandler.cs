using Arahk.Application.Repository;
using Arahk.Domain.Identity.ValueObjects;
using Arahk.Domain.Membership.Entities;
using Arahk.Domain.Membership.ValueObjects;

namespace Arahk.Application.Usecases.Membership;

public class UpdateUserProfileHandler(IUnitOfWork unitOfWork)
{
    public async Task Handle(UpdateUserProfileRequest request)
    {
        var user = await unitOfWork.UserRepository.GetByUserNameAsync(new UsernameValueObject(request.Username));

        if (user == null) throw new NullReferenceException();
        
        var userProfileExisted = await unitOfWork.UserProfileRepository.GetByUserIdentityIdAsync(user.Id);

        if (userProfileExisted == null)
        {
            var userProfile = new UserProfileEntity(Guid.NewGuid(),
                user.Id,
                new SalutationValueObject(request.Salutation),
                new PersonNameValueObject(request.Firstname),
                new PersonNameValueObject(request.Lastname),
                new GenderValueObject(Enum.Parse<Genders>(request.Gender)),
                new MobilePhoneValueObject(request.PrimaryMobilePhone));
            
            if (!userProfile.IsValid()) throw new InvalidDataException();
            
            await unitOfWork.UserProfileRepository.AddAsync(userProfile);
        }
        else
        {
            userProfileExisted.Firstname = new PersonNameValueObject(request.Firstname);
            userProfileExisted.Lastname = new PersonNameValueObject(request.Lastname);
            userProfileExisted.Salutation = new SalutationValueObject(request.Salutation);
            userProfileExisted.Gender = new GenderValueObject(Enum.Parse<Genders>(request.Gender));
            userProfileExisted.PrimaryMobilePhone = new MobilePhoneValueObject(request.PrimaryMobilePhone);

            if (!userProfileExisted.IsValid()) throw new InvalidDataException();

            await unitOfWork.UserProfileRepository.UpdateAsync(userProfileExisted);
        }

        await unitOfWork.SaveAsync();
    }
}