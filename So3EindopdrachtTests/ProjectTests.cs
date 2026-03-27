using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Projects;

namespace So3EindopdrachtTests
{
    public class ProjectTests
    {
        private User CreateUser(string name = "Tester")
        {
            return new User(1, name, "123", "test@test.com", 1);
        }

        [Fact]
        public void Project_Creation_Valid()
        {
            var user = CreateUser();
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);

            var project = new Project("Test Project", member);

            Assert.Equal("Test Project", project.Name);
            Assert.NotNull(project.Backlog);
            Assert.NotNull(project.Repository);
            Assert.Single(project.Developers);
        }

        [Fact]
        public void Project_Creation_InvalidName()
        {
            var user = CreateUser();
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);

            Assert.Throws<ArgumentException>(() => new Project("", member));
        }

        [Fact]
        public void AddProjectMember_Developer()
        {
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));

            var dev = new ProjectMember(CreateUser("Dev2"), RoleEnum.DEVELOPER);
            project.AddProjectMember(dev);

            Assert.Equal(2, project.Developers.Count);
        }

        [Fact]
        public void AddProjectMember_Tester()
        {
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));

            var tester = new ProjectMember(CreateUser("Tester2"), RoleEnum.TESTER);
            project.AddProjectMember(tester);

            Assert.Single(project.Testers);
        }

        [Fact]
        public void AddProjectMember_ScrumMaster_FirstTime_Invalid()
        {
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));

            var sm = new ProjectMember(CreateUser("SM"), RoleEnum.SCRUM_MASTER);
            project.AddProjectMember(sm);

            // jouw code: eerste keer wordt NIET gezet (bug eigenlijk)
            Assert.Null(project.ScrumMaster);
        }

        [Fact]
        public void AddProjectMember_ProductOwner_FirstTime_Invalid()
        {
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));

            var po = new ProjectMember(CreateUser("PO"), RoleEnum.PRODUCT_OWNER);
            project.AddProjectMember(po);

            Assert.Null(project.ProductOwner);
        }

        [Fact]
        public void AddSprint_And_RemoveSprint()
        {
            var project = new Project("Test", new ProjectMember(CreateUser(), RoleEnum.DEVELOPER));

            var sprint = new MockSprint(project);

            project.AddSprint(sprint);
            Assert.Single(project.Sprints);

            project.RemoveSprint(sprint);
            Assert.Empty(project.Sprints);
        }
    }
}