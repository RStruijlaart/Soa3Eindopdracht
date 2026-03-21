using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Notification;
public interface INotificationObserver
{
    public string SendNotification(string body, string subject, List<ProjectMember> projectMemeber);
}
