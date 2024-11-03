namespace FitCoders.Domain.Entities.Base;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    //Private init > property value is initialized as soon and only when its created, enforces immutability.
    protected int Id { get; private init; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    protected BaseEntity(int id)
    {
        Id = id;
    }
    //Operator rules assure inherited objects safety and robustness
    public static bool operator ==(BaseEntity? first, BaseEntity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }
    public static bool operator !=(BaseEntity? first, BaseEntity? second)
    {
        return !(first == second);
    }

    public bool Equals(BaseEntity? other)
    {
        if(other is null) return false;

        if(other.GetType() != GetType()) return false;

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if(obj is null) return false;

        if(obj.GetType() != GetType()) return false;

        if(obj is not BaseEntity entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}
