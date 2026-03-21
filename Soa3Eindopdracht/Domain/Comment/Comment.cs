using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Comment;
public abstract class Comment
{
    public string body { get; set; }
    public ProjectMember author { get; set; }
    protected Comment(string body, ProjectMember author)
    {
        this.body = body;
        this.author = author;
    }

    public bool SendNotification()
    {
        throw new NotImplementedException();
    }

    public bool Operation()
    {
        throw new NotImplementedException();
    }
}
