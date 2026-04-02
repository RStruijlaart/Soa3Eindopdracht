using Moq;
using Soa3Eindopdracht.Domain.Reports;
using Xunit;

namespace So3EindopdrachtTests
{
    public class ReportTests : BaseTest
    {
        public ReportTests() : base()
        {
        }

        [Fact]
        public void Report_ShouldUsePdfStrategy_ToExport_FR8_3()
        {
            // Arrange
            var report = new Report("Sprint Rapport", "Data...");
            report.SetHeader("Bedrijfsnaam");
            report.SetFooter("Pagina 1");

            var strategyMock = new Mock<IReportExportStrategy>();
            report.SetExportStrategy(strategyMock.Object);

            // Act
            report.Export();

            // Assert
            strategyMock.Verify(s => s.Export(report), Times.Once);
        }

        [Fact]
        public void Report_ShouldThrowException_IfNoStrategySet()
        {
            // Arrange
            var report = new Report("Titel", "Content");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => report.Export());
        }

        [Fact]
        public void PdfStrategy_ShouldPrintToConsole()
        {
            // Arrange
            var report = new Report("T", "C");
            var strategy = new PdfExportStrategy();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                strategy.Export(report);

                // Assert
                var output = sw.ToString();
                Assert.Contains("Exporting PDF", output);
            }
        }
    }
}