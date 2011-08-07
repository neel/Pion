using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Pion.Domain.UnitTests
{
    [TestFixture]
    public class YouTubeParserTests
    {
        [Test]
        [ExpectedException(ExpectedException=typeof(ArgumentNullException))]
        public void Constructor_MissingHtmlSource_ThrowsException()
        {
            // Arrange + Act + Assert
            YouTubeParser parser = new YouTubeParser(null);
        }

        [Test]
        [ExpectedException(ExpectedException=typeof(InvalidSyntaxException))]
        public void ExtractTitle_MissingStartTagInSource_ThrowsException()
        {
            // Arrange
            string testVideoTitle = "TEST";

            YouTubeParser parser = new YouTubeParser(testVideoTitle + YouTubeParser.VIDEO_END_TAG);

            // Act + Assert
            parser.ExtractTitle();
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(InvalidSyntaxException))]
        public void ExtractTitle_MissingEndTagInSource_ThrowsException()
        {
            // Arrange
            string testVideoTitle = "TEST";

            YouTubeParser parser = new YouTubeParser(YouTubeParser.VIDEO_START_TAG + testVideoTitle);

            // Act + Assert
            parser.ExtractTitle();
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(InvalidSyntaxException))]
        public void ExtractTitle_MissingVideoTitleInSource_ThrowsException()
        {
            // Arrange
            YouTubeParser parser = new YouTubeParser(YouTubeParser.VIDEO_START_TAG + YouTubeParser.VIDEO_END_TAG);

            // Act + Assert
            parser.ExtractTitle();
        }

        [Test]
        public void ExtractTitle_ValidHtml_ReturnsCorrectVideoTitle()
        {
            // Arrange
            string expectedVideoTitle = "TEST";

            YouTubeParser parser = new YouTubeParser(YouTubeParser.VIDEO_START_TAG + expectedVideoTitle + YouTubeParser.VIDEO_END_TAG);

            // Act
            string actualVideoTitle = parser.ExtractTitle();

            // Assert
            Assert.AreEqual(expectedVideoTitle, actualVideoTitle);
        }
    }
}
