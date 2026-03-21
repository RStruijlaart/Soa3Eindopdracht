using System;

namespace Soa3Eindopdracht.Domain.Reports
{
    public class Report
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string Header { get; private set; }
        public string Footer { get; private set; }

        private IReportExportStrategy _exportStrategy;

        public Report(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public void SetHeader(string header)
        {
            Header = header;
        }

        public void SetFooter(string footer)
        {
            Footer = footer;
        }

        public void SetExportStrategy(IReportExportStrategy strategy)
        {
            _exportStrategy = strategy;
        }

        public void Export()
        {
            if (_exportStrategy == null)
                throw new InvalidOperationException("Geen exportstrategie ingesteld.");

            _exportStrategy.Export(this);
        }
    }
}