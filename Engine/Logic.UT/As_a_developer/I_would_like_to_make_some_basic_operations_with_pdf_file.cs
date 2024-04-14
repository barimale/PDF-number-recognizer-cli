using Logic.UT.BaseUT;
using TR.Engine.Services;
using Xunit.Abstractions;

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
            var result = await detector.Detect(output, 5, "ExecuteAll");

            // then
            Assert.NotNull(result);
            Assert.Equal("en", result);
        }
    }
}
