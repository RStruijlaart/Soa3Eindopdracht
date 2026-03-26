using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class TestingState : IBacklogItemState
{
    private readonly BacklogItem backlogItem;

    public TestingState(BacklogItem backlogItem)
    {
        this.backlogItem = backlogItem;
    }
    public void SetDoing()
    {
        Invalid("Backlog item kan niet van \"Testing\" naar \"Doing\" gezet worden");
    }

    public void SetDone()
    {
        Invalid("Backlog item kan niet van \"Testing\" naar \"Done\" gezet worden");
    }

    public void SetReadyForTesting()
    {
        Invalid("Backlog item kan niet van \"Testing\" naar \"Ready for Testing\" gezet worden");
    }

    public void SetTested()
    {
        this.backlogItem.SetState(new TestedState(backlogItem));
        this.backlogItem.SendNotificationToScumMaster();
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Tested\" gezet");
    }

    public void SetTesting()
    {
        Invalid("Backlog item staat al op \"Testing\"");
    }

    public void setTodo()
    {
        this.backlogItem.SetState(new TodoState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Todo\" gezet");
    }

    private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
}
