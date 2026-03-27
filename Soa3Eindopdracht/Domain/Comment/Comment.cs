using Soa3Eindopdracht.Domain.BacklogItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        SendNotification([]);
    }

    protected virtual void SendNotification(List<ProjectMember> recipients)
    {
        recipients.AddRange(getAuthors());
        List<ProjectMember> noDupes = recipients.Distinct().ToList();

        foreach (ProjectMember member in noDupes)
        {
            member.SendNotification(Body, $"Nieuwe comment voor PBI: {BacklogItem.Name}");
        }
    }

    private List<ProjectMember> getAuthors()
    {
        List<ProjectMember> authors = [];
        List<Activity> activities = BacklogItem.GetActivities();
        if (activities.Count != 0)
        {
            foreach (Activity activity in activities)
            {
                if (activity.ProjectMember != null)
                {
                    authors.Add(activity.ProjectMember);
                }
            }
        }
        if (BacklogItem.ProjectMember != null)
        {
            authors.Add(BacklogItem.ProjectMember);
        }

        return authors;
    }

    public void Operation()
    {
        Console.WriteLine("Comment is geplaatst");
    }
}
