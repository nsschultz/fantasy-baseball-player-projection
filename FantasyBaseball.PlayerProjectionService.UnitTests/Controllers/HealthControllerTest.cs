using FantasyBaseball.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Controllers.UnitTests
{
    public class HealthControllerTest
    {
        [Fact] public void MissingBatter() 
        {
            var batterSection = new Mock<IConfigurationSection>();
            batterSection.Setup(o => o.Value).Returns("batters.csv");
            var pitcherSection = new Mock<IConfigurationSection>();
            pitcherSection.Setup(o => o.Value).Returns("pitchers.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:BatterFile")).Returns(batterSection.Object);
            config.Setup(o => o.GetSection("CsvFiles:PitcherFile")).Returns(pitcherSection.Object);
            var fileInfoGood = new Mock<IFileInfo>();
            fileInfoGood.Setup(o => o.Exists).Returns(true);
            var fileInfoBad = new Mock<IFileInfo>();
            fileInfoBad.Setup(o => o.Exists).Returns(false);
            var provider = new Mock<IFileProvider>();
            provider.Setup(o => o.GetFileInfo("batters.csv")).Returns(fileInfoBad.Object);
            provider.Setup(o => o.GetFileInfo("pitchers.csv")).Returns(fileInfoGood.Object);
            var e = Assert.Throws<CsvFileException>(() => new HealthController(config.Object, provider.Object).GetHealth());
            Assert.Equal("Batter File Doesn't Exist", e.Message);
        }

        [Fact] public void MissingPitcher() 
        {
            var batterSection = new Mock<IConfigurationSection>();
            batterSection.Setup(o => o.Value).Returns("batters.csv");
            var pitcherSection = new Mock<IConfigurationSection>();
            pitcherSection.Setup(o => o.Value).Returns("pitchers.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:BatterFile")).Returns(batterSection.Object);
            config.Setup(o => o.GetSection("CsvFiles:PitcherFile")).Returns(pitcherSection.Object);
            var fileInfoGood = new Mock<IFileInfo>();
            fileInfoGood.Setup(o => o.Exists).Returns(true);
            var fileInfoBad = new Mock<IFileInfo>();
            fileInfoBad.Setup(o => o.Exists).Returns(false);
            var provider = new Mock<IFileProvider>();
            provider.Setup(o => o.GetFileInfo("batters.csv")).Returns(fileInfoGood.Object);
            provider.Setup(o => o.GetFileInfo("pitchers.csv")).Returns(fileInfoBad.Object);
            var e = Assert.Throws<CsvFileException>(() => new HealthController(config.Object, provider.Object).GetHealth());
            Assert.Equal("Pitcher File Doesn't Exist", e.Message);
        }

        [Fact] public void ValidFiles() 
        {
            var batterSection = new Mock<IConfigurationSection>();
            batterSection.Setup(o => o.Value).Returns("batters.csv");
            var pitcherSection = new Mock<IConfigurationSection>();
            pitcherSection.Setup(o => o.Value).Returns("pitchers.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:BatterFile")).Returns(batterSection.Object);
            config.Setup(o => o.GetSection("CsvFiles:PitcherFile")).Returns(pitcherSection.Object);
            var fileInfo = new Mock<IFileInfo>();
            fileInfo.Setup(o => o.Exists).Returns(true);
            var provider = new Mock<IFileProvider>();
            provider.Setup(o => o.GetFileInfo("batters.csv")).Returns(fileInfo.Object).Verifiable();
            provider.Setup(o => o.GetFileInfo("pitchers.csv")).Returns(fileInfo.Object).Verifiable();
            new HealthController(config.Object, provider.Object).GetHealth();
            provider.VerifyAll();
        }
    }
}