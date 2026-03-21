using System;

namespace Soa3Eindopdracht.Domain.Reports
{
    public class PdfExportStrategy : IReportExportStrategy
    {
        public void Export(Report report)
        {
            Console.WriteLine("=== Exporting PDF ===");
            Console.WriteLine($"Header: {report.Header}");
            Console.WriteLine($"Title: {report.Title}");
            Console.WriteLine($"Content: {report.Content}");
            Console.WriteLine($"Footer: {report.Footer}");
            Console.WriteLine("PDF export voltooid.");
        }
    }
}