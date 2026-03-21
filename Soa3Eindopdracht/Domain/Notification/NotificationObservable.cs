using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Notification;
public abstract class NotificationObservable
{
    private List<INotificationObserver> channels = [];

    public void AddObserver(INotificationObserver observer)
    {
        channels.Add(observer);
    }

    public void DeleteObserver(INotificationObserver observer)
    {
        channels.Remove(observer);
    }

    public void SendNotification(string body, string subject, List<ProjectMember> recipients)
    {
        foreach (var channel in channels)
        {
            channel.SendNotification(body, subject, recipients);
        }
    }
}
