using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Reports;
using System;

namespace Soa3Eindopdracht.Domain.Sprints
{
    public class ReviewSprint : Sprint
    {
        public ReviewSprint(string name, DateTime start, DateTime end, Project project)
            : base(name, start, end, project)
        {
            HasPipeline = false;
        }

        public override bool IsReleaseSprint() => false;

        public override bool GenerateReport()
        {
            var report = GenerateReportObject();

            report.SetHeader("Avans DevOps");
            report.SetFooter($"Datum: {DateTime.Now}");

            report.SetExportStrategy(new PdfExportStrategy());

            report.Export();

            return true;
        }
    }
}