using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FantasyBaseball.Common.Exceptions;

namespace FantasyBaseball.PlayerProjectionService.FileReaders
{
    /// <summary>Helper for reading the contents of a file.</summary>
    public class FileReader : IFileReader
    {
        private readonly string fileName;

        /// <summary>Creates a wrapper around the file.</summary>
        /// <param name="fileName">The full path and name of the file.</param>
        public FileReader(string fileName) => this.fileName = fileName;

        /// <summary>Reads in all of the lines from the file.</summary>
        /// <returns>All of the lines from the files.</returns>
        public async Task<List<string>> ReadLines()
        {
            if (!File.Exists(fileName)) throw new CsvFileException($"Unable to load file: {fileName}");
            return (await File.ReadAllLinesAsync(fileName)).ToList();
        }
    }
}