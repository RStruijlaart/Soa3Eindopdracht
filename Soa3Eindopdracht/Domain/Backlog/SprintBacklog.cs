using Soa3Eindopdracht.Domain.Sprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Backlog;
public class SprintBacklog : IBacklog
{
    public List<BacklogItem.BacklogItem> backlogItems = [];
    public Sprint Sprint { get; set; }

    public SprintBacklog(Sprint sprint)
    {
        Sprint = sprint;
    }

    public void orderItems()
    {
        Console.WriteLine("Backlog items have been ordered");
    }
}
