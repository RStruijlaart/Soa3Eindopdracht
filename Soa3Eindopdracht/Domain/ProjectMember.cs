using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;
public class ProjectMember
{
    public User User { get; set; }
    public RoleEnum Role { get; set; }
    public ProjectMember(User user, RoleEnum role)
    {
        User = user;
        Role = role;
    }
}
