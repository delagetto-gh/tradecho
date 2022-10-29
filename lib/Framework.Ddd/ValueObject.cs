using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Ddd;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public override bool Equals(object obj)
    {
        return Equals(obj as ValueObject);
    }

    public bool Equals(ValueObject other)
    {
        if (other == null)
            return false;

        if (GetType() != other.GetType())
            return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Aggregate(default(int), HashCode.Combine);
    }

    public static bool operator ==(ValueObject x, ValueObject y)
    {
         if (x is null)
            return y is null;

        return x.Equals(y);
    }

    public static bool operator !=(ValueObject x, ValueObject y)
    {
        return !(x == y);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();
}
