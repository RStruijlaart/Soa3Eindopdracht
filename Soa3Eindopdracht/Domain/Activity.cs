using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;
public class Activity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ProjectMember { get; set; }
    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
