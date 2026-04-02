using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Notification;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Comment;
using Xunit;

namespace So3EindopdrachtTests
{
    public class BacklogItemTests : BaseTest
    {
        private readonly Project _project;
        private readonly BacklogItem _backlogItem;
        private readonly ProjectMember _dev1;
        private readonly ProjectMember _dev2;
        private readonly Mock<INotificationObserver> _notificationMock;

        public BacklogItemTests() : base()
        {
            // Setup Users & Members
            var smUser = new User(1, "SM", "123", "sm@test.nl", 1);
            var devUser1 = new User(2, "Dev1", "456", "d1@test.nl", 2);
            var devUser2 = new User(3, "Dev2", "789", "d2@test.nl", 3);

            var sm = new ProjectMember(smUser, RoleEnum.SCRUM_MASTER);
            _dev1 = new ProjectMember(devUser1, RoleEnum.DEVELOPER);
            _dev2 = new ProjectMember(devUser2, RoleEnum.DEVELOPER);

            // Setup Project
            _project = new Project("Test Project", sm);
            _project.AddProjectMember(_dev1);
            _project.AddProjectMember(_dev2);

            var projectBacklog = (ProjectBacklog)_project.Backlog;
            _backlogItem = new BacklogItem("Test PBI", "Beschrijving", projectBacklog);

            // Setup Notification Mock
            _notificationMock = new Mock<INotificationObserver>();
        }

        // ============================================================
        // FR-2: ACTIVITEITEN & DEVELOPERS
        // ============================================================

        [Fact]
        public void BacklogItem_CanHaveMultipleActivities_FR2_1()
        {
            // Arrange
            var act1 = new Activity("Taak 1", "D", _dev1);
            var act2 = new Activity("Taak 2", "D", _dev2);

            // Act
            _backlogItem.AddActivity(act1);
            _backlogItem.AddActivity(act2);

            // Assert
            Assert.Equal(2, _backlogItem.GetActivities().Count);
        }

        [Fact]
        public void BacklogItem_ShouldAssignOnlyOneDeveloper_FR2_2()
        {
            // Act
            _backlogItem.ProjectMember = _dev1;
            _backlogItem.ProjectMember = _dev2;

            // Assert
            Assert.Equal(_dev2, _backlogItem.ProjectMember);
        }

        // ============================================================
        // FR-4: WORKFLOW VALIDATIE (ACTIVITEITEN CHECK)
        // ============================================================

        [Fact]
        public void BacklogItem_ShouldNotGoToDone_IfActivitiesNotFinished_FR4_2()
        {
            // Arrange
            var act1 = new Activity("Taak 1", "D", _dev1);
            act1.Finished = false;
            _backlogItem.AddActivity(act1);

            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();

            // Act
            _backlogItem.SetDone();

            // Assert
            Assert.NotEqual(typeof(DoneState), _backlogItem.CurrentState.GetType());
        }

        [Fact]
        public void BacklogItem_CanGoToDone_IfAllActivitiesFinished_FR4_2()
        {
            // Arrange
            var act1 = new Activity("Taak 1", "D", _dev1);
            act1.Finished = true;
            _backlogItem.AddActivity(act1);

            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();

            // Act
            _backlogItem.SetDone();

            // Assert
            Assert.IsType<DoneState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // FR-5 & FR-6: DISCUSSIE & NOTIFICATIES
        // ============================================================

        [Fact]
        public void BacklogItem_AddingComment_ShouldNotifyObservers_FR6_3()
        {
            // Arrange
            _dev1.AddObserver(_notificationMock.Object);
            _backlogItem.ProjectMember = _dev1;

            // Act
            new SimpleComment("Dit is een test bericht", _dev2, _backlogItem);

            // Assert
            _notificationMock.Verify(n => n.SendNotification(
                It.Is<string>(s => s.Contains("test bericht")),
                It.IsAny<string>(),
                _dev1), Times.Once);
        }

        [Fact]
        public void BacklogItem_AddingComment_ToDoneItem_ShouldThrowException_FR5_3()
        {
            // Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();
            _backlogItem.SetDone();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new SimpleComment("Dit zou niet moeten kunnen", _dev1, _backlogItem));
        }

        // ============================================================
        // TRACEABILITY CHECK
        // ============================================================

        [Fact]
        public void BacklogItem_ShouldCorrectlyReferenceItsBacklog()
        {
            // Assert
            Assert.NotNull(_backlogItem.Backlog);
            Assert.Equal(_project, ((ProjectBacklog)_backlogItem.Backlog).Project);
        }
    }
}