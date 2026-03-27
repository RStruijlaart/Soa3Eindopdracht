using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class Activity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectMember ProjectMember { get; set; }
    public bool Finished { get; set; }
    public Activity(string name, string description, ProjectMember member)
    {
        Name = name;
        Description = description;
        ProjectMember = member;
        Finished = false;
    }
}
