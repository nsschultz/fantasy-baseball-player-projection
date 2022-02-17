using System.IO;
using System.Threading.Tasks;
using FantasyBaseball.PlayerProjectionService.FileReaders;

namespace FantasyBaseball.PlayerProjectionService.Services
{
    /// <summary>Service for uploading a CSV file.</summary>
    public class CsvFileUploaderService : ICsvFileUploaderService
    {
        /// <summary>Reads the file off the HTTP request and saves it to file system.</summary>
        /// <param name="fileReader">Helper for reading the contents of a file.</param>
        /// <param name="fileName">The file name to process.</param>
        public async Task UploadFile(IFileReader fileReader, string fileName)
        {
            var lines = await fileReader.ReadLines();
            File.Delete(fileName);
            using var stream = new FileStream(fileName, FileMode.Create);
            using var writer = new StreamWriter(stream);
            foreach (var line in lines) await writer.WriteLineAsync(line);
        }
    }
}