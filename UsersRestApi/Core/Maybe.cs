// **********************************************************************************
// Filename					- Maybe.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;

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

        /// <summary>
        /// Override to permit explicit action on empty and present.
        /// </summary>
        public TResult Select<TResult>(Func<TResult> empty, Func<T, TResult> present)
        {
            if (present == null)
            {
                throw new ArgumentNullException(nameof(present));
            }

            return _hasItem ? present(_item) : empty();
        }

        /// <summary>
        /// Composable func for fluent usage.
        /// </summary>
        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (_hasItem)
            {
                return new Maybe<TResult>(selector(_item));
            }
            else
            {
                return new Maybe<TResult>();
            }
        }
    }
}
