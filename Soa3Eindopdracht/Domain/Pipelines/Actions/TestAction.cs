namespace Soa3Eindopdracht.Domain.Pipelines.Actions
{
    public class TestAction : PipelineAction
    {
        public override void Execute()
        {
            Log("Tests uitvoeren...");
        }
    }
}