using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyBaseball.PlayerProjectionService.FileReaders;
using FantasyBaseball.PlayerProjectionService.Models;
using FantasyBaseball.PlayerProjectionService.Services;
using FantasyBaseball.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FantasyBaseball.PlayerProjectionService.Controllers.V1
{
    /// <summary>Endpoint for retrieving BHQ player stats data.</summary>
    [Route("api/v1/bhq-stats")] [ApiController] public class BhqStatsController : ControllerBase
    {
        private readonly ICsvFileReaderService _fileReaderService;
        private readonly ICsvFileUploaderService _fileUploadService;
        private readonly IConfiguration _configuration;

        /// <summary>Creates a new instance of the controller.</summary>
        /// <param name="fileReaderService">The service for reading the CSV file.</param>
        /// <param name="fileUploadService">The service for uploading a CSV file.</param>
        /// <param name="configuration">The configuration for the application.</param>
        public BhqStatsController(ICsvFileReaderService fileReaderService, ICsvFileUploaderService fileUploadService, IConfiguration configuration)  
        {
            _fileReaderService = fileReaderService;
            _fileUploadService = fileUploadService;
            _configuration = configuration;
        }

        /// <summary>Gets all of the batters from the BHQ stats source.</summary>
        /// <returns>All of the batters from the BHQ stats source.</returns>
        [HttpGet("batters")] public async Task<IEnumerable<BhqBattingStats>> GetBatters() =>
            await _fileReaderService.ReadCsvData<BhqBattingStats>(new FileReader(_configuration.GetValue<string>("CsvFiles:BatterFile")));

        /// <summary>Gets all of the pitchers from the BHQ stats source.</summary>
        /// <returns>All of the pitchers from the BHQ stats source.</returns>
        [HttpGet("pitchers")] public async Task<IEnumerable<BhqPitchingStats>> GetPitchers() =>
            await _fileReaderService.ReadCsvData<BhqPitchingStats>(new FileReader(_configuration.GetValue<string>("CsvFiles:PitcherFile")));

        /// <summary>Overwrites the underlying batter.csv file.</summary>
        [HttpPost("batters/upload")] public async Task UploadBatterFile() => await UploadFile<BhqBattingStats>("CsvFiles:BatterFile");
        
        /// <summary>Overwrites the underlying pitcher.csv file.</summary>
        [HttpPost("pitchers/upload")] public async Task UploadPitcherFile() => await UploadFile<BhqPitchingStats>("CsvFiles:PitcherFile");

        private async Task UploadFile<T>(string filePath)
        {
            var fileReader = new FormFileReader(Request);
            try
            {
                await _fileReaderService.ReadCsvData<T>(fileReader);
            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid file type");
            }
            await _fileUploadService.UploadFile(fileReader, _configuration.GetValue<string>(filePath));
        }
    }
}