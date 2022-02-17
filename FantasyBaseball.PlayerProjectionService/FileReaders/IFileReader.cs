using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyBaseball.PlayerProjectionService.FileReaders
{
    /// <summary>Helper for reading the contents of a file.</summary>
    public interface IFileReader
    {
        /// <summary>Reads in all of the lines from the file.</summary>
        /// <returns>All of the lines from the files.</returns>
        Task<List<string>> ReadLines();
    }
}