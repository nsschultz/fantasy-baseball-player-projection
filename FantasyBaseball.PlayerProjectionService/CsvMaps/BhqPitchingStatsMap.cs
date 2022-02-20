using CsvHelper;
using CsvHelper.Configuration;
using FantasyBaseball.PlayerProjectionService.Converters;
using FantasyBaseball.PlayerProjectionService.Models;
using FantasyBaseball.Common.Enums;

namespace FantasyBaseball.PlayerProjectionService.CsvMaps
{
    /// <summary>Mapper for BHQ's pitching file.</summary>
    public class BhqPitchingStatsMap : ClassMap<ProjectionPitchingStats>
    {
        /// <summary>Creates a new instance of the mapper.</summary>
        public BhqPitchingStatsMap()
        {
            Map(m => m.BhqId).Name("PLAYERPLAYERID");
            Map(m => m.Type).Name("PLAYERPLAYERID").ConvertUsing((IReaderRow value) => PlayerType.P);
            Map(m => m.Status).Name("PLAYERPLAYERID").ConvertUsing((IReaderRow value) => PlayerStatus.NE);
            Map(m => m.LastName).Name("PLAYERLASTNAME");
            Map(m => m.FirstName).Name("PLAYERFIRSTNAME");
            Map(m => m.Age).Name("PLAYERAGE");
            Map(m => m.Positions).Name("PLAYERPOS").ConvertUsing((IReaderRow value) => "P");
            Map(m => m.Team).Name("PLAYERTM").TypeConverter<TeamConverter>();
            Map(m => m.Reliability).Name("PLAYERMM CODE").TypeConverter<ReliabilityConverter>();
            Map(m => m.MayberryMethod).Name("PLAYERMM");
            Map(m => m.YearToDatePitchingStats.Wins).Name("YTDW");
            Map(m => m.YearToDatePitchingStats.Losses).Name("YTDL");
            Map(m => m.YearToDatePitchingStats.QualityStarts).Name("YTDQS");
            Map(m => m.YearToDatePitchingStats.Saves).Name("YTDSV");
            Map(m => m.YearToDatePitchingStats.BlownSaves).Name("YTDBS");
            Map(m => m.YearToDatePitchingStats.Holds).Name("YTDHLD");
            Map(m => m.YearToDatePitchingStats.InningsPitched).Name("YTDIP");
            Map(m => m.YearToDatePitchingStats.HitsAllowed).Name("YTDH");
            Map(m => m.YearToDatePitchingStats.EarnedRuns).Name("YTDER");
            Map(m => m.YearToDatePitchingStats.HomeRunsAllowed).Name("YTDHR");
            Map(m => m.YearToDatePitchingStats.BaseOnBallsAllowed).Name("YTDBB");
            Map(m => m.YearToDatePitchingStats.StrikeOuts).Name("YTDK");
            Map(m => m.YearToDatePitchingStats.FlyBallRate).Name("YTDF%").TypeConverter<RateConverter>();
            Map(m => m.YearToDatePitchingStats.GroundBallRate).Name("YTDG%").TypeConverter<RateConverter>();
            Map(m => m.ProjectedPitchingStats.Wins).Name("PROJW");
            Map(m => m.ProjectedPitchingStats.Losses).Name("PROJL");
            Map(m => m.ProjectedPitchingStats.QualityStarts).Name("PROJQS");
            Map(m => m.ProjectedPitchingStats.Saves).Name("PROJSV");
            Map(m => m.ProjectedPitchingStats.BlownSaves).Name("PROJBSV");
            Map(m => m.ProjectedPitchingStats.Holds).Name("PROJHLD");
            Map(m => m.ProjectedPitchingStats.InningsPitched).Name("PROJIP");
            Map(m => m.ProjectedPitchingStats.HitsAllowed).Name("PROJH");
            Map(m => m.ProjectedPitchingStats.EarnedRuns).Name("PROJER");
            Map(m => m.ProjectedPitchingStats.HomeRunsAllowed).Name("PROJHR");
            Map(m => m.ProjectedPitchingStats.BaseOnBallsAllowed).Name("PROJBB");
            Map(m => m.ProjectedPitchingStats.StrikeOuts).Name("PROJK");
            Map(m => m.ProjectedPitchingStats.FlyBallRate).Name("PROJF%").TypeConverter<RateConverter>();
            Map(m => m.ProjectedPitchingStats.GroundBallRate).Name("PROJG%").TypeConverter<RateConverter>();
        }
    }
}