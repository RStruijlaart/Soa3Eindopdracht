using Soa3Eindopdracht.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain;
public class BacklogItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ProjectMember { get; set; }
    public List<Comment.Comment> comments = [];
    private List<Activity> activities = [];

    public BacklogItem(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddActivity(Activity activity)
    {
        activities.Add(activity);
    }

    public void DeleteActivity(Activity activity)
    {
        activities.Remove(activity);
    }

    public bool SendNotificationToTetsters()
    {
        throw new NotImplementedException();
    }

    public bool SendNotificationToScumMaster()
    {
        throw new NotImplementedException();
    }
}
