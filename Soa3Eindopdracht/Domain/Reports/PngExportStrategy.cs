using System;

namespace Soa3Eindopdracht.Domain.Reports
{
    public class PngExportStrategy : IReportExportStrategy
    {
        public void Export(Report report)
        {
            Console.WriteLine("=== Exporting PNG ===");
            Console.WriteLine($"Header: {report.Header}");
            Console.WriteLine($"Title: {report.Title}");
            Console.WriteLine($"Content: {report.Content}");
            Console.WriteLine($"Footer: {report.Footer}");
            Console.WriteLine("PNG export voltooid.");
        }
    }
}