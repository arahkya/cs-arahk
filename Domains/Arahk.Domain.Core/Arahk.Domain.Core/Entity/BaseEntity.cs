namespace Arahk.Domain.Core.Entity;

public abstract class BaseEntity<TKeyId>(TKeyId id)
{
    public TKeyId Id { get; private init; } = id;
    
    public abstract bool IsValid();
}