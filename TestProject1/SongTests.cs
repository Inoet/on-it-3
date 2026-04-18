using on_it_1.Models;
using Xunit;

namespace NirvanaTests
{
    public class SongTests
    {
        [Fact]
        public void CreateSong_SetsTitleCorrectly()
        {
            var song = new Song { Title = "Lithium" };
            Assert.Equal("Lithium", song.Title);
        }
    }
}