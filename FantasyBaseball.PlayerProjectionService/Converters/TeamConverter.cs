using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FantasyBaseball.PlayerProjectionService.Converters
{
    /// <summary>Converts the player's team to one of the standard values.</summary>
    public class TeamConverter : DefaultTypeConverter
    {
        private static readonly Dictionary<string, string> BhqTeamDictionary = new Dictionary<string, string> 
        {
            { "ARI", "ARZ" }, { "CHW", "CWS" }, { "LA" , "LAD" }, { "TAM", "TB"  }
        };
        private static readonly HashSet<string> StandardTeams = new HashSet<string>
        {
            "BAL", "BOS", "NYY", "TB" , "TOR",
            "CWS", "CLE", "DET", "KC" , "MIN",
            "HOU", "LAA", "OAK", "SEA", "TEX",
            "ATL", "MIA", "NYM", "PHI", "WAS",
            "CHC", "CIN", "MIL", "PIT", "STL",
            "ARZ", "COL", "LAD", "SD" , "SF"
        };

        /// <summary>Converts the object to a string.</summary>
        /// <param name="text">The string to convert to an object.</param>
        /// <param name="row">The <see cref="IWriterRow"/> for the current record.</param>
        /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being written.</param>
        /// <returns>The string representation of the object.</returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) => FindTeam(text);

        /// <summary>Converts the string to an object.</summary>
        /// <param name="value">The object to convert to a string.</param>
        /// <param name="row">The <see cref="IReaderRow"/> for the current record.</param>
        /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
        /// <returns>The object created from the string.</returns>
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) => value == null ? "" : FindTeam(value.ToString());

        private static string FindInBhqTeamDictionary(string team) => BhqTeamDictionary.ContainsKey(team) ? BhqTeamDictionary[team] : "";

        private static string FindTeam(string text) 
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            var team = text.ToUpper();
            return StandardTeams.Contains(team) ? team : FindInBhqTeamDictionary(team);
        }
    }
}