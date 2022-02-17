using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using FantasyBaseball.PlayerProjectionService.FileReaders;
using FantasyBaseball.PlayerProjectionService.Models;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Services.UnitTets
{
    public class CsvFileReaderServiceTest
    {
        private static readonly List<string> BATTER_RESULTS = new List<string>
        {
            "Player,Player,Player,Player,Player,Player,Player,Player,Player,Player,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,",
            "PlayerID,MLBAM ID,Lastname,Firstname,Age,B,Pos,Tm,MM Code,MM,DL,AB,R,H,2B,3B,HR,RBI,BB,K,SB,CS,AVG,OBP,SLG,OPS,12$,15$,BB%,Ct%,Eye,H%,PX,xPX,hctX,SPD,RSPD,G%,L%,F%,XBA,BA,DOM%,DIS%,RCG,RAR,BPV,AB,R,H,2B,3B,HR,RBI,BB,K,SB,CS,AVG,OBP,SLG,OPS,12$,15$,BB%,Ct%,Eye,H%,PX,SPD,RSPD,G%,L%,F%,XBA,BA,RCG,RAR,BPV",
            "1234,0,Last,First,33,R,30,CHW,4053 AAB,36,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
            "(Generated Sep 14 2020 8:36 AM) - Baseball HQ is intended for entertainment purposes only. No part of this site may be reproduced or retransmitted without written permission of the publisher. All rights reserved.  Copyright 2020"
        };
        
        private static readonly List<string> PITCHER_RESULTS = new List<string>
        {
            "Player,Player,Player,Player,Player,Player,Player,Player,Player,Player,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ",
            "PlayerID,MLBAM ID,Lastname,Firstname,Age,Th,Tm,MM Code,MM,DL,W,L,G,QS,Sv,BS,Hld,IP,H,ER,HR,BB,K,ERA,WHIP,12$,15$,BFG,K9,K%,BB9,BB%,Cmd,K-BB%,HR9,OOB,xERA,G%,L%,F%,Sv%,REff%,H%,S%,XE+/-,EP,DOM%,DIS%,RAR,BPV,W,L,G,QS,Sv,BSv,Hld,IP,H,ER,HR,BB,K,ERA,WHIP,12$,15$,BFG,K9,K%,BB9,BB%,Cmd,K-BB%,HR9,OOB,xERA,G%,L%,F%,H%,S%,RAR,BPV",
            "1234,0,Last,First,35,L,BAL,0000 AFF,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.0,0.0,0,0,0,0,0,0.0,0,0,0,0,0,0,0,0,0,0,0,0,0",
            "(Generated Sep 14 2020 8:36 AM) - Baseball HQ is intended for entertainment purposes only. No part of this site may be reproduced or retransmitted without written permission of the publisher. All rights reserved.  Copyright 2020"
        };

        [Fact] public async Task ReadCsvDataInvalidBatterTest() 
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(o => o.ReadLines()).Returns(Task.FromResult(PITCHER_RESULTS));
            await Assert.ThrowsAsync<HeaderValidationException>(() => new CsvFileReaderService().ReadCsvData<BhqBattingStats>(fileReader.Object));
        }

        [Fact] public async Task ReadCsvDataValidBatterTest() 
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(o => o.ReadLines()).Returns(Task.FromResult(BATTER_RESULTS));
            var results = await new CsvFileReaderService().ReadCsvData<BhqBattingStats>(fileReader.Object);
            Assert.Single(results);
            Assert.Equal(1234, results.First().BhqId);
        }

        [Fact] public async Task ReadCsvDataInvalidPitcherTest() 
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(o => o.ReadLines()).Returns(Task.FromResult(BATTER_RESULTS));
            await Assert.ThrowsAsync<HeaderValidationException>(() => new CsvFileReaderService().ReadCsvData<BhqPitchingStats>(fileReader.Object));
        }

        [Fact] public async Task ReadCsvDataValidPitcherTest() 
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(o => o.ReadLines()).Returns(Task.FromResult(PITCHER_RESULTS));
            var results = await new CsvFileReaderService().ReadCsvData<BhqPitchingStats>(fileReader.Object);
            Assert.Single(results);
            Assert.Equal(1234, results.First().BhqId);
        }
    }
}