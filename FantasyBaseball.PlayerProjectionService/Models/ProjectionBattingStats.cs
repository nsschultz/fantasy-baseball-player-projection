using System.Linq;
using System.Text.Json.Serialization;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;

namespace FantasyBaseball.PlayerProjectionService.Models
{
    /// <summary>A marker object for breaking up the mappers.</summary>
    public class ProjectionBattingStats : BaseballPlayer 
    { 
        /// <summary>Batting Stats (Projected) for a given player.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)] public BattingStats ProjectedBattingStats
        { 
            get { return GetBattingStats(StatsType.PROJ); }
            set { AddBattingStats(value, StatsType.PROJ); }
        }

        /// <summary>Batting Stats (Year to Date) for a given player.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)] public BattingStats YearToDateBattingStats 
        { 
            get { return GetBattingStats(StatsType.YTD); }
            set { AddBattingStats(value, StatsType.YTD); }
         }

        private void AddBattingStats(BattingStats value, StatsType statsType)
        {
            if (value == null) return;
            value.StatsType = statsType;
            var existing = GetBattingStats(statsType);
            if (existing != null) BattingStats.Remove(existing);
            BattingStats.Add(value);
        } 

        private BattingStats GetBattingStats(StatsType statsType) 
        {
            if (!BattingStats.Any(s => s.StatsType == statsType)) BattingStats.Add(new BattingStats { StatsType = statsType });
            return BattingStats.FirstOrDefault(s => s.StatsType == statsType);
        }
    }
}