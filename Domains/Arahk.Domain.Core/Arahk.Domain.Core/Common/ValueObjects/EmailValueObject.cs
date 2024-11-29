using System.Net.Mail;
using Arahk.Domain.Core.ValueObject;

namespace Arahk.Domain.Core.Common.ValueObjects;

public class EmailValueObject(string value) : BaseValueObject<string>(value ?? throw new ArgumentNullException(nameof(value)))
{
    public override bool IsValid()
    {
        if(string.IsNullOrWhiteSpace(value)) return false;
        
        try
        {
            var mailAddress = new MailAddress(value);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}