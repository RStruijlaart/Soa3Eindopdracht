namespace Soa3Eindopdracht.Domain.Pipelines.Actions
{
    public class AnalyseAction : PipelineAction
    {
        public override void Execute()
        {
            Log("Code analyse uitvoeren (Sonar)...");
        }
    }
}