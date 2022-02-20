using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyBaseball.PlayerProjectionService.FileReaders;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Services.UnitTets
{
    public class CsvFileUploaderServiceTest
    {
        private static readonly List<string> PITCHER_RESULTS = new List<string>
        {
            "Player,Player,Player,Player,Player,Player,Player,Player,Player,Player,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,YTD,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ,PROJ",
            "PlayerID,MLBAM ID,Lastname,Firstname,Age,Th,Tm,MM Code,MM,DL,W,L,G,QS,Sv,BS,Hld,IP,H,ER,HR,BB,K,ERA,WHIP,12$,15$,BFG,K9,K%,BB9,BB%,Cmd,K-BB%,HR9,OOB,xERA,G%,L%,F%,Sv%,REff%,H%,S%,XE+/-,EP,DOM%,DIS%,RAR,BPV,W,L,G,QS,Sv,BSv,Hld,IP,H,ER,HR,BB,K,ERA,WHIP,12$,15$,BFG,K9,K%,BB9,BB%,Cmd,K-BB%,HR9,OOB,xERA,G%,L%,F%,H%,S%,RAR,BPV",
            "1234,0,Last,First,35,L,BAL,0000 AFF,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.0,0.0,0,0,0,0,0,0.0,0,0,0,0,0,0,0,0,0,0,0,0,0",
            "(Generated Sep 14 2020 8:36 AM) - Baseball HQ is intended for entertainment purposes only. No part of this site may be reproduced or retransmitted without written permission of the publisher. All rights reserved.  Copyright 2020"
        };

        [Fact] public async Task NewFileUploadTest()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(o => o.ReadLines()).Returns(Task.FromResult(PITCHER_RESULTS));
            await new CsvFileUploaderService().UploadFile(fileReader.Object, "data/new-file.csv");
            var results = await new FileReader("data/new-file.csv").ReadLines();
            Assert.Equal(PITCHER_RESULTS, results);
        }

        [Fact] public async Task OverwriteFileUploadTest()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(o => o.ReadLines()).Returns(Task.FromResult(PITCHER_RESULTS));
            await new CsvFileUploaderService().UploadFile(fileReader.Object, "data/test-pitcher.csv");
            var results = await new FileReader("data/test-pitcher.csv").ReadLines();
            Assert.Equal(PITCHER_RESULTS, results);
        }
    }
}