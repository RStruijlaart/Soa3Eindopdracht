using Soa3Eindopdracht.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;
public class ProjectMember : NotificationObservable
{
    public bool Active { get; set; }
    public User User { get; set; }
    public RoleEnum Role { get; set; }
    public ProjectMember(User user, RoleEnum role)
    {
        Active = true;
        User = user;
        Role = role;
    }

    public void SendNotification(string body, string subject)
    {
        this.SendNotification(body, subject, this);
    }
}
