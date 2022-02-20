using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyBaseball.PlayerProjectionService.FileReaders;

namespace FantasyBaseball.PlayerProjectionService.Services
{
    /// <summary>Service for reading CSV file.</summary>
    public interface ICsvFileReaderService
    {
        /// <summary>Reads in data from the given CSV file.</summary>
        /// <param name="fileReader">Helper for reading the contents of a file.</param>
        /// <returns>All of the data within the given file.</returns>
        Task<List<T>> ReadCsvData<T>(IFileReader fileReader);
    }
}