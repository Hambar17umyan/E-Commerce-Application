using API.Models.Domain.Concrete;
using API.Services.Interfaces.Control;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;

namespace API.Services.Concrete.Control
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateOrderPdf(Order order)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(memoryStream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        // Load a default font
                        PdfFont font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
                        document.SetFont(font);

                        // Add Title
                        AddTitle(document, $"Order Documentation - ID: {order.Id}");

                        // Add Order Details Section
                        AddSection(document, "Order Details", $"" +
                            $"Order ID: {order.Id}\n" +
                            $"User ID: {order.UserId}\n" +
                            $"Creation Date: {order.CreationDateTime}\n" +
                            $"Number of Line Items: {order.LineItems.Count}\n");

                        // Add Line Items Section
                        foreach (var lineItem in order.LineItems)
                        {
                            AddSection(document, "Line Item", $"" +
                                $"Line Item ID: {lineItem.Id}\n" +
                                $"Product ID: {lineItem.ProductId}\n" +
                                $"Product Name: {lineItem.Product?.Name}\n" +
                                $"Quantity: {lineItem.Quantity}\n" +
                                $"Overall Price (AMD): {lineItem.OverallPriceAMD}\n");
                        }

                        document.Close();
                    }
                }
                return memoryStream.ToArray(); // Return the PDF data as byte array
            }
        }

        private void AddTitle(Document document, string title)
        {
            document.Add(new Paragraph(title)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20));
            document.Add(new Paragraph("\n")); // Add spacing
        }

        private void AddSection(Document document, string sectionTitle, string sectionContent)
        {
            document.Add(new Paragraph(sectionTitle)
                .SetFontSize(14)
                .SetUnderline());
            document.Add(new Paragraph("\n")); // Add spacing

            document.Add(new Paragraph(sectionContent)
                .SetFontSize(10));
            document.Add(new Paragraph("\n")); // Add spacing
        }
    }
}
