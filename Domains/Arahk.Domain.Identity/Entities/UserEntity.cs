using Arahk.Domain.Core.Common.ValueObjects;
using Arahk.Domain.Core.Entity;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Domain.Identity.Entities;

public class UserEntity(Guid id, UsernameValueObject username, PasswordValueObject hashedPassword, EmailValueObject email) : BaseEntity<Guid>(id)
{
    public UsernameValueObject Username { get; internal init; } = username ?? throw new ArgumentNullException(nameof(username));
    public PasswordValueObject HashedPassword { get; internal set; } = hashedPassword ?? throw new ArgumentNullException(nameof(hashedPassword));
    public EmailValueObject Email { get; internal init; } = email ?? throw new ArgumentNullException(nameof(email));
    
    public override bool IsValid()
    {
        return Username.IsValid() && HashedPassword.IsValid() && Email.IsValid();
    }

    public void ChangePassword(string password)
    {
        HashedPassword = new PasswordValueObject(password);
    }
}