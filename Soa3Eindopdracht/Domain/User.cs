using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int SlackId { get; set; }

    public User(int id, string name, string phoneNumber, string email, int slackId)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        SlackId = slackId;
    }
}

