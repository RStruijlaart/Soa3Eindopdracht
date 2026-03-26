using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class DoneState : IBacklogItemState
{
    private readonly BacklogItem backlogItem;

    public DoneState(BacklogItem backlogItem)
    {
        this.backlogItem = backlogItem;
    }

    public void SetDoing()
    {
        Invalid("Backlog item kan niet van \"Done\" gehaalt worden");
    }

    public void SetDone()
    {
        Invalid("Backlog item kan niet van \"Done\" gehaalt worden");
    }

    public void SetReadyForTesting()
    {
        Invalid("Backlog item kan niet van \"Done\" gehaalt worden");
    }

    public void SetTested()
    {
        Invalid("Backlog item kan niet van \"Done\" gehaalt worden");
    }

    public void SetTesting()
    {
        Invalid("Backlog item kan niet van \"Done\" gehaalt worden");
    }

    public void setTodo()
    {
        Invalid("Backlog item kan niet van \"Done\" gehaalt worden");
    }

    private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
}
