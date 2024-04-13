using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

namespace TR.Engine.Services
{
    public class PdfReaderService
    {
        public List<string> ExtractTextFromPDF(string filePath)
        {
            List<string> lines = new List<string>();

            using (PdfReader pdfReader = new PdfReader(filePath))
            {
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string pageContent = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    var words = pageContent.Split('\n');
                    for (int j = 0, len = words.Length; j < len; j++)
                    {
                        lines.Add(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j])));
                    }
                }
            }

            return lines;
        }
    }
}
