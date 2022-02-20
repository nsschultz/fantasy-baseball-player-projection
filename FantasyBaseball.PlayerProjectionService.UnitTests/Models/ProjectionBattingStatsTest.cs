using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Models.UnitTests
{
    public class ProjectionBattingStatsTest
    {
        [Fact] public void ProjectedBattingStatsTest()
        {
            var player = new ProjectionBattingStats();
            AssertStats(player.ProjectedBattingStats, 0, 0, 0);
            player.ProjectedBattingStats.AtBats = 100;
            player.ProjectedBattingStats.BaseOnBalls = 50;
            AssertStats(player.ProjectedBattingStats, 100, 50, 0);
            player.ProjectedBattingStats = new BattingStats{ HomeRuns = 25 };
            AssertStats(player.ProjectedBattingStats, 0, 0, 25);
            player.ProjectedBattingStats = null;
            AssertStats(player.ProjectedBattingStats, 0, 0, 25);
        }

        [Fact] public void YearToDateBattingStatsTest()
        {
            var player = new ProjectionBattingStats();
            AssertStats(player.YearToDateBattingStats, 0, 0, 0);
            player.YearToDateBattingStats.AtBats = 100;
            player.YearToDateBattingStats.BaseOnBalls = 50;
            AssertStats(player.YearToDateBattingStats, 100, 50, 0);
            player.YearToDateBattingStats = new BattingStats{ HomeRuns = 25 };
            AssertStats(player.YearToDateBattingStats, 0, 0, 25);
            player.YearToDateBattingStats = null;
            AssertStats(player.YearToDateBattingStats, 0, 0, 25);
        }

        private static void AssertStats(BattingStats battingStats, int abs, int bbs, int hrs)
        {
            Assert.Equal(abs, battingStats.AtBats);
            Assert.Equal(bbs, battingStats.BaseOnBalls);
            Assert.Equal(hrs, battingStats.HomeRuns);
        }
    }
}