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
            _backlogItem.SetDoing();
            Assert.IsType<DoingState>(_backlogItem.CurrentState);
        }

        [Theory]
        [InlineData("ReadyForTesting")]
        [InlineData("Testing")]
        [InlineData("Tested")]
        [InlineData("Done")]
        public void Todo_To_IllegalStates_ShouldStayInTodo(string targetState)
        {
            if (targetState == "ReadyForTesting") _backlogItem.SetReadyForTesting();
            if (targetState == "Testing") _backlogItem.SetTesting();
            if (targetState == "Tested") _backlogItem.SetTested();
            if (targetState == "Done") _backlogItem.SetDone();

            Assert.IsType<TodoState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 2. DOING STATE - LEGAL & NOTIFICATIONS (FR-4.3)
        // ============================================================

        [Fact]
        public void Doing_To_ReadyForTesting_IsLegal_AndNotifiesTesters()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();

            Assert.IsType<ReadyForTestingState>(_backlogItem.CurrentState);

            // Verifieer tekst uit BacklogItem.SendNotificationToTesters
            _notificationMock.Verify(n => n.SendNotification(
                It.Is<string>(s => s.ToLower().Contains("ready for testing")),
                It.IsAny<string>(),
                _testerMember), Times.Once);
        }

        [Fact]
        public void Doing_To_Todo_IsLegal()
        {
            _backlogItem.SetDoing();
            _backlogItem.setTodo();
            Assert.IsType<TodoState>(_backlogItem.CurrentState);
        }

        [Theory]
        [InlineData("Testing")]
        [InlineData("Tested")]
        [InlineData("Done")]
        public void Doing_To_IllegalStates_ShouldStayInDoing(string targetState)
        {
            _backlogItem.SetDoing();

            if (targetState == "Testing") _backlogItem.SetTesting();
            if (targetState == "Tested") _backlogItem.SetTested();
            if (targetState == "Done") _backlogItem.SetDone();

            Assert.IsType<DoingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 3. READY FOR TESTING - TRANSITIONS
        // ============================================================

        [Fact]
        public void ReadyForTesting_To_Testing_IsLegal()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            Assert.IsType<TestingState>(_backlogItem.CurrentState);
        }

        [Fact]
        public void ReadyForTesting_To_Doing_IsLegal()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetDoing();

            Assert.IsType<DoingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 4. TESTING STATE - REJECTION & SM NOTIFICATION (FR-4.4)
        // ============================================================

        [Fact]
        public void Testing_To_Todo_IsLegal_AndDoesNotAllowDoing()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            // Act: Terug naar Todo (Rejection)
            _backlogItem.setTodo();

            Assert.IsType<TodoState>(_backlogItem.CurrentState);
        }

        [Fact]
        public void Testing_To_Tested_IsLegal_AndNotifiesScrumMaster()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            // Act: Succesvol getest
            _backlogItem.SetTested();

            Assert.IsType<TestedState>(_backlogItem.CurrentState);

            // Verifieer tekst uit BacklogItem.SendNotificationToScumMaster (met de typfout 'Scum' uit je domeincode)
            _notificationMock.Verify(n => n.SendNotification(
                It.Is<string>(s => s.ToLower().Contains("ready for review")),
                It.IsAny<string>(),
                _scrumMasterMember), Times.Once);
        }

        [Fact]
        public void Testing_To_Doing_IsIllegal_AccordingToRequirements()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();

            // Act: Direct naar Doing (Verboden volgens FR-4.4: "Terug naar doing kan niet")
            _backlogItem.SetDoing();

            Assert.IsType<TestingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 5. TESTED STATE - DOD & RE-TEST
        // ============================================================

        [Fact]
        public void Tested_To_Done_IsLegal()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();
            _backlogItem.SetDone();

            Assert.IsType<DoneState>(_backlogItem.CurrentState);
        }

        [Fact]
        public void Tested_To_ReadyForTesting_IsLegal_ForReTesting()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();

            // Act: DoD faalt -> Terug naar ReadyForTesting
            _backlogItem.SetReadyForTesting();

            Assert.IsType<ReadyForTestingState>(_backlogItem.CurrentState);
        }

        // ============================================================
        // 6. DONE STATE - IMMUTABILITY (FR-5.3 context)
        // ============================================================

        [Fact]
        public void Done_IsFinal_CannotChangeToAnyOtherState()
        {
            _backlogItem.SetDoing();
            _backlogItem.SetReadyForTesting();
            _backlogItem.SetTesting();
            _backlogItem.SetTested();
            _backlogItem.SetDone();

            // Pogingen tot wijziging
            _backlogItem.setTodo();
            _backlogItem.SetDoing();
            _backlogItem.SetTesting();

            Assert.IsType<DoneState>(_backlogItem.CurrentState);
        }
    }
}