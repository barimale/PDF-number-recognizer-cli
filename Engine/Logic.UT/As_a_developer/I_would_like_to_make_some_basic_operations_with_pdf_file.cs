using Logic.UT.BaseUT;
using TR.Engine.Services;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace Logic.UT.As_a_developer
{
    public class I_would_like_to_make_some_basic_operations_with_pdf_file : PrintToConsoleUTBase
    {
        public I_would_like_to_make_some_basic_operations_with_pdf_file(
            ITestOutputHelper output)
            : base(output)
        {
            // intentionally left blank
        }

        [Fact]
        public void Read_pdf_file()
        {
            // given
            var engine = new PdfReaderService();
            var path = ".//Data//Refactoring To Patterns - Joshua Kerievsky.pdf";

            // when

            var output = engine.ExtractTextFromPDF(path);

            // then
            Assert.NotNull(output);
        }

        [Fact]
        public async Task Read_pdf_file_and_detect_language()
        {
            // given
            var engine = new PdfReaderService();
            var path = ".//Data//Refactoring To Patterns - Joshua Kerievsky.pdf";
            var output = engine.ExtractTextFromPDF(path);

            // when
            var detector = new LanguageDetector();
            var result = await detector.Detect(output, 5);

            // then
            Assert.NotNull(result);
            Assert.Equal("en", result);
        }

        [Fact]
        public async Task Send_data_to_elasticsearch()
        {
            // given
            var engine = new ToDbService(
                new Uri("https://localhost:9200"),
                "elastic",
                "524DQGTLLZFBOiz8vdba");

            var tweet = new Tweet
            {
                Id = 12,
                User = "stevejgordonasd",
                PostDate = new DateTime(2009, 11, 15),
                Message = "Trying out the client, so far so good?"
            };
            // when

            var result = await engine.AddDocument(tweet, "tweet-index");

            // then
            Assert.True(result);
        }
    }

    internal class Tweet
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime PostDate { get; set; }
        public string Message { get; set; }
    }
}
