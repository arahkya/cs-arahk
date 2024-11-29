using Arahk.Domain.Core.Common.ValueObjects;
using Arahk.Domain.Core.Entity;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Domain.Identity.Entities;

public class UserEntity(Guid id, string username, string password, string email) : BaseEntity<Guid>(id)
{
    public UsernameValueObject Username { get; private init; } = new (username ?? throw new ArgumentNullException(nameof(username)));
    public PasswordValueObject HashedPassword { get; private set; } = new(password ?? throw new ArgumentNullException(nameof(password)));
    public EmailValueObject Email { get; private init; } = new (email ?? throw new ArgumentNullException(nameof(email)));

    public override bool IsValid()
    {
        return Username.IsValid() && HashedPassword.IsValid() && Email.IsValid();
    }
}