using System;
using System.IO;
using Soa3Eindopdracht.Domain.Pipelines;
using Soa3Eindopdracht.Domain.Pipelines.Actions;
using Xunit;

namespace So3EindopdrachtTests
{
    public class PipelineTests
    {
        [Fact]
        public void Pipeline_Executes_All_Actions_In_Order()
        {
            var pipeline = new PipelineComposite();
            pipeline.Add(new SourceAction());
            pipeline.Add(new BuildAction());
            pipeline.Add(new TestAction());

            var sw = new StringWriter();
            Console.SetOut(sw);

            pipeline.Execute();

            var output = sw.ToString();

            Assert.Contains("=== Pipeline gestart ===", output);
            Assert.Contains("[SourceAction] Source code ophalen...", output);
            Assert.Contains("[BuildAction] Build uitvoeren...", output);
            Assert.Contains("[TestAction] Tests uitvoeren...", output);
            Assert.Contains("=== Pipeline afgerond ===", output);
        }

        [Fact]
        public void Pipeline_Executes_Nested_Pipelines()
        {
            var parent = new PipelineComposite();
            var child = new PipelineComposite();

            child.Add(new BuildAction());
            child.Add(new TestAction());

            parent.Add(new SourceAction());
            parent.Add(child);

            var sw = new StringWriter();
            Console.SetOut(sw);

            parent.Execute();

            var output = sw.ToString();

            Assert.Contains("[SourceAction] Source code ophalen...", output);
            Assert.Contains("[BuildAction] Build uitvoeren...", output);
            Assert.Contains("[TestAction] Tests uitvoeren...", output);
        }

        [Fact]
        public void Pipeline_Remove_Component_Should_Not_Execute_It()
        {
            var pipeline = new PipelineComposite();
            var build = new BuildAction();

            pipeline.Add(new SourceAction());
            pipeline.Add(build);
            pipeline.Remove(build);

            var sw = new StringWriter();
            Console.SetOut(sw);

            pipeline.Execute();

            var output = sw.ToString();

            Assert.DoesNotContain("[BuildAction] Build uitvoeren...", output);
        }

        [Fact]
        public void Pipeline_Empty_Should_Still_Start_And_End()
        {
            var pipeline = new PipelineComposite();

            var sw = new StringWriter();
            Console.SetOut(sw);

            pipeline.Execute();

            var output = sw.ToString();

            Assert.Contains("=== Pipeline gestart ===", output);
            Assert.Contains("=== Pipeline afgerond ===", output);
        }

        [Fact]
        public void Pipeline_Executes_All_Action_Types()
        {
            var pipeline = new PipelineComposite();

            pipeline.Add(new SourceAction());
            pipeline.Add(new PackageAction());
            pipeline.Add(new BuildAction());
            pipeline.Add(new TestAction());
            pipeline.Add(new AnalyseAction());
            pipeline.Add(new UtilityAction());
            pipeline.Add(new DeployAction());

            var sw = new StringWriter();
            Console.SetOut(sw);

            pipeline.Execute();

            var output = sw.ToString();

            Assert.Contains("[SourceAction] Source code ophalen...", output);
            Assert.Contains("[PackageAction] Packages installeren...", output);
            Assert.Contains("[BuildAction] Build uitvoeren...", output);
            Assert.Contains("[TestAction] Tests uitvoeren...", output);
            Assert.Contains("[AnalyseAction] Code analyse uitvoeren (Sonar)...", output);
            Assert.Contains("[UtilityAction] Utility taak uitvoeren...", output);
            Assert.Contains("[DeployAction] Deployment uitvoeren...", output);
        }
    }
}