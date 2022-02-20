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
    /// <summary>Endpoint for retrieving player projection stats data.</summary>
    [Route("api/v1/projection")] [ApiController] public class PlayerProjectionController : ControllerBase
    {
        private readonly ICsvFileReaderService _fileReaderService;
        private readonly ICsvFileUploaderService _fileUploadService;
        private readonly IConfiguration _configuration;

        /// <summary>Creates a new instance of the controller.</summary>
        /// <param name="fileReaderService">The service for reading the CSV file.</param>
        /// <param name="fileUploadService">The service for uploading a CSV file.</param>
        /// <param name="configuration">The configuration for the application.</param>
        public PlayerProjectionController(ICsvFileReaderService fileReaderService, ICsvFileUploaderService fileUploadService, IConfiguration configuration)  
        {
            _fileReaderService = fileReaderService;
            _fileUploadService = fileUploadService;
            _configuration = configuration;
        }

        /// <summary>Gets all of the batters from the projection stats source.</summary>
        /// <returns>All of the batters from the projection stats source.</returns>
        [HttpGet("batters")] public async Task<IEnumerable<ProjectionBattingStats>> GetBatters() =>
            await _fileReaderService.ReadCsvData<ProjectionBattingStats>(new FileReader(_configuration.GetValue<string>("CsvFiles:BatterFile")));

        /// <summary>Gets all of the pitchers from the projection stats source.</summary>
        /// <returns>All of the pitchers from the projection stats source.</returns>
        [HttpGet("pitchers")] public async Task<IEnumerable<ProjectionPitchingStats>> GetPitchers() =>
            await _fileReaderService.ReadCsvData<ProjectionPitchingStats>(new FileReader(_configuration.GetValue<string>("CsvFiles:PitcherFile")));

        /// <summary>Overwrites the underlying batter.csv file.</summary>
        [HttpPost("batters/upload")] public async Task UploadBatterFile() => await UploadFile<ProjectionBattingStats>("CsvFiles:BatterFile");
        
        /// <summary>Overwrites the underlying pitcher.csv file.</summary>
        [HttpPost("pitchers/upload")] public async Task UploadPitcherFile() => await UploadFile<ProjectionPitchingStats>("CsvFiles:PitcherFile");

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