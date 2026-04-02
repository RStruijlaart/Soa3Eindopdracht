using Moq;
using Soa3Eindopdracht.Domain.Pipelines;
using Soa3Eindopdracht.Domain.Pipelines.Actions;
using Soa3Eindopdracht.Domain.Sprints;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Notification;
using Soa3Eindopdracht.Domain;
using Xunit;

namespace So3EindopdrachtTests
{
    public class PipelineTests : BaseTest
    {
        private readonly Mock<INotificationObserver> _notificationMock;
        private readonly ProjectMember _scrumMaster;
        private readonly Project _project;

        public PipelineTests() : base()
        {
            // Setup voor integratie tests
            var user = new User(1, "SM", "123", "sm@test.nl", 1);
            _scrumMaster = new ProjectMember(user, RoleEnum.SCRUM_MASTER);
            _project = new Project("DevOps Project", _scrumMaster);

            _notificationMock = new Mock<INotificationObserver>();
            _scrumMaster.AddObserver(_notificationMock.Object);
        }

        // ============================================================
        // COMPOSITE PATTERN TESTS
        // ============================================================

        [Fact]
        public void Pipeline_ShouldExecuteAllAddedActions_FR7_1()
        {
            // Arrange
            var pipeline = new PipelineComposite();
            pipeline.Add(new SourceAction());
            pipeline.Add(new BuildAction());
            pipeline.Add(new TestAction());

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                pipeline.Execute();

                // Assert
                var output = sw.ToString();
                Assert.Contains("Source code ophalen", output);
                Assert.Contains("Build uitvoeren", output);
                Assert.Contains("Tests uitvoeren", output);
            }
        }

        [Fact]
        public void Pipeline_ShouldSupportNestedPipelines_CompositePattern()
        {
            // Arrange
            var mainPipeline = new PipelineComposite();
            var subPipeline = new PipelineComposite();

            subPipeline.Add(new BuildAction());
            mainPipeline.Add(subPipeline);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                mainPipeline.Execute();

                // Assert
                var output = sw.ToString();
                Assert.Contains("Build uitvoeren", output);
            }
        }

        // ============================================================
        // FR-7: SPRINT INTEGRATIE & AUTOMATISERING
        // ============================================================

        [Fact]
        public void ReleaseSprint_ShouldAutomaticallyExecutePipeline_WhenFinished_FR7_2()
        {
            // Arrange
            var start = DateTime.Now;
            var end = start.AddDays(14);
            var sprint = new ReleaseSprint("Release 1.0", start, end, _project);

            var pipelineMock = new Mock<IPipelineComponent>();
            sprint.SetPipeline(pipelineMock.Object);

            // Act
            sprint.Start();
            sprint.Finish();
            sprint.StartReleasePipeline(); // In jouw FinishedState.cs triggert dit de execute

            // Assert
            pipelineMock.Verify(p => p.Execute(), Times.Once);
            Assert.IsType<Soa3Eindopdracht.Domain.Sprints.States.ReleasedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReviewSprint_ShouldNotSupportPipeline_FR7_Voorwaarde()
        {
            // Arrange
            var start = DateTime.Now;
            var end = start.AddDays(14);
            var sprint = new ReviewSprint("Review 1.0", start, end, _project);
            var pipeline = new PipelineComposite();

            // Act & Assert
            // Volgens jouw Sprint.SetPipeline() gooit dit een exception bij ReviewSprints
            Assert.Throws<InvalidOperationException>(() => sprint.SetPipeline(pipeline));
        }

        // ============================================================
        // FR-7.3 & 7.4: ERROR HANDLING & NOTIFICATIONS
        // ============================================================

        [Fact]
        public void Pipeline_Failure_ShouldNotifyScrumMaster_FR7_3()
        {
            // OPMERKING: Jouw huidige FinishedState.cs heeft nog geen try-catch rond de execute.
            // Deze test dwingt af dat we nadenken over wat er gebeurt als de pipeline faalt.

            // Arrange
            var start = DateTime.Now;
            var end = start.AddDays(14);
            var sprint = new ReleaseSprint("Failed Release", start, end, _project);

            var failingPipeline = new Mock<IPipelineComponent>();
            failingPipeline.Setup(p => p.Execute()).Throws(new Exception("Build Failed"));

            sprint.SetPipeline(failingPipeline.Object);
            sprint.Start();
            sprint.Finish();

            // Act
            try { sprint.StartReleasePipeline(); } catch { /* Simulatie van fout */ }

            // Assert
            // Volgens FR-7.3 moet de SM bericht krijgen bij een fout. 
            // Hiervoor moet je in de FinishedState.cs een try-catch toevoegen die de SM notificeert.
            _notificationMock.Verify(n => n.SendNotification(
                It.Is<string>(s => s.Contains("fout") || s.Contains("gefaald") || s.Contains("Failed")),
                It.IsAny<string>(),
                _scrumMaster), Times.AtLeastOnce);
        }
    }
}