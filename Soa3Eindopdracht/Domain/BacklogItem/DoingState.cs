using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class DoingState : IBacklogItemState
{
    private readonly BacklogItem backlogItem;
    public DoingState(BacklogItem backlogItem)
    {
        this.backlogItem = backlogItem;
    }

    public void SetDoing()
    {
        Invalid("BacklogItem staat al op \"Doing\" naar \"Doing\"");
    }

    public void SetDone()
    {
        Invalid("BacklogItem kan niet van \"Doing\" naar \"Done\" gezet worden");
    }

    public void SetReadyForTesting()
    {
        this.backlogItem.SetState(new ReadyForTestingState(backlogItem));
        this.backlogItem.SendNotificationToTesters();
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Ready for Testing\" gezet");
    }

    public void SetTested()
    {
        Invalid("BacklogItem kan niet van \"Doing\" naar \"Tested\" gezet worden");
    }

    public void SetTesting()
    {
        Invalid("BacklogItem kan niet van \"Doing\" naar \"Testing\" gezet worden");
    }

    public void setTodo()
    {
        this.backlogItem.SetState(new TodoState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Todo\" gezet");
    }

    private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
}
