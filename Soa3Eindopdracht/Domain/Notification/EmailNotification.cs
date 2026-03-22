using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Notification;
public class EmailNotification : INotificationObserver
{
    private EmailService emailService = new();

    public void SendNotification(string body, string subject, ProjectMember member)
    {
        bool sendMail = emailService.SendEmail(body, subject, member.User.Email);
        if (sendMail)
        {
            Console.WriteLine($"Succesfully send email notification to: {member.User.Name}");
        }
        else
        {
            Console.WriteLine($"Something went wrong sending email notification to: {member.User.Name}");
        }
    }
}
