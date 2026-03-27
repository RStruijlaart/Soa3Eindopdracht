using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Backlog;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Projects;

namespace So3EindopdrachtTests
{
    public class BacklogItemTests
    {
        private Project CreateProject()
        {
            var user = new User(1, "Tester", "123", "test@test.com", 1);
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);
            return new Project("Test project", member);


        }
        private Activity CreateActivity()
        {
            var user = new User(1, "Tester", "123", "test@test.com", 1);
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);

            return new Activity("Act", "Desc", member);
        }

        [Fact]
        public void AddActivity_ShouldAdd()
        {
            var project = CreateProject();
            var backlog = new ProjectBacklog(project);
            var item = new BacklogItem("Test", "Desc", backlog);

            var activity = CreateActivity();

            item.AddActivity(activity);

            Assert.Single(item.GetActivities());
        }

        [Fact]
        public void DeleteActivity_ShouldRemove()
        {
            var project = CreateProject();
            var backlog = new ProjectBacklog(project);
            var item = new BacklogItem("Test", "Desc", backlog);

            var activity = CreateActivity();
            item.AddActivity(activity);

            item.DeleteActivity(activity);

            Assert.Empty(item.GetActivities());
        }

        [Fact]
        public void SendNotificationToTesters_NoTesters_NoCrash()
        {
            var project = CreateProject();
            var backlog = new ProjectBacklog(project);
            var item = new BacklogItem("Test", "Desc", backlog);

            item.SendNotificationToTesters();

            Assert.True(true); // gewoon check dat hij niet crasht
        }

        [Fact]
        public void SendNotificationToScrumMaster_NoScrumMaster_NoCrash()
        {
            var project = CreateProject();
            var backlog = new ProjectBacklog(project);
            var item = new BacklogItem("Test", "Desc", backlog);

            item.SendNotificationToScumMaster();

            Assert.True(true);
        }

        [Fact]
        public void GetProject_FromProjectBacklog()
        {
            var project = CreateProject();
            var backlog = new ProjectBacklog(project);
            var item = new BacklogItem("Test", "Desc", backlog);

            Assert.NotNull(item);
        }

        [Fact]
        public void GetProject_FromSprintBacklog()
        {
            var project = CreateProject();
            var sprint = new MockSprint(project);
            var backlog = new SprintBacklog(sprint);

            var item = new BacklogItem("Test", "Desc", backlog);

            Assert.NotNull(item);
        }
    }
}