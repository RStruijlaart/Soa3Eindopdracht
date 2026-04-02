using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Backlog;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;
using Xunit;

namespace So3EindopdrachtTests
{
    public class BacklogTests : BaseTest
    {
        private readonly Project _project;
        private readonly ProjectMember _devMember;

        public BacklogTests() : base()
        {
            // Setup basis context
            var user = new User(1, "Dev", "123", "dev@test.nl", 1);
            _devMember = new ProjectMember(user, RoleEnum.DEVELOPER);
            _project = new Project("DevOps Project", _devMember);
        }

        // ============================================================
        // FR-1: PROJECT BACKLOG
        // ============================================================

        [Fact]
        public void ProjectBacklog_ShouldBeLinkedToProject_FR1()
        {
            // Act
            var backlog = new ProjectBacklog(_project);

            // Assert
            Assert.Equal(_project, backlog.Project);
            Assert.NotNull(backlog.backlogItems);
        }

        [Fact]
        public void ProjectBacklog_OrderItems_ShouldPrintCorrectMessage()
        {
            // Arrange
            var backlog = new ProjectBacklog(_project);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            backlog.orderItems();

            // Assert
            Assert.Contains("Backlog items have been ordered", sw.ToString());
        }

        // ============================================================
        // FR-3: SPRINT BACKLOG
        // ============================================================

        [Fact]
        public void SprintBacklog_ShouldBeLinkedToSprint_FR3()
        {
            // Arrange
            var sprint = new ReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7), _project);

            // Act
            var backlog = new SprintBacklog(sprint);

            // Assert
            Assert.Equal(sprint, backlog.Sprint);
            Assert.NotNull(backlog.backlogItems);
        }

        [Fact]
        public void SprintBacklog_OrderItems_ShouldPrintCorrectMessage()
        {
            // Arrange
            var sprint = new ReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7), _project);
            var backlog = new SprintBacklog(sprint);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                backlog.orderItems();

                // Assert
                Assert.Contains("Backlog items have been ordered", sw.ToString());
            }
        }

        // ============================================================
        // INTERFACE & POLYMORPHISM CHECK
        // ============================================================

        [Fact]
        public void BothBacklogs_ShouldImplement_IBacklog()
        {
            // Arrange
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(7), _project);

            var projectBacklog = new ProjectBacklog(_project);
            var sprintBacklog = new SprintBacklog(sprint);

            // Assert
            Assert.IsAssignableFrom<IBacklog>(projectBacklog);
            Assert.IsAssignableFrom<IBacklog>(sprintBacklog);
        }
    }
}