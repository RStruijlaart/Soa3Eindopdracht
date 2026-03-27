using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.Comment;
public class CommentComposite : CompositeComment
{
    public CommentComposite(string body, ProjectMember author, BacklogItem.BacklogItem backlogItem, CompositeComment parent) : base(body, author, backlogItem, parent)
    {
    }
}
