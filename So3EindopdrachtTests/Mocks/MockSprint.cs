using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;

public class MockSprint : Sprint
{
    public MockSprint(Project project)
        : base("MockSprint", DateTime.Now, DateTime.Now.AddDays(1), project)
    {
    }

    public override bool IsReleaseSprint()
    {
        return false;
    }

    public override bool GenerateReport()
    {
        return false;
    }
}