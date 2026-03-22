using Soa3Eindopdracht.Domain.BacklogItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Comment;
public abstract class Comment
{
    public string Body { get; set; }
    public ProjectMember Author { get; set; }
    public BacklogItem.BacklogItem BacklogItem { get; set; }
    protected Comment(string body, ProjectMember author, BacklogItem.BacklogItem backlogItem)
    {
        this.Body = body;
        this.Author = author;
        this.BacklogItem = backlogItem;
    }

    public void SendNotification()
    {
        List<Activity> activities = BacklogItem.GetActivities();
        if (activities.Count != 0)
        {
            foreach (Activity activity in BacklogItem.GetActivities())
            {
                activity.ProjectMember?.SendNotification(Body, $"Nieuwe comment voor PBI: {BacklogItem.Name}");
            }
        }
        BacklogItem.ProjectMember?.SendNotification(Body, $"Nieuwe comment voor PBI: {BacklogItem.Name}");
    }

    public void Operation()
    {
        Console.WriteLine("Comment is geplaatst");
    }
}
