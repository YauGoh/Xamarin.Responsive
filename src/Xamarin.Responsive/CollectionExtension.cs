using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Responsive
{
    internal static class CollectionExtension
    {
        internal static TCollection AddUnique<TCollection, TItem>(this TCollection collection, TItem item)
            where TCollection : ICollection<TItem>
        {
            if (!collection.Any(a => a.Equals(item)))
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
