using System;
using System.Linq;
using Xamarin.Forms;

namespace Xamarin.Responsive
{
    public enum RowHeightUnit
    {
        Auto,
        Fill,
        Pixels
    }

    [TypeConverter(typeof(ResponsiveRowHieghtSpecificationTypeConverter))]
    public class ResponsiveRowHieghtSpecification
    {
        public ResponsiveRowHieghtSpecification(
            RowHeightSpecification xs = null,
            RowHeightSpecification sm = null,
            RowHeightSpecification md = null,
            RowHeightSpecification lg = null,
            RowHeightSpecification xl = null)
        {
            Xs = xs;
            Sm = sm;
            Md = md;
            Lg = lg;
            Xl = xl;
        }

        public RowHeightSpecification Xs { get; }
        public RowHeightSpecification Sm { get; }
        public RowHeightSpecification Md { get; }
        public RowHeightSpecification Lg { get; }
        public RowHeightSpecification Xl { get; }
    }

    [TypeConverter(typeof(RowHeightSpecificationTypeConverter))]
    public class RowHeightSpecification
    {
        public RowHeightSpecification(double value, RowHeightUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public double Value { get; }

        public RowHeightUnit Unit { get; }
    }

    public class ResponsiveRowHieghtSpecificationTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(Type sourceType) => sourceType == typeof(string);

        public override object ConvertFromInvariantString(string value)
        {
            var converter = new RowHeightSpecificationTypeConverter();

            var specifications = value
                .Split(',')
                .Select(s => s.Trim())
                .Select(s => (RowHeightSpecification)converter.ConvertFromInvariantString(s));

            return new ResponsiveRowHieghtSpecification
            (
                specifications.FirstOrDefault(),
                specifications.Skip(1).FirstOrDefault(),
                specifications.Skip(2).FirstOrDefault(),
                specifications.Skip(3).FirstOrDefault(),
                specifications.Skip(4).FirstOrDefault()
            );
        }
    }

    public class RowHeightSpecificationTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(Type sourceType) => sourceType == typeof(string);

        public override object ConvertFromInvariantString(string value)
        {
            if (value?.ToLower() == "auto") return new RowHeightSpecification(1, RowHeightUnit.Auto);

            if (value?.ToLower() == "fill") return new RowHeightSpecification(1, RowHeightUnit.Fill);

            var components = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (components.Length == 2 && double.TryParse(components[0], out var length) && components[1].ToLower() == "px")
            {
                return new RowHeightSpecification(length, RowHeightUnit.Pixels);
            }

            throw new ArgumentException($"{value} is not a valid height value");
        }
    }
}
