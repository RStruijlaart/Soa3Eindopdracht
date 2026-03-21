using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Comment;
public abstract class CompositeComment : Comment
{
    private List<Comment> children = [];
    protected CompositeComment(string body, ProjectMember author) : base(body, author)
    {
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
}
