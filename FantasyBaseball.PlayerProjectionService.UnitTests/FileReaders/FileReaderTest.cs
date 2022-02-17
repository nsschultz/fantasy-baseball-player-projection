using System.Threading.Tasks;
using FantasyBaseball.Common.Exceptions;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.FileReaders
{
    public class FileReaderTest
    {
        [Fact] public async Task BadFileTest() => await Assert.ThrowsAsync<CsvFileException>(() => new FileReader("bad.csv").ReadLines());

        [Fact] public async Task NullFileTest() => await Assert.ThrowsAsync<CsvFileException>(() => new FileReader(null).ReadLines());

        [Fact] public async Task RealFileTest() 
        {
            var results = await new FileReader("data/test-batter.csv").ReadLines();
            Assert.Equal(4, results.Count);
        }
    }
}