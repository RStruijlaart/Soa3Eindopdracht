using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Notification;
public class SmsNotification : INotificationObserver
{
    private SmsService smsService = new();

    public string SendNotification(string body, string subject, ProjectMember member)
    {
        bool sendMail = smsService.SendSms(body, subject, member.User.PhoneNumber);
        if (sendMail)
        {
            return $"Succesfully send sms notifications to: {member.User.Name}";
        }
        return $"Something went wrong sending sms notification to: {member.User.Name}";
    }
}
