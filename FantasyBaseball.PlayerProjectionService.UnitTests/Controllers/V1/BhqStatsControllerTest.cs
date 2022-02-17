using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyBaseball.PlayerProjectionService.FileReaders;
using FantasyBaseball.PlayerProjectionService.Models;
using FantasyBaseball.PlayerProjectionService.Services;
using FantasyBaseball.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.Controllers.V1.UnitTests
{
    public class BhqStatsControllerTest
    {
        [Fact] public async Task GetBattersTest() 
        {
            var returnData = new List<BhqBattingStats> { new BhqBattingStats() };
            var section = new Mock<IConfigurationSection>();
            section.Setup(o => o.Value).Returns("test.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:BatterFile")).Returns(section.Object);
            var service = new Mock<ICsvFileReaderService>();
            service.Setup(o => o.ReadCsvData<BhqBattingStats>(It.IsAny<FileReader>())).Returns(Task.FromResult(returnData));
            Assert.NotEmpty(await new BhqStatsController(service.Object, null, config.Object).GetBatters());
        }

        [Fact] public async Task GetPitchersTest() 
        {
            var returnData = new List<BhqPitchingStats> { new BhqPitchingStats() };
            var section = new Mock<IConfigurationSection>();
            section.Setup(o => o.Value).Returns("test.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:PitcherFile")).Returns(section.Object);
            var service = new Mock<ICsvFileReaderService>();
            service.Setup(o => o.ReadCsvData<BhqPitchingStats>(It.IsAny<FileReader>())).Returns(Task.FromResult(returnData));
            Assert.NotEmpty(await new BhqStatsController(service.Object, null, config.Object).GetPitchers());
        }

        [Fact] public async Task UploadBattersTest() 
        {
            var returnData = new List<BhqBattingStats> { new BhqBattingStats() };
            var section = new Mock<IConfigurationSection>();
            section.Setup(o => o.Value).Returns("batters.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:BatterFile")).Returns(section.Object);
            var uploadService = new Mock<ICsvFileUploaderService>();
            uploadService.Setup(o => o.UploadFile(It.IsAny<FormFileReader>(), "batters.csv")).Returns(Task.FromResult(0));
            var readService = new Mock<ICsvFileReaderService>();
            readService.Setup(o => o.ReadCsvData<BhqBattingStats>(It.IsAny<FormFileReader>())).Returns(Task.FromResult(returnData));
            var request = new Mock<HttpRequest>();
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Request).Returns(request.Object);
            var controller = new BhqStatsController(readService.Object, uploadService.Object, config.Object);
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext.Object };
            await controller.UploadBatterFile();
            readService.VerifyAll();
            uploadService.VerifyAll();
        }

        [Fact] public async Task UploadBattersExceptionTest()
        {
            var readService = new Mock<ICsvFileReaderService>();
            readService.Setup(o => o.ReadCsvData<BhqBattingStats>(It.IsAny<FormFileReader>())).ThrowsAsync(new Exception("Bad Request"));
            var request = new Mock<HttpRequest>().Object;
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Request).Returns(request);
            var controller = new BhqStatsController(readService.Object, null, null);
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext.Object };
            await Assert.ThrowsAsync<BadRequestException>(() => controller.UploadBatterFile());
            readService.VerifyAll();
        }

        [Fact] public async Task UploadPitchersTest() 
        {
            var returnData = new List<BhqPitchingStats> { new BhqPitchingStats() };
            var section = new Mock<IConfigurationSection>();
            section.Setup(o => o.Value).Returns("pitchers.csv");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("CsvFiles:PitcherFile")).Returns(section.Object);
            var uploadService = new Mock<ICsvFileUploaderService>();
            uploadService.Setup(o => o.UploadFile(It.IsAny<FormFileReader>(), "pitchers.csv")).Returns(Task.FromResult(0));
            var readService = new Mock<ICsvFileReaderService>();
            readService.Setup(o => o.ReadCsvData<BhqPitchingStats>(It.IsAny<FormFileReader>())).Returns(Task.FromResult(returnData));
            var request = new Mock<HttpRequest>().Object;
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Request).Returns(request);
            var controller = new BhqStatsController(readService.Object, uploadService.Object, config.Object);
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext.Object };
            await controller.UploadPitcherFile();
            readService.VerifyAll();
            uploadService.VerifyAll();
        }

        [Fact] public async Task UploadPitchersExceptionTest()
        {
            var readService = new Mock<ICsvFileReaderService>();
            readService.Setup(o => o.ReadCsvData<BhqPitchingStats>(It.IsAny<FormFileReader>())).ThrowsAsync(new Exception("Bad Request"));
            var request = new Mock<HttpRequest>().Object;
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Request).Returns(request);
            var controller = new BhqStatsController(readService.Object, null, null);
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext.Object };
            await Assert.ThrowsAsync<BadRequestException>(() => controller.UploadPitcherFile());
            readService.VerifyAll();
        }
    }
}