using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Converters.UnitTets
{
    public class TeamConverterTest
    {
        [Theory]
        [InlineData("ARI", "ARZ")]
        [InlineData("ARZ", "ARZ")]
        [InlineData("ATL", "ATL")]
        [InlineData("BAL", "BAL")]
        [InlineData("BOS", "BOS")]
        [InlineData("CHC", "CHC")]
        [InlineData("CHW", "CWS")]
        [InlineData("CWS", "CWS")]
        [InlineData("CIN", "CIN")]
        [InlineData("CLE", "CLE")]
        [InlineData("COL", "COL")]
        [InlineData("DET", "DET")]
        [InlineData("FAA", ""   )]
        [InlineData("FAN", ""   )]
        [InlineData("HOU", "HOU")]
        [InlineData("KC" , "KC" )]
        [InlineData("LA" , "LAD")]
        [InlineData("LAD", "LAD")]
        [InlineData("LAA", "LAA")]
        [InlineData("MIA", "MIA")]
        [InlineData("MIL", "MIL")]
        [InlineData("MIN", "MIN")]
        [InlineData("NYM", "NYM")]
        [InlineData("NYY", "NYY")]
        [InlineData("OAK", "OAK")]
        [InlineData("PHI", "PHI")]
        [InlineData("PIT", "PIT")]
        [InlineData("SD" , "SD" )]
        [InlineData("SEA", "SEA")]
        [InlineData("SF" , "SF" )]
        [InlineData("STL", "STL")]
        [InlineData("TAM", "TB" )]
        [InlineData("TB" , "TB" )]
        [InlineData("TEX", "TEX")]
        [InlineData("TOR", "TOR")]
        [InlineData("WAS", "WAS")]
        [InlineData("mIl", "MIL")]
        [InlineData(""   , ""   )]
        [InlineData(null , ""   )]
        public void ConvertFromStringTest(string value, string expected) => 
            Assert.Equal(expected, new TeamConverter().ConvertFromString(value, null, null));

        [Theory]
        [InlineData("MIL", "MIL")]
        [InlineData("mil", "MIL")]
        [InlineData("TAM", "TB" )]
        [InlineData(null , ""   )]
        public void ConvertToStringTest(object value, string expected) => 
            Assert.Equal(expected, new TeamConverter().ConvertToString(value, null, null));
    }
}