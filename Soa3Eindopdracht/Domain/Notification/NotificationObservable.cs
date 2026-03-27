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

    protected void SendNotification(string body, string subject, ProjectMember recipient)
    {
        foreach (INotificationObserver channel in channels)
        {
            channel.SendNotification(body, subject, recipient);
        }
    }
}
