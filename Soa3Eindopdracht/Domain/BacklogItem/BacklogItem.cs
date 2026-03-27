using Soa3Eindopdracht.Domain.Backlog;
using Soa3Eindopdracht.Domain.Comment;
using Soa3Eindopdracht.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class BacklogItem
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectMember? ProjectMember { get; set; }
    public IBacklog Backlog {  get; set; }
    private IBacklogItemState state { get; set; }
    public List<Comment.Comment> comments = [];
    public List<Activity> activities = [];

    public BacklogItem(string name, string description, IBacklog backlog)
    {
        Name = name;
        Description = description;
        Backlog = backlog;
        state = new TodoState(this);
    }

    public void AddActivity(Activity activity)
    {
        activities.Add(activity);
    }

    public void DeleteActivity(Activity activity)
    {
        activities.Remove(activity);
    }

    public List<Activity> GetActivities()
    {
        return activities;
    }

    public void SendNotificationToTesters()
    {
        Project? project = GetProject();
        if(project == null)
        {
            return;
        }
        List<ProjectMember> projectMembers = project.Testers;

        foreach (ProjectMember member in projectMembers)
        {
            member.SendNotification($"Backlog item: {Name} is ready for testing!", "Backlog item ready for testing");
        }
    }

    public void SendNotificationToScumMaster()
    {
        Project? project = GetProject();
        if (project == null)
        {
            return;
        }
        ProjectMember? scrumMaster = project.ScrumMaster;

        if(scrumMaster != null)
        {
            scrumMaster.SendNotification($"Backlog item: {Name} is done and ready for review!", "Backlog item ready for review");
        }
    }

    private Project? GetProject()
    {
        return Backlog switch
        {
            SprintBacklog sprintBacklog => sprintBacklog.Sprint.Project,
            ProjectBacklog sprintBacklog => sprintBacklog.Project,
            _ => null
        };
    }

    public void SetState(IBacklogItemState state)
    {
        this.state = state;
    }

    public void SetDoing()
    {
        this.state.SetDoing();
    }

    public void SetDone()
    {
        this.state.SetDone();
    }

    public void SetReadyForTesting()
    {
        this.state.SetReadyForTesting();
    }

    public void SetTested()
    {
        this.state.SetTested();
    }

    public void SetTesting()
    {
        this.state.SetTesting();
    }

    public void setTodo()
    {
        this.state.setTodo();
    }

    public IBacklogItemState CurrentState => state;
}
