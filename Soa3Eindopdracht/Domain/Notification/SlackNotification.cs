using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Notification;
public class SlackNotification : INotificationObserver
{
    private SlackApi slackApi = new();

    public string SendNotification(string body, string subject, ProjectMember member)
    {
        bool sendMail = slackApi.SendSlackMessage(body, subject, member.User.SlackId);
        if (sendMail)
        {
            return $"Succesfully send slack notifications to: {member.User.Name}";
        }
        return $"Something went wrong sending slack notification to: {member.User.Name}";
    }
}
