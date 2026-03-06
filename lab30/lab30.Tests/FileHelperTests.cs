using Xunit;
using lab30vN;
using System;

namespace lab30vN.Tests
{
    public class FileHelperTests
    {
        private readonly FileHelper helper = new FileHelper();

        [Fact]
        public void GetExtension_ReturnsTxt()
        {
            var result = helper.GetExtension("file.txt");

            Assert.Equal(".txt", result);
        }

        [Fact]
        public void GetExtension_ReturnsPng()
        {
            var result = helper.GetExtension("image.png");

            Assert.Equal(".png", result);
        }

        [Fact]
        public void GetExtension_EmptyName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => helper.GetExtension(""));
        }

        [Fact]
        public void GetExtension_FileWithoutExtension_ReturnsEmpty()
        {
            var result = helper.GetExtension("file");

            Assert.Equal("", result);
        }

        [Theory]
        [InlineData("photo.jpg")]
        [InlineData("picture.png")]
        [InlineData("image.jpeg")]
        [InlineData("icon.gif")]
        [InlineData("bitmap.bmp")]
        public void IsImageFile_ValidImages_ReturnsTrue(string fileName)
        {
            var result = helper.IsImageFile(fileName);

            Assert.True(result);
        }

        [Theory]
        [InlineData("document.txt")]
        [InlineData("video.mp4")]
        [InlineData("archive.zip")]
        public void IsImageFile_NotImage_ReturnsFalse(string fileName)
        {
            var result = helper.IsImageFile(fileName);

            Assert.False(result);
        }

        [Fact]
        public void IsImageFile_Empty_ReturnsFalse()
        {
            var result = helper.IsImageFile("");

            Assert.False(result);
        }

        [Fact]
        public void IsImageFile_UpperCaseExtension_ReturnsTrue()
        {
            var result = helper.IsImageFile("PHOTO.JPG");

            Assert.True(result);
        }
    }
}
