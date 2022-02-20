using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FantasyBaseball.PlayerProjectionService.Converters
{
    /// <summary>Converts the rate to a percentage.</summary>
    public class RateConverter : DefaultTypeConverter
    {
        /// <summary>Converts the object to a string.</summary>
        /// <param name="text">The string to convert to an object.</param>
        /// <param name="row">The <see cref="IWriterRow"/> for the current record.</param>
        /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being written.</param>
        /// <returns>The string representation of the object.</returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) => ConvertRate(text);

        /// <summary>Converts the string to an object.</summary>
        /// <param name="value">The object to convert to a string.</param>
        /// <param name="row">The <see cref="IReaderRow"/> for the current record.</param>
        /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
        /// <returns>The object created from the string.</returns>
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) => value?.ToString() ?? "0";

        private static double ConvertRate(double value) => value < 1 ? value : value / 100;

        private static double ConvertRate(string value) => string.IsNullOrEmpty(value) ? 0 : ConvertRate(double.Parse(value));
    }
}