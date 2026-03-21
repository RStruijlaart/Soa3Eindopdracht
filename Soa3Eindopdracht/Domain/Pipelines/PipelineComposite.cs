using System;
using System.Collections.Generic;

namespace Soa3Eindopdracht.Domain.Pipelines
{
    public class PipelineComposite : IPipelineComponent
    {
        private readonly List<IPipelineComponent> _components = new();

        public void Add(IPipelineComponent component)
        {
            _components.Add(component);
        }

        public void Remove(IPipelineComponent component)
        {
            _components.Remove(component);
        }

        public void Execute()
        {
            Console.WriteLine("=== Pipeline gestart ===");

            foreach (var component in _components)
            {
                component.Execute();
            }

            Console.WriteLine("=== Pipeline afgerond ===");
        }
    }
}