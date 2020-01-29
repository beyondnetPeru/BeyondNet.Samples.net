using System;
using System.Collections.Generic;

namespace BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects
{
    public abstract class AbstractComparableValueObject : AbstractValueObject, IComparable
    {
        protected abstract IEnumerable<IComparable> GetComparableComponents();

        protected IComparable AsNonGenericComparable<T>(IComparable<T> comparable)
        {
            return new NonGenericComparable<T>(comparable);
        }

        class NonGenericComparable<T> : IComparable
        {
            public NonGenericComparable(IComparable<T> comparable)
            {
                this._comparable = comparable;
            }

            readonly IComparable<T> _comparable;

            public int CompareTo(object obj)
            {
                if (object.ReferenceEquals(this._comparable, obj)) return 0;
                if (object.ReferenceEquals(null, obj))
                    throw new ArgumentNullException();
                return this._comparable.CompareTo((T)obj);
            }
        }

        protected int CompareTo(AbstractComparableValueObject other)
        {
            using (var thisComponents = GetComparableComponents().GetEnumerator())
            using (var otherComponents = other.GetComparableComponents().GetEnumerator())
            {
                while (true)
                {
                    var x = thisComponents.MoveNext();
                    var y = otherComponents.MoveNext();
                    if (x != y)
                        throw new InvalidOperationException();
                    if (x)
                    {
                        var c = thisComponents.Current.CompareTo(otherComponents.Current);
                        if (c != 0)
                            return c;
                    }
                    else
                    {
                        break;
                    }
                }
                return 0;
            }
        }

        public int CompareTo(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return 0;
            if (object.ReferenceEquals(null, obj)) return 1;

            if (GetType() != obj.GetType())
                throw new InvalidOperationException();

            return CompareTo(obj as AbstractComparableValueObject);
        }
    }

    public abstract class AbstractComparableValueObject<T> : AbstractComparableValueObject, IComparable<T>
        where T : AbstractComparableValueObject<T>
    {
        public int CompareTo(T other)
        {
            return base.CompareTo(other);
        }
    }
}
