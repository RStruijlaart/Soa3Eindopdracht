using Soa3Eindopdracht.Domain.Sprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;
public class SprintBacklog : IBacklog
{
    public List<BacklogItem.BacklogItem> backlogItems = [];
    public Sprint Sprint { get; set; }

    public SprintBacklog(Sprint sprint)
    {
        this.Sprint = sprint;
    }

    public void orderItems()
    {
        Console.WriteLine("Backlog items have been ordered");
    }
}
