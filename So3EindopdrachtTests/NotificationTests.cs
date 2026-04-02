using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Notification;
using Xunit;

namespace So3EindopdrachtTests
{
    public class NotificationTests : BaseTest
    {
        private readonly ProjectMember _projectMember;

        public NotificationTests() : base()
        {
            // Setup basis context voor elke test
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            _projectMember = new ProjectMember(user, RoleEnum.DEVELOPER);
        }

        // ============================================================
        // FR-6.1: INDIVIDUELE KANALEN (Console verificatie)
        // ============================================================

        [Fact]
        public void EmailNotification_ShouldLogSuccess_FR6_1()
        {
            // Arrange
            _projectMember.AddObserver(new EmailNotification());
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _projectMember.SendNotification("Body", "Subject");

                // Assert
                var output = sw.ToString().Trim();
                Assert.Contains($"Succesfully send email notification to: {_projectMember.User.Name}", output);
            }
        }

        [Fact]
        public void SlackNotification_ShouldLogSuccess_FR6_1()
        {
            // Arrange
            _projectMember.AddObserver(new SlackNotification());
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _projectMember.SendNotification("Body", "Subject");

                // Assert
                var output = sw.ToString().Trim();
                Assert.Contains($"Succesfully send slack notification to: {_projectMember.User.Name}", output);
            }
        }

        [Fact]
        public void SmsNotification_ShouldLogSuccess_FR6_1()
        {
            // Arrange
            _projectMember.AddObserver(new SmsNotification());
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _projectMember.SendNotification("Body", "Subject");

                // Assert
                var output = sw.ToString().Trim();
                Assert.Contains($"Succesfully send sms notification to: {_projectMember.User.Name}", output);
            }
        }

        // ============================================================
        // FR-6.2: COMBINATIES VAN KANALEN (Standaard Mocks)
        // ============================================================

        [Fact]
        public void ProjectMember_ShouldNotifyMultipleObservers_WhenMultipleChannelsAdded_FR6_2()
        {
            // Arrange
            var emailMock = new Mock<INotificationObserver>();
            var slackMock = new Mock<INotificationObserver>();

            _projectMember.AddObserver(emailMock.Object);
            _projectMember.AddObserver(slackMock.Object);

            // Act
            _projectMember.SendNotification("Het systeem is klaar", "Update");

            // Assert
            emailMock.Verify(n => n.SendNotification("Het systeem is klaar", "Update", _projectMember), Times.Once);
            slackMock.Verify(n => n.SendNotification("Het systeem is klaar", "Update", _projectMember), Times.Once);
        }

        [Fact]
        public void ProjectMember_ShouldNoLongerNotify_WhenObserverRemoved()
        {
            // Arrange
            var mock = new Mock<INotificationObserver>();
            _projectMember.AddObserver(mock.Object);
            _projectMember.DeleteObserver(mock.Object);

            // Act
            _projectMember.SendNotification("Test", "Test");

            // Assert
            mock.Verify(n => n.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ProjectMember>()), Times.Never);
        }
    }
}