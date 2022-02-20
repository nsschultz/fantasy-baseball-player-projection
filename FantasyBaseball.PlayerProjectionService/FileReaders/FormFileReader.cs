using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FantasyBaseball.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FantasyBaseball.PlayerProjectionService.FileReaders
{
    /// <summary>Helper for reading the contents of a file on a web request.</summary>
    public class FormFileReader : IFileReader
    {
        private readonly HttpRequest request;

        /// <summary>Creates a wrapper around the web request.</summary>
        /// <param name="request">The incoming web request.</param>
        public FormFileReader(HttpRequest request) => this.request = request;

        /// <summary>Reads in all of the lines from the file.</summary>
        /// <returns>All of the lines from the files.</returns>
        public async Task<List<string>> ReadLines()
        {
            var result = new List<string>();
            var file = await GetFileFromRequest();
            using var reader = new StreamReader(file.OpenReadStream());
            while (reader.Peek() >= 0) result.Add(await reader.ReadLineAsync()); 
            return result;
        }

        private async Task<IFormFile> GetFileFromRequest()
        {
            var files = (await this.request.ReadFormAsync()).Files;
            if (files.Count == 0) throw new BadRequestException("There are no files attached.");
            return files[0];
        }
    }
}