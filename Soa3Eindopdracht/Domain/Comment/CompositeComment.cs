using Soa3Eindopdracht.Domain.BacklogItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Comment;
public abstract class CompositeComment : Comment
{
    private List<Comment> children = [];
    private CompositeComment? parent;
    protected CompositeComment(string body, ProjectMember author, BacklogItem.BacklogItem backlogItem, CompositeComment parent) : base(body, author, backlogItem)
    {
        this.parent = parent;
    }

    protected override void SendNotification(List<ProjectMember> recipients)
    {
        base.SendNotification(GetParents(this));
    }

    public void AddChild(Comment comment)
    {
        children.Add(comment);
    }

    public void RemoveChild(Comment comment)
    {
        children.Remove(comment);
    }

    public List<Comment> GetChildren(Comment comment)
    {
        return children;
    }

    private List<ProjectMember> GetParents(CompositeComment comment)
    {
        List<ProjectMember> parents = [];
        if (comment.parent != null)
        {
            parents.Add(comment.parent.Author);
            GetParents(comment.parent);
        }
        return parents; 
    }
}
