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

    public void SendNotification(string body, string subject, ProjectMember member)
    {
        bool sendMail = smsService.SendSms(body, subject, member.User.PhoneNumber);
        if (sendMail)
        {
            Console.WriteLine($"Succesfully send sms notification to: {member.User.Name}");
        }
        else
        {
            Console.WriteLine($"Something went wrong sending sms notification to: {member.User.Name}");
        }
    }
}
