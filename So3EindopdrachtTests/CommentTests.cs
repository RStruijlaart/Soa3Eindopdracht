using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Comment;
using Soa3Eindopdracht.Domain.Notification;
using Soa3Eindopdracht.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So3EindopdrachtTests
{
    public class CommentTests
    {
        [Fact]
        public void SimpleCommentSendNotification_WithoutActivities_Valid()
        {
            // Arrange
            User author = new (1, "Author", "0123456789", "test@test.nl", 1);
            User member1 = new (3, "Member1", "416895231", "test@test.nl", 3);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var projectMember1 = new ProjectMember(member1, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();

            projectMember1.AddObserver(emailMock.Object);
            projectMember1.AddObserver(smsMock.Object);

            Project project = new ("TestProject", authorMember);
            ProjectBacklog backlog = new (project);
            BacklogItem backlogItem = new ("TestBacklogItem", "TestDescription", backlog);
            backlogItem.ProjectMember = projectMember1;

            // Act
            SimpleComment simpleComment = new ("CommentBody", authorMember, backlogItem);

            // Assert
            emailMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);
        }

        [Fact]
        public void SimpleCommentSendNotification_WithActivities_Valid()
        {
            // Arrange
            User author = new (1, "Author", "0123456789", "test@test.nl", 1);
            User member1 = new (3, "Member1", "416895231", "test@test.nl", 3);

            var authorMember = new ProjectMember(author, RoleEnum.DEVELOPER);
            var projectMember1 = new ProjectMember(member1, RoleEnum.DEVELOPER);

            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();
            var slackMock = new Mock<INotificationObserver>();

            authorMember.AddObserver(slackMock.Object);
            authorMember.AddObserver(smsMock.Object);
            projectMember1.AddObserver(emailMock.Object);
            projectMember1.AddObserver(smsMock.Object);

            Project project = new("TestProject", authorMember);
            ProjectBacklog backlog = new(project);
            BacklogItem backlogItem = new("TestBacklogItem", "TestDescription", backlog);
            Activity activity = new("Activity", "ActivityDescription", authorMember);

            backlogItem.AddActivity(activity);
            backlogItem.ProjectMember = projectMember1;

            // Act
            SimpleComment simpleComment = new("CommentBody", authorMember, backlogItem);

            // Assert
            emailMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {backlogItem.Name}", authorMember), Times.Once);
            slackMock.Verify(n => n.SendNotification("CommentBody", $"Nieuwe comment voor PBI: {backlogItem.Name}", authorMember), Times.Once);
        }

        [Fact]
        public void CommentCompositeSendNotification_WithoutActivities_Valid()
        {
            // Arrange
            User author = new (1, "Author", "0123456789", "test@test.nl", 1);
            User member1 = new(2, "Member1", "416895231", "test@test.nl", 2);
            User member2 = new (3, "Member2", "416895231", "test@test.nl", 3);

            ProjectMember authorMember = new (author, RoleEnum.DEVELOPER);
            ProjectMember projectMember1 = new(member1, RoleEnum.DEVELOPER);
            ProjectMember projectMember2 = new (member2, RoleEnum.DEVELOPER);

            // 👇 mocks i.p.v. echte notificaties
            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();
            var slackMock = new Mock<INotificationObserver>();

            projectMember1.AddObserver(emailMock.Object);
            projectMember1.AddObserver(slackMock.Object);
            projectMember2.AddObserver(emailMock.Object);
            projectMember2.AddObserver(smsMock.Object);
            authorMember.AddObserver(slackMock.Object);
            authorMember.AddObserver(smsMock.Object);

            Project project = new ("TestProject", authorMember);
            ProjectBacklog backlog = new (project);
            BacklogItem backlogItem = new("TestBacklogItem", "TestDescription", backlog);
            backlogItem.ProjectMember = projectMember2;

            // Act
            CommentComposite CommentComposite1 = new ("CommentBody1", authorMember, backlogItem, null);
            CommentComposite CommentComposite2 = new("CommentBody2", projectMember1, backlogItem, CommentComposite1);

            // Assert
            emailMock.Verify(n => n.SendNotification("CommentBody1", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody1", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);

            emailMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);

            smsMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", authorMember), Times.Once);
            slackMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", authorMember), Times.Once);
        }

        [Fact]
        public void CommentCompositeSendNotification_WithActivities_Valid()
        {
            // Arrange
            User author = new(1, "Author", "0123456789", "test@test.nl", 1);
            User member1 = new(2, "Member1", "416895231", "test@test.nl", 2);
            User member2 = new(3, "Member2", "416895231", "test@test.nl", 3);

            ProjectMember authorMember = new(author, RoleEnum.DEVELOPER);
            ProjectMember projectMember1 = new(member1, RoleEnum.DEVELOPER);
            ProjectMember projectMember2 = new(member2, RoleEnum.DEVELOPER);

            // 👇 mocks i.p.v. echte notificaties
            var emailMock = new Mock<INotificationObserver>();
            var smsMock = new Mock<INotificationObserver>();
            var slackMock = new Mock<INotificationObserver>();

            projectMember1.AddObserver(emailMock.Object);
            projectMember1.AddObserver(slackMock.Object);
            projectMember2.AddObserver(emailMock.Object);
            projectMember2.AddObserver(smsMock.Object);
            authorMember.AddObserver(slackMock.Object);
            authorMember.AddObserver(smsMock.Object);

            Project project = new("TestProject", authorMember);
            ProjectBacklog backlog = new(project);
            BacklogItem backlogItem = new("TestBacklogItem", "TestDescription", backlog);
            Activity activity = new("Activity", "ActivityDescription", projectMember1);

            backlogItem.AddActivity(activity);
            backlogItem.ProjectMember = projectMember2;

            // Act
            CommentComposite CommentComposite1 = new("CommentBody1", authorMember, backlogItem, null);
            CommentComposite CommentComposite2 = new("CommentBody2", projectMember1, backlogItem, CommentComposite1);

            // Assert
            emailMock.Verify(n => n.SendNotification("CommentBody1", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody1", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);

            emailMock.Verify(n => n.SendNotification("CommentBody1", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);
            slackMock.Verify(n => n.SendNotification("CommentBody1", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);

            emailMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);
            smsMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember2), Times.Once);

            emailMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);
            slackMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", projectMember1), Times.Once);

            smsMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", authorMember), Times.Once);
            slackMock.Verify(n => n.SendNotification("CommentBody2", $"Nieuwe comment voor PBI: {backlogItem.Name}", authorMember), Times.Once);
        }
    }
}
