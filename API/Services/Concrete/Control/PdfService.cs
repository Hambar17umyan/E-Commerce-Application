using API.Models.Domain.Concrete;
using API.Services.Interfaces.Control;
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
            using var memoryStream = new MemoryStream();
            using var pdfWriter = new PdfWriter(memoryStream);
            using var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            // Add Title
            document.Add(new Paragraph("Order Details").SetFontSize(18).SimulateBold().SetTextAlignment(TextAlignment.CENTER));

            // Add Order Information
            document.Add(new Paragraph($"Order ID: {order.Id}"));
            document.Add(new Paragraph($"User ID: {order.UserId ?? 0}"));
            document.Add(new Paragraph($"Creation Date: {order.CreationDateTime:yyyy-MM-dd HH:mm:ss}"));

            // Add Line Items
            document.Add(new Paragraph("\nLine Items:").SimulateBold());
            var table = new Table(UnitValue.CreatePercentArray(new float[] { 3, 2, 2, 2 })).UseAllAvailableWidth();
            table.AddHeaderCell("Product Name");
            table.AddHeaderCell("Quantity");
            table.AddHeaderCell("Price (AMD)");
            table.AddHeaderCell("Total Price (AMD)");

            foreach (var lineItem in order.LineItems)
            {
                table.AddCell(lineItem.Product.Name);
                table.AddCell(lineItem.Quantity.ToString());
                table.AddCell(lineItem.Product.Price.ToString("F"));
                table.AddCell(lineItem.OverallPriceAMD.ToString("F"));
            }

            document.Add(table);

            // Calculate Total
            decimal total = order.LineItems.Sum(li => li.OverallPriceAMD);
            document.Add(new Paragraph($"\nTotal Amount: {total:F} AMD").SimulateBold().SetTextAlignment(TextAlignment.RIGHT));

            document.Close();
            return memoryStream.ToArray();
        }


    }
}
