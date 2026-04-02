using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;
using Xunit;

namespace So3EindopdrachtTests
{
    public class ProjectTests : BaseTest
    {
        public ProjectTests() : base()
        {
        }

        private User CreateUser(string name = "Tester", int id = 1)
        {
            return new User(id, name, "123", "test@test.com", id);
        }

        // ============================================================
        // FR-1: PROJECT CREATIE & BACKLOG
        // ============================================================

        [Fact]
        public void Project_Creation_ShouldInitializeCorrectly_FR1()
        {
            // Arrange
            var user = CreateUser();
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);

            // Act
            var project = new Project("Avans DevOps", member);

            // Assert
            Assert.Equal("Avans DevOps", project.Name);
            Assert.NotNull(project.Backlog);
            Assert.NotNull(project.Repository);
            Assert.Contains(member, project.Developers);
        }

        [Fact]
        public void Project_ShouldThrowException_OnEmptyName_FR1_Voorwaarde()
        {
            // Arrange
            var member = new ProjectMember(CreateUser(), RoleEnum.DEVELOPER);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Project("", member));
        }

        // ============================================================
        // FR-10: ROLLEN & AUTORISATIE
        // ============================================================

        [Fact]
        public void Project_ShouldAssignScrumMaster_WhenRoleMatches_FR10_4()
        {
            // Arrange
            var creator = new ProjectMember(CreateUser("Creator"), RoleEnum.DEVELOPER);
            var project = new Project("Test", creator);
            var smMember = new ProjectMember(CreateUser("SM"), RoleEnum.SCRUM_MASTER);

            // Act
            project.AddProjectMember(smMember);

            // Assert
            // Let op: Je domeincode heeft hier een bug (if ScrumMaster != null). 
            // Deze test dwingt af dat de SM ook echt gezet wordt!
            Assert.Equal(smMember, project.ScrumMaster);
        }

        [Fact]
        public void Project_ShouldAssignProductOwner_WhenRoleMatches_FR10_4()
        {
            // Arrange
            var creator = new ProjectMember(CreateUser("Creator"), RoleEnum.DEVELOPER);
            var project = new Project("Test", creator);
            var poMember = new ProjectMember(CreateUser("PO"), RoleEnum.PRODUCT_OWNER);

            // Act
            project.AddProjectMember(poMember);

            // Assert
            // Ook hier dwingt de test de correcte werking van de bug in je domeincode af.
            Assert.Equal(poMember, project.ProductOwner);
        }

        [Fact]
        public void Project_ShouldSupportMultipleTesters_FR10_4()
        {
            // Arrange
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));
            var tester1 = new ProjectMember(CreateUser("T1", 2), RoleEnum.TESTER);
            var tester2 = new ProjectMember(CreateUser("T2", 3), RoleEnum.TESTER);

            // Act
            project.AddProjectMember(tester1);
            project.AddProjectMember(tester2);

            // Assert
            Assert.Equal(2, project.Testers.Count);
            Assert.Contains(tester1, project.Testers);
            Assert.Contains(tester2, project.Testers);
        }

        // ============================================================
        // SPRINT MANAGEMENT
        // ============================================================

        [Fact]
        public void Project_ShouldManageSprintsCorrectly()
        {
            // Arrange
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));
            // We gebruiken een echte ReviewSprint ipv een mock voor een pure domein test
            var sprint = new ReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7), project);

            // Act
            project.AddSprint(sprint);
            Assert.Single(project.Sprints);

            project.RemoveSprint(sprint);

            // Assert
            Assert.Empty(project.Sprints);
        }
    }
}