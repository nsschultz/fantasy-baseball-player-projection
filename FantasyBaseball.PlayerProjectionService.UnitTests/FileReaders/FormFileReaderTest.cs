using System.Threading.Tasks;
using FantasyBaseball.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerProjectionService.FileReaders
{
    public class FormFileReaderTest
    {
        [Fact] public async Task NoFilesTest() 
        {
            var formFileCollection = new Mock<IFormFileCollection>();
            formFileCollection.Setup(o => o.Count).Returns(0);
            var formCollection = new Mock<IFormCollection>();
            formCollection.Setup(o => o.Files).Returns(formFileCollection.Object);
            var request = new Mock<HttpRequest>();
            request.Setup(o => o.ReadFormAsync(default)).ReturnsAsync(formCollection.Object);
            await Assert.ThrowsAsync<BadRequestException>(() => new FormFileReader(request.Object).ReadLines());
        }
    }
}