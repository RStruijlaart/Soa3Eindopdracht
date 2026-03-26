using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class ReadyForTestingState : IBacklogItemState
{
    private BacklogItem backlogItem;

    public ReadyForTestingState(BacklogItem backlogItem)
    {
        this.backlogItem = backlogItem;
    }
    public void SetDoing()
    {
        this.backlogItem.SetState(new DoingState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Doing\" gezet");
    }

    public void SetDone()
    {
        Invalid("Backlog item kan niet van \"Ready for Testing\" naar \"Done\" gezet worden");
    }

    public void SetReadyForTesting()
    {
        Invalid("Backlog item staat al op \"Ready for Testing\"");
    }

    public void SetTested()
    {
        Invalid("Backlog item kan niet van \"Ready for Testing\" naar \"Tested\" gezet worden");
    }

    public void SetTesting()
    {
        this.backlogItem.SetState(new TestingState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Testing\" gezet");
    }

    public void setTodo()
    {
        Invalid("Backlog item kan niet van \"Ready for Testing\" naar \"Todo\" gezet worden");
    }

    private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
}
