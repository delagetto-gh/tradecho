using System;

namespace Framework.Ddd;

public abstract class Entity<TEntityId> : IEquatable<Entity<TEntityId>> where TEntityId : IEquatable<TEntityId>
{
    protected Entity() { }

    protected Entity(TEntityId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public TEntityId Id { get; private set; }

    public bool Equals(Entity<TEntityId> other)
    {
        if (other == null)
            return false;
            
        if (GetType() != other.GetType())
            return false;

        var thisId = (this.Id as IEquatable<TEntityId>);
        var otherId = (other.Id as IEquatable<TEntityId>);

        return thisId.Equals(otherId);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Entity<TEntityId>);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TEntityId> x, Entity<TEntityId> y)
    {
        if (x is null)
            return y is null;

        return x.Equals(y);
    }

    public static bool operator !=(Entity<TEntityId> x, Entity<TEntityId> y)
    {
        return !(x == y);
    }
}