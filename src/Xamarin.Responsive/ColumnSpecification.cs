using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.Responsive
{
    [TypeConverter(typeof(ColumnSpecificationTypeConverter))]
    public struct ColumnSpecification
    {
        public int? Xs { get; set; }
        public int? Sm { get; set; }
        public int? Md { get; set; }
        public int? Lg { get; set; }
        public int? Xl { get; set; }

        public ColumnSpecification(int? xs = null, int? sm = null, int? md = null, int? lg = null, int? xl = null)
        {
            Xs = xs;
            Sm = sm;
            Md = md;
            Lg = lg;
            Xl = xl;
        }

        internal ColumnSpecification WithXs(int? xs) => new ColumnSpecification(xs, Sm, Md, Lg, Xl);

        internal ColumnSpecification WithSm(int? sm) => new ColumnSpecification(Xs, sm, Md, Lg, Xl);

        internal ColumnSpecification WithMd(int? md) => new ColumnSpecification(Xs, Sm, md, Lg, Xl);

        internal ColumnSpecification WithLg(int? lg) => new ColumnSpecification(Xs, Sm, Md, lg, Xl);

        internal ColumnSpecification WithXl(int? xl) => new ColumnSpecification(Xs, Sm, Md, Lg, xl);
    }

    public class ColumnSpecificationTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            var specifications = value
                .Split(',')
                .Select(str => str.Trim())
                .Select(str => int.TryParse(str, out var intValue) ? (int?)intValue : null)
                .ToList();

            var specification = new ColumnSpecification
            {
                Xs = specifications.FirstOrDefault(),
                Sm = specifications.Skip(1).FirstOrDefault(),
                Md = specifications.Skip(2).FirstOrDefault(),
                Lg = specifications.Skip(3).FirstOrDefault(),
                Xl = specifications.Skip(4).FirstOrDefault()
            };

            return specification;
        }
    }
}
