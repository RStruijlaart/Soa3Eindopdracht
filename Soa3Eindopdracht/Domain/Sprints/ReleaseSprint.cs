using Soa3Eindopdracht.Domain.Reports;
using System;

namespace Soa3Eindopdracht.Domain.Sprints
{
    public class ReleaseSprint : Sprint
    {
        public ReleaseSprint(string name, DateTime start, DateTime end)
            : base(name, start, end)
        {
            HasPipeline = true;
        }

        public override bool IsReleaseSprint() => true;

        public override bool GenerateReport()
        {
            var report = GenerateReportObject();

            report.SetHeader("Avans DevOps");
            report.SetFooter($"Datum: {DateTime.Now}");

            report.SetExportStrategy(new PdfExportStrategy()); // kan wisselen!

            report.Export();

            return true;
        }
    }
}