using System;

namespace Soa3Eindopdracht.Domain.Pipelines.Actions
{
    public abstract class PipelineAction : IPipelineComponent
    {
        public abstract void Execute();

        protected void Log(string message)
        {
            Console.WriteLine($"[{GetType().Name}] {message}");
        }
    }
}