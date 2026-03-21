using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public interface IBacklogItemState
{
    public void setTodo();
    public void SetDoing();
    public void SetReadyForTesting();
    public void SetTesting();
    public void SetTested();
    public void SetDone();
}
