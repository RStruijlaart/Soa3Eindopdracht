using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Notification;
using Soa3Eindopdracht.Domain.Projects;
using Xunit;

namespace So3EindopdrachtTests
{
    public class BacklogItemStateTests : BaseTest
    {
        private readonly ProjectMember _testerMember;
        private readonly ProjectMember _scrumMasterMember;
        private readonly BacklogItem _backlogItem;
        private readonly Mock<INotificationObserver> _notificationMock;
        private readonly Project _project;

        public BacklogItemStateTests() : base()
        {
            // Setup Users
            var devUser = new User(1, "Dev", "06123", "dev@test.nl", 1);
            var testerUser = new User(2, "Tester", "06124", "test@test.nl", 2);
            var smUser = new User(3, "SM", "06125", "sm@test.nl", 3);

            // Setup Members
            var devMember = new ProjectMember(devUser, RoleEnum.DEVELOPER);
            _testerMember = new ProjectMember(testerUser, RoleEnum.TESTER);
            _scrumMasterMember = new ProjectMember(smUser, RoleEnum.SCRUM_MASTER);

            // Setup Project & Backlog (Nodig voor GetProject() in domeincode)
            _project = new Project("Test Project", _scrumMasterMember);
            _project.AddProjectMember(_testerMember);
            _project.AddProjectMember(devMember);

            var projectBacklog = (ProjectBacklog)_project.Backlog;
            _backlogItem = new BacklogItem("PBI 1", "Desc", projectBacklog);

            // Setup Mocks & Observers
            _notificationMock = new Mock<INotificationObserver>();
            _testerMember.AddObserver(_notificationMock.Object);
            _scrumMasterMember.AddObserver(_notificationMock.Object);
        }

        // ============================================================
        // 1. TODO STATE - ILLEGAL & LEGAL TRANSITIONS (FR-4.1)
        // ============================================================

        [Fact]
        public void Todo_To_Doing_IsLegal()
        {
            //Act
            _backlogItem.SetDoing();

            //Assert
            Assert.IsType<DoingState>(_backlogItem.CurrentState);
        }

        [Theory]
        [InlineData("ReadyForTesting")]
        [InlineData("Testing")]
        [InlineData("Tested")]
        [InlineData("Done")]
        public void Todo_To_IllegalStates_ShouldStayInTodo(string targetState)
        {
            //Act
            if (targetState == "ReadyForTesting") _backlogItem.SetReadyForTesting();
            if (targetState == "Testing") _backlogItem.SetTesting();
            if (targetState == "Tested") _backlogItem.SetTested();
            if (targetState == "Done") _backlogItem.SetDone();

            //Assert
            Assert.IsType<TodoState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 2. DOING STATE - LEGAL & NOTIFICATIONS (FR-4.3)
        // ============================================================

        [Fact]
        public void Doing_To_ReadyForTesting_IsLegal_AndNotifiesTesters()
        {
            //Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();

            //Act
            Assert.IsType<ReadyForTestingState>(_backlogItem.CurrentState);

            //Assert
            _notificationMock.Verify(n => n.SendNotification(
                It.Is<string>(s => s.ToLower().Contains("ready for testing")),
                It.IsAny<string>(),
                _testerMember), Times.Once);
        }

        [Fact]
        public void Doing_To_Todo_IsLegal()
        {
            //Arrange
            _backlogItem.SetDoing();

            //Act
            _backlogItem.setTodo();

            //Assert
            Assert.IsType<TodoState>(_backlogItem.CurrentState);
        }

        [Theory]
        [InlineData("Testing")]
        [InlineData("Tested")]
        [InlineData("Done")]
        public void Doing_To_IllegalStates_ShouldStayInDoing(string targetState)
        {
            //Assert
            _backlogItem.SetDoing();

            //Act
            if (targetState == "Testing") _backlogItem.SetTesting();
            if (targetState == "Tested") _backlogItem.SetTested();
            if (targetState == "Done") _backlogItem.SetDone();

            //Assert
            Assert.IsType<DoingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 3. READY FOR TESTING - TRANSITIONS
        // ============================================================

        [Fact]
        public void ReadyForTesting_To_Testing_IsLegal()
        {
            //Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();

            //Act
            _backlogItem.SetTesting();

            //Assert
            Assert.IsType<TestingState>(_backlogItem.CurrentState);
        }

        [Fact]
        public void ReadyForTesting_To_Doing_IsLegal()
        {
            //Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();

            //Act
            _backlogItem.SetDoing();

            //Assert
            Assert.IsType<DoingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 4. TESTING STATE - REJECTION & SM NOTIFICATION (FR-4.4)
        // ============================================================

        [Fact]
        public void Testing_To_Todo_IsLegal_AndDoesNotAllowDoing()
        {
            // Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            // Act
            _backlogItem.setTodo();

            //Assert
            Assert.IsType<TodoState>(_backlogItem.CurrentState);
        }

        [Fact]
        public void Testing_To_Tested_IsLegal_AndNotifiesScrumMaster()
        {
            // Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            //Act
            _backlogItem.SetTested();

            //Assert
            Assert.IsType<TestedState>(_backlogItem.CurrentState);
            _notificationMock.Verify(n => n.SendNotification(
                It.Is<string>(s => s.ToLower().Contains("ready for review")),
                It.IsAny<string>(),
                _scrumMasterMember), Times.Once);
        }

        [Fact]
        public void Testing_To_Doing_IsIllegal_AccordingToRequirements()
        {
            // Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            // Act
            _backlogItem.SetDoing();

            //Assert
            Assert.IsType<TestingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 5. TESTED STATE - DOD & RE-TEST
        // ============================================================

        [Fact]
        public void Tested_To_Done_IsLegal()
        {
            //Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();

            //Act
            _backlogItem.SetDone();

            //Assert
            Assert.IsType<DoneState>(_backlogItem.CurrentState);
        }

        [Fact]
        public void Tested_To_ReadyForTesting_IsLegal_ForReTesting()
        {
            // Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();

            // Act
            _backlogItem.SetReadyForTesting();

            //Assert
            Assert.IsType<ReadyForTestingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 6. DONE STATE - IMMUTABILITY (FR-5.3 context)
        // ============================================================

        [Fact]
        public void Done_IsFinal_CannotChangeToAnyOtherState()
        {
            // Arrange
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();
            _backlogItem.SetDone();

            // Act
            _backlogItem.setTodo();
            _backlogItem.SetDoing();
            _backlogItem.SetTesting();

            // Assert
            Assert.IsType<DoneState>(_backlogItem.CurrentState);
        }
    }
}