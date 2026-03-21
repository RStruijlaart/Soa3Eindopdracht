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

    public string SendNotification(string body, string subject, ProjectMember member)
    {
        bool sendMail = emailService.SendEmail(body, subject, member.User.Email);
        if (sendMail)
        {
            return $"Succesfully send email notifications to: {member.User.Name}";
        }
        return $"Something went wrong sending email notification to: {member.User.Name}";
    }
}
