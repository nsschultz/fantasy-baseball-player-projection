using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FantasyBaseball.PlayerProjectionService.Converters
{
    /// <summary>Converts the Mayberry Method Code to a reliability percentage.</summary>
    public class ReliabilityConverter : DefaultTypeConverter
    {
        private static Dictionary<char, double> ReliabilityDictionary = new Dictionary<char, double> 
        {
            { 'A', 1.0 }, { 'B', 0.8 }, { 'C', 0.6 }, { 'D', 0.4 }, { 'F', 0.2 }
        };

        /// <summary>Converts the object to a string.</summary>
        /// <param name="text">The string to convert to an object.</param>
        /// <param name="row">The <see cref="IWriterRow"/> for the current record.</param>
        /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being written.</param>
        /// <returns>The string representation of the object.</returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) =>
            text != null && text.Length == 8 ? CalculateReliability(text.Substring(5)) : 0;

        /// <summary>Converts the string to an object.</summary>
        /// <param name="value">The object to convert to a string.</param>
        /// <param name="row">The <see cref="IReaderRow"/> for the current record.</param>
        /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
        /// <returns>The object created from the string.</returns>
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) => value?.ToString() ?? "0";

        private static double CalculateReliability(string mmCode) => 
            mmCode.ToUpper().Select(c => ReliabilityDictionary.ContainsKey(c) ? ReliabilityDictionary[c] : 0).Sum() / 3;
    }
}