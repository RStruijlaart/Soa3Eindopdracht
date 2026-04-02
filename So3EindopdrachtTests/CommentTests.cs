using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Comment;
using Soa3Eindopdracht.Domain.Notification;
using Soa3Eindopdracht.Domain.Projects;
using Xunit;

namespace So3EindopdrachtTests
{
    public class CommentTests : BaseTest
    {
        public CommentTests() : base() 
        {
        }
        // ========================
        // SIMPLE COMMENT
        // ========================

        [Fact]
        public void SimpleComment_ShouldNotify_AssignedDeveloper_WhenNoActivities()
        {
            // Arrange
            var author = new User(1, "Author", "0123456789", "test@test.nl", 1);
            var member = new User(2, "Dev", "123", "dev@test.nl", 2);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var devMember = new ProjectMember(member, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();

            devMember.AddObserver(emailMock.Object);
            devMember.AddObserver(smsMock.Object);

            var project = new Project("Test", authorMember);
            var backlog = new ProjectBacklog(project);
            var pbi = new BacklogItem("PBI", "Desc", backlog);
            pbi.ProjectMember = devMember;

            // Act
            new SimpleComment("CommentBody", authorMember, pbi);

            // Assert
            emailMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {pbi.Name}", devMember), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {pbi.Name}", devMember), Times.Once);
        }

        [Fact]
        public void SimpleComment_ShouldAlsoNotify_Author_WhenActivitiesExist()
        {
            // Arrange
            var author = new User(1, "Author", "0123456789", "test@test.nl", 1);
            var member = new User(2, "Dev", "123", "dev@test.nl", 2);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var devMember = new ProjectMember(member, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();
            var slackMock = new Mock<INotificationObserver>();

            authorMember.AddObserver(slackMock.Object);
            authorMember.AddObserver(smsMock.Object);
            devMember.AddObserver(emailMock.Object);
            devMember.AddObserver(smsMock.Object);

            var project = new Project("Test", authorMember);
            var backlog = new ProjectBacklog(project);
            var pbi = new BacklogItem("PBI", "Desc", backlog);

            var activity = new Activity("Act", "Desc", authorMember);
            pbi.AddActivity(activity);
            pbi.ProjectMember = devMember;

            // Act
            new SimpleComment("CommentBody", authorMember, pbi);

            // Assert
            emailMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {pbi.Name}", devMember), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {pbi.Name}", devMember), Times.Once);

            smsMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {pbi.Name}", authorMember), Times.Once);
            slackMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {pbi.Name}", authorMember), Times.Once);
        }

        // ========================
        // COMMENT COMPOSITE
        // ========================

        [Fact]
        public void CommentComposite_ShouldNotify_AssignedDeveloper_ForEachComment()
        {
            // Arrange
            var author = new User(1, "Author", "0123456789", "test@test.nl", 1);
            var dev1 = new User(2, "Dev1", "123", "dev@test.nl", 2);
            var dev2 = new User(3, "Dev2", "123", "dev@test.nl", 3);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var member1 = new ProjectMember(dev1, RoleEnum.DEVELOPER);
            var member2 = new ProjectMember(dev2, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();

            member2.AddObserver(emailMock.Object);
            member2.AddObserver(smsMock.Object);

            var project = new Project("Test", authorMember);
            var backlog = new ProjectBacklog(project);
            var pbi = new BacklogItem("PBI", "Desc", backlog);
            pbi.ProjectMember = member2;

            // Act
            var c1 = new CommentComposite("C1", authorMember, pbi, null);
            var c2 = new CommentComposite("C2", member1, pbi, c1);

            // Assert
            emailMock.Verify(n => n.SendNotification("C1", $"Nieuwe comment voor PBI: {pbi.Name}", member2), Times.Once);
            smsMock.Verify(n => n.SendNotification("C1", $"Nieuwe comment voor PBI: {pbi.Name}", member2), Times.Once);

            emailMock.Verify(n => n.SendNotification("C2", $"Nieuwe comment voor PBI: {pbi.Name}", member2), Times.Once);
            smsMock.Verify(n => n.SendNotification("C2", $"Nieuwe comment voor PBI: {pbi.Name}", member2), Times.Once);
        }

        [Fact]
        public void CommentComposite_ShouldNotify_ActivityDevelopers_WhenActivitiesExist()
        {
            // Arrange
            var author = new User(1, "Author", "0123456789", "test@test.nl", 1);
            var dev1 = new User(2, "Dev1", "123", "dev@test.nl", 2);
            var dev2 = new User(3, "Dev2", "123", "dev@test.nl", 3);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var member1 = new ProjectMember(dev1, RoleEnum.DEVELOPER);
            var member2 = new ProjectMember(dev2, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            var slackMock = new Mock<INotificationObserver>();

            member1.AddObserver(emailMock.Object);
            member1.AddObserver(slackMock.Object);
            member2.AddObserver(emailMock.Object);

            var project = new Project("Test", authorMember);
            var backlog = new ProjectBacklog(project);
            var pbi = new BacklogItem("PBI", "Desc", backlog);

            var activity = new Activity("Act", "Desc", member1);
            pbi.AddActivity(activity);
            pbi.ProjectMember = member2;

            // Act
            var c1 = new CommentComposite("C1", authorMember, pbi, null);
            var c2 = new CommentComposite("C2", member1, pbi, c1);

            // Assert
            emailMock.Verify(n => n.SendNotification("C1", $"Nieuwe comment voor PBI: {pbi.Name}", member1), Times.Once);
            slackMock.Verify(n => n.SendNotification("C1", $"Nieuwe comment voor PBI: {pbi.Name}", member1), Times.Once);

            emailMock.Verify(n => n.SendNotification("C2", $"Nieuwe comment voor PBI: {pbi.Name}", member1), Times.Once);
            slackMock.Verify(n => n.SendNotification("C2", $"Nieuwe comment voor PBI: {pbi.Name}", member1), Times.Once);
        }

        // ========================
        // EDGE CASES
        // ========================

        [Fact]
        public void Comment_ShouldNotCrash_WhenNoObservers()
        {
            // Arrange
            var author = new User(1, "Author", "0123456789", "test@test.nl", 1);
            var dev = new User(2, "Dev", "123", "dev@test.nl", 2);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var devMember = new ProjectMember(dev, RoleEnum.DEVELOPER);

            var project = new Project("Test", authorMember);
            var backlog = new ProjectBacklog(project);
            var pbi = new BacklogItem("PBI", "Desc", backlog);
            pbi.ProjectMember = devMember;

            // Act & Assert
            new SimpleComment("CommentBody", authorMember, pbi);
        }

        [Fact]
        public void MultipleComments_ShouldTriggerMultipleNotifications()
        {
            // Arrange
            var author = new User(1, "Author", "0123456789", "test@test.nl", 1);
            var dev = new User(2, "Dev", "123", "dev@test.nl", 2);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var devMember = new ProjectMember(dev, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            devMember.AddObserver(emailMock.Object);

            var project = new Project("Test", authorMember);
            var backlog = new ProjectBacklog(project);
            var pbi = new BacklogItem("PBI", "Desc", backlog);
            pbi.ProjectMember = devMember;

            // Act
            new SimpleComment("C1", authorMember, pbi);
            new SimpleComment("C2", authorMember, pbi);

            // Assert
            emailMock.Verify(n => n.SendNotification("C1", $"Nieuwe comment voor PBI: {pbi.Name}", devMember), Times.Once);
            emailMock.Verify(n => n.SendNotification("C2", $"Nieuwe comment voor PBI: {pbi.Name}", devMember), Times.Once);
        }
    }
}