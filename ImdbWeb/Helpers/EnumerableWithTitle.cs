using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Helpers
{
    public static class EnumerableWithTitleExtension
    {
        public static EnumerableWithTitle<T> WithTitle<T>(this IEnumerable<T> items, string title)
        {
            return new EnumerableWithTitle<T>(items, title);
        }

        public static string GetTitle<T>(this IEnumerable<T> enumerable)
        {
            var enumerableWithTitle = enumerable as EnumerableWithTitle<T>;
            return enumerableWithTitle?.Title;
        }
    }

    public class EnumerableWithTitle<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        public EnumerableWithTitle(IEnumerable<T> enumerable, string title)
        {
            _enumerable = enumerable;
            Title = title;
        }

        public string Title { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }
    }
}

