using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soa3Eindopdracht.Domain.BacklogItem;
public class TestedState : IBacklogItemState
{
    private readonly BacklogItem backlogItem;

    public TestedState(BacklogItem backlogItem)
    {
        this.backlogItem = backlogItem;
    }

    public void SetDoing()
    {
        Invalid("Backlog item kan niet van \"Tested\" naar \"Doing\" gezet worden");
    }

    public void SetDone()
    {
        this.backlogItem.SetState(new DoneState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Done\" gezet");
    }

    public void SetReadyForTesting()
    {
        this.backlogItem.SetState(new ReadyForTestingState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Ready for Testing\" gezet");
    }

    public void SetTested()
    {
        Invalid("Backlog item staat al op \"Tested\"");
    }

    public void SetTesting()
    {
        Invalid("Backlog item kan niet van \"Tested\" naar \"Testing\" gezet worden");
    }

    public void setTodo()
    {
        this.backlogItem.SetState(new TodoState(backlogItem));
        Console.WriteLine($"Backlog item: {backlogItem.Name} is naar \"Todo\" gezet");
    }

    private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
}
