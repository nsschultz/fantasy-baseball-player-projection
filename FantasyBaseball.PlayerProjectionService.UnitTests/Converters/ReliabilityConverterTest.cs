using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Converters.UnitTets
{
    public class ReliabilityConverterTest
    {
        [Theory]
        [InlineData("3500 ACF", 0.6)]
        [InlineData("3500 aCf", 0.6)]
        [InlineData("3500 ZZZ", 0.0)]
        [InlineData("3500"    , 0.0)]
        [InlineData(""        , 0.0)]
        [InlineData(null      , 0.0)]
        public void ConvertFromStringTest(string value, double expected) => 
            Assert.Equal(expected, new ReliabilityConverter().ConvertFromString(value, null, null));

        [Theory]
        [InlineData(0.2  , "0.2")]
        [InlineData("0.2", "0.2")]
        [InlineData(null , "0"  )]
        public void ConvertToStringTest(object value, string expected) => 
            Assert.Equal(expected, new ReliabilityConverter().ConvertToString(value, null, null));
    }
}