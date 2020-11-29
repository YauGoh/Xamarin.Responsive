using System.Linq;
using Xamarin.Forms;

namespace Xamarin.Responsive
{
    [TypeConverter(typeof(VisibleSpecificationTypeConverter))]
    public class VisibleSpecification
    {
        public VisibleSpecification() { }

        public bool Xs { get; set; } = true;
        public bool Sm { get; set; } = true;
        public bool Md { get; set; } = true;
        public bool Lg { get; set; } = true;
        public bool Xl { get; set; } = true;
    }

    public class VisibleSpecificationTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            var specifications = value
                .Split(',')
                .Select(str => str.Trim())
                .Select(str => bool.TryParse(str, out var boolValue) ? boolValue : true)
                .ToList();

            var specification = new VisibleSpecification
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
