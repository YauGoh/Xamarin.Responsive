using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Responsive
{
    public class OrderSpecification
    {
        public OrderSpecification(
            int? xs = null,
            int? sm = null,
            int? md = null,
            int? lg = null,
            int? xl = null)
        {
            Xs = xs;
            Sm = sm;
            Md = md;
            Lg = lg;
            Xl = xl;
        }

        public int? Xs { get; }
        public int? Sm { get; }
        public int? Md { get; }
        public int? Lg { get; }
        public int? Xl { get; }

        internal OrderSpecification WithXs(int? xs) => new OrderSpecification(xs, Sm, Md, Lg, Xl);

        internal OrderSpecification WithSm(int? sm) => new OrderSpecification(Xs, sm, Md, Lg, Xl);

        internal OrderSpecification WithMd(int? md) => new OrderSpecification(Xs, Sm, md, Lg, Xl);

        internal OrderSpecification WithLg(int? lg) => new OrderSpecification(Xs, Sm, Md, lg, Xl);

        internal OrderSpecification WithXl(int? xl) => new OrderSpecification(Xs, Sm, Md, Lg, xl);
    }
}
