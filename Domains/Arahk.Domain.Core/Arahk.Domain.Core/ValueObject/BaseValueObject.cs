namespace Arahk.Domain.Core.ValueObject;

public abstract class BaseValueObject<TValue>(TValue value)
{
    protected TValue value = value;

    public TValue Value
    {
        get => value;
        internal set => this.value = value;
    }
    
    public abstract bool IsValid();
}