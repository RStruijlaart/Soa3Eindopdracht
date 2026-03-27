using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Notification;
using Soa3Eindopdracht.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So3EindopdrachtTests
{
    public class NotificationTests
    {
        [Fact]
        public void ObserverSendNotification_EmailNotification_Valid()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            projectMember.AddObserver(new EmailNotification());

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            projectMember.SendNotification("TestBody", "TestSubject");

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal($"Succesfully send email notification to: {projectMember.User.Name}", output);
        }

        [Fact]
        public void ObserverSendNotification_SlackNotification_Valid()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            projectMember.AddObserver(new SlackNotification());

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            projectMember.SendNotification("TestBody", "TestSubject");

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal($"Succesfully send slack notification to: {projectMember.User.Name}", output);
        }

        [Fact]
        public void ObserverSendNotification_SmsNotification_Valid()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            projectMember.AddObserver(new SmsNotification());

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            projectMember.SendNotification("TestBody", "TestSubject");

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal($"Succesfully send sms notification to: {projectMember.User.Name}", output);
        }
    }
}
