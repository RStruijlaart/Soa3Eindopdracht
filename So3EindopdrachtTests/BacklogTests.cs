using Soa3Eindopdracht.Domain.Backlog;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain;

namespace So3EindopdrachtTests
{
    public class BacklogTests
    {
        private Project CreateProject()
        {
            var user = new User(1, "Tester", "123", "test@test.com", 1);
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);
            return new Project("Test project", member);
        }

        [Fact]
        public void SprintBacklog_OrderItems_PrintsMessage()
        {
            var project = CreateProject();
            var sprint = new MockSprint(project);
            var backlog = new SprintBacklog(sprint);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlog.orderItems();

            var output = sw.ToString().Trim();
            Assert.Equal("Backlog items have been ordered", output);
        }
    }
}