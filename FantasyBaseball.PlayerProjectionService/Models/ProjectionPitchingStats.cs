using System.Linq;
using System.Text.Json.Serialization;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;

namespace FantasyBaseball.PlayerProjectionService.Models
{
    /// <summary>A marker object for breaking up the mappers.</summary>
    public class ProjectionPitchingStats : BaseballPlayer
    { 
         /// <summary>Pitching Stats (Projected) for a given player.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)] public PitchingStats ProjectedPitchingStats
        { 
            get { return GetPitchingStats(StatsType.PROJ); }
            set { AddPitchingStats(value, StatsType.PROJ); }
        }

        /// <summary>Pitching Stats (Year to Date) for a given player.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)] public PitchingStats YearToDatePitchingStats 
        { 
            get { return GetPitchingStats(StatsType.YTD); }
            set { AddPitchingStats(value, StatsType.YTD); }
         }

        private void AddPitchingStats(PitchingStats value, StatsType statsType)
        {
            if (value == null) return;
            value.StatsType = statsType;
            var existing = GetPitchingStats(statsType);
            if (existing != null) PitchingStats.Remove(existing);
            PitchingStats.Add(value);
        } 

        private PitchingStats GetPitchingStats(StatsType statsType) 
        {
            if (!PitchingStats.Any(s => s.StatsType == statsType)) PitchingStats.Add(new PitchingStats { StatsType = statsType });
            return PitchingStats.FirstOrDefault(s => s.StatsType == statsType);
        }
    }
}