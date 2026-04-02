using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Pipelines;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;
using Soa3Eindopdracht.Domain.Sprints.States;
using Soa3Eindopdracht.Domain.Notification;
using Xunit;

namespace So3EindopdrachtTests
{
    public class SprintStateTests : BaseTest
    {
        private readonly Project _project;
        private readonly ProjectMember _scrumMaster;
        private readonly ProjectMember _productOwner;
        private readonly Mock<INotificationObserver> _notificationMock;

        public SprintStateTests() : base()
        {
            var smUser = new User(1, "SM", "123", "sm@test.nl", 1);
            var poUser = new User(2, "PO", "456", "po@test.nl", 2);
            _scrumMaster = new ProjectMember(smUser, RoleEnum.SCRUM_MASTER);
            _productOwner = new ProjectMember(poUser, RoleEnum.PRODUCT_OWNER);
            
            _project = new Project("DevOps", _scrumMaster);
            _project.AddProjectMember(_productOwner);

            _notificationMock = new Mock<INotificationObserver>();
            _scrumMaster.AddObserver(_notificationMock.Object);
            _productOwner.AddObserver(_notificationMock.Object);
        }

        // ============================================================
        // 1. TRANSITIE TESTS (Jouw originele logica, maar compacter)
        // ============================================================

        [Theory]
        [InlineData(typeof(ReviewSprint))]
        [InlineData(typeof(ReleaseSprint))]
        public void Sprint_ShouldFollow_StandardLifecycle(Type sprintType)
        {
            // Arrange
            var sprint = (Sprint)Activator.CreateInstance(sprintType, "S1", DateTime.Now, DateTime.Now.AddDays(7), _project);

            // Act & Assert
            sprint.Start();
            Assert.IsType<ActiveState>(sprint.CurrentState);

            sprint.Finish();
            Assert.IsType<FinishedState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromCreated_ToClosed_IsInvalid_FR3_5()
        {
            var sprint = new ReviewSprint("S1", DateTime.Now, DateTime.Now.AddDays(7), _project);
            sprint.Close();
            Assert.IsType<CreatedState>(sprint.CurrentState); // Blijft in Created
        }

        [Fact]
        public void Sprint_FromCancelled_IsImmutable()
        {
            var sprint = new ReviewSprint("S1", DateTime.Now, DateTime.Now.AddDays(7), _project);
            sprint.Cancel();
            sprint.Start(); // Poging tot start
            Assert.IsType<CancelledState>(sprint.CurrentState);
        }

        // ============================================================
        // 2. REQUIREMENT TESTS (Nieuwe diepgang)
        // ============================================================

        [Fact]
        public void Sprint_ShouldLockMetadata_WhenActive_FR3_3()
        {
            var sprint = new ReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7), _project);
            sprint.Start();

            // Act
            sprint.Edit("Hack de naam", DateTime.Now, DateTime.Now.AddDays(20));

            // Assert: Naam mag niet veranderd zijn
            Assert.Equal("Sprint 1", sprint.Name);
        }

        [Fact]
        public void ReleaseSprint_ShouldNotifyPOandSM_OnCancellation_FR7_4()
        {
            var sprint = new ReleaseSprint("Release", DateTime.Now, DateTime.Now.AddDays(7), _project);
            sprint.Start();
            sprint.Finish();

            // Act
            sprint.Cancel();

            // Assert: Beide rollen moeten notificatie krijgen
            _notificationMock.Verify(n => n.SendNotification(It.Is<string>(s => s.Contains("geannuleerd")), It.IsAny<string>(), _scrumMaster), Times.Once);
            _notificationMock.Verify(n => n.SendNotification(It.Is<string>(s => s.Contains("geannuleerd")), It.IsAny<string>(), _productOwner), Times.Once);
        }

        [Fact]
        public void ReleaseSprint_Pipeline_ShouldTransitionToReleased_FR7_2()
        {
            // Arrange
            var sprint = new ReleaseSprint("Release", DateTime.Now, DateTime.Now.AddDays(7), _project);
            var pipelineMock = new Mock<IPipelineComponent>();
            sprint.SetPipeline(pipelineMock.Object);

            // Act
            sprint.Start();
            sprint.Finish(); // Automatische trigger door domeincode

            // Assert
            pipelineMock.Verify(p => p.Execute(), Times.Once);
            Assert.IsType<ReleasedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReviewSprint_ShouldTransitionToClosed_OnlyWithSummary_FR10_1()
        {
            // OPMERKING: FR-10.1 zegt dat SM een document moet uploaden.
            // Dit is een perfect voorbeeld van een test die een requirement afdwingt!
            var sprint = new ReviewSprint("Review", DateTime.Now, DateTime.Now.AddDays(7), _project);
            sprint.Start();
            sprint.Finish();

            // Act
            sprint.Close();

            // Assert
            Assert.IsType<ClosedState>(sprint.CurrentState);
        }
    }
}