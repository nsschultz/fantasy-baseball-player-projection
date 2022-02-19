using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Models.UnitTests
{
    public class ProjectionPitchingStatsTest
    {
        [Fact] public void ProjectedPitchingStatsTest()
        {
            var player = new ProjectionPitchingStats();
            AssertStats(player.ProjectedPitchingStats, 0, 0, 0);
            player.ProjectedPitchingStats.InningsPitched = 100;
            player.ProjectedPitchingStats.EarnedRuns = 50;
            AssertStats(player.ProjectedPitchingStats, 100, 50, 0);
            player.ProjectedPitchingStats = new PitchingStats{ StrikeOuts = 25 };
            AssertStats(player.ProjectedPitchingStats, 0, 0, 25);
            player.ProjectedPitchingStats = null;
            AssertStats(player.ProjectedPitchingStats, 0, 0, 25);
        }

        [Fact] public void YearToDatePitchingStatsTest()
        {
            var player = new ProjectionPitchingStats();
            AssertStats(player.YearToDatePitchingStats, 0, 0, 0);
            player.YearToDatePitchingStats.InningsPitched = 100;
            player.YearToDatePitchingStats.EarnedRuns = 50;
            AssertStats(player.YearToDatePitchingStats, 100, 50, 0);
            player.YearToDatePitchingStats = new PitchingStats{ StrikeOuts = 25 };
            AssertStats(player.YearToDatePitchingStats, 0, 0, 25);
            player.YearToDatePitchingStats = null;
            AssertStats(player.YearToDatePitchingStats, 0, 0, 25);
        }

        private static void AssertStats(PitchingStats PitchingStats, int ips, int ers, int ks)
        {
            Assert.Equal(ips, PitchingStats.InningsPitched);
            Assert.Equal(ers, PitchingStats.EarnedRuns);
            Assert.Equal(ks, PitchingStats.StrikeOuts);
        }
    }
}