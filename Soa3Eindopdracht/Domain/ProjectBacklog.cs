using Soa3Eindopdracht.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;
public class ProjectBacklog : IBacklog
{
    public List<BacklogItem.BacklogItem> backlogItems = [];
    public Project Project { get; set; }

    public ProjectBacklog(Project project)
    {
        this.Project = project;
    }

    public void orderItems()
    {
        Console.WriteLine("Backlog items have been ordered");
    }
}
