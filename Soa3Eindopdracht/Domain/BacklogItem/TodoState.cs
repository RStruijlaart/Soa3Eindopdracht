using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class TodoState : IBacklogItemState
{
    private readonly BacklogItem backlogItem;
    public TodoState(BacklogItem backlogitem)
    {
        this.backlogItem = backlogitem;
    }
    public void SetDoing()
    {
        this.backlogItem.SetState(new DoingState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is nu in \"Doing\" gezet");
    }

    public void SetDone()
    {
        Invalid("BacklogItem kan niet van \"Todo\" naar \"Done\" gezet worden");
    }

    public void SetReadyForTesting()
    {
        Invalid("Backlog item kan niet van \"Todo\" naar \"Ready for Testing\" gezet worden");
    }

    public void SetTested()
    {
        Invalid("Backlog item kan niet van \"Todo\" naar \"Tested\" gezet worden");
    }

    public void SetTesting()
    {
        Invalid("Backlog item kan niet van \"Todo\" naar \"Testing\" gezet worden");
    }

    public void setTodo()
    {
        Invalid("Backlog item staat al op \"Todo\" naar \"Todo\"");
    }

    private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
}
