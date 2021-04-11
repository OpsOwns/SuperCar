using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperCar.Shared.Domain.Abstraction
{
    public abstract class ValueObject : IComparable, IComparable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            var value = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(value.GetEqualityComponents());
        }
        public int CompareTo(object obj) => CompareTo((ValueObject)obj);
        public int CompareTo(ValueObject other)
        {
            var thisType = GetType();
            var otherType = other.GetType();
            if (thisType != otherType)
                return string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);
            var components = GetEqualityComponents().ToArray();
            var otherComponents = other.GetEqualityComponents().ToArray();
            return components.Select((value, indexer) => CompareComponents(value, otherComponents[indexer]))
                .FirstOrDefault(comparison => comparison != 0);
        }
        private int CompareComponents(object value1, object value2) =>
            value1 is null && value2 is null
                ? 0
                : value1 is null
                    ? -1
                    : value2 is null
                        ? 1
                        : value1 is IComparable comparable1 && value2 is IComparable comparable2
                            ? comparable1.CompareTo(comparable2)
                            : value1.Equals(value2) ? 0 : -1;
        public override int GetHashCode() => GetEqualityComponents().Aggregate(1, (current, obj) =>
        {
            unchecked
            {
                return current * 23 + (obj?.GetHashCode() ?? 0);
            }
        });
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left?.Equals(right) ?? ReferenceEquals(right, null);
        }
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
        public static bool operator <(ValueObject left, ValueObject right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }
        public static bool operator <=(ValueObject left, ValueObject right)
        {
            return !(left is not null && left.CompareTo(right) > 0);
        }
        public static bool operator >(ValueObject left, ValueObject right)
        {
            return !(left is null || left.CompareTo(right) <= 0);
        }
        public static bool operator >=(ValueObject left, ValueObject right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
