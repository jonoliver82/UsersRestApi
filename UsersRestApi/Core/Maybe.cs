using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Core
{
    public class Maybe<T>
    {
        private readonly bool _hasItem;
        private readonly T _item;

        public Maybe()
        {
            _hasItem = false;
        }

        public Maybe(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _hasItem = true;
            _item = item;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Maybe<T> other))
            {
                return false;
            }

            return Equals(_hasItem, other._hasItem) && Equals(_item, other._item);
        }

        public override int GetHashCode()
        {
            return _hasItem ? _item.GetHashCode() : 0;
        }

        public TResult Select<TResult>(Func<TResult> empty, Func<T, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return _hasItem ? func(_item) : empty();
        }
    }
}
