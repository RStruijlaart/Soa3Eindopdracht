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

    public string SendNotification(string body, string subject, List<ProjectMember> projectMemeber)
    {
        StringBuilder sb = new StringBuilder("Succesfully send slack notifications to the following members: ");
        int counter = 1;
        foreach (ProjectMember member in projectMemeber)
        {
            bool sendMail = slackApi.SendSlackMessage(body, subject, member.User.SlackId);
            if (sendMail)
            {
                sb.Append($"{member.User.Name}");
                if (counter != projectMemeber.Count())
                {
                    sb.Append(", ");
                }
            }
            counter++;
        }
        return sb.ToString();
    }
}
