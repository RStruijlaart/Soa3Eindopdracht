using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Pipelines;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;
using Soa3Eindopdracht.Domain.Sprints.States;

namespace So3EindopdrachtTests
{
    public class SprintStateTests
    {
        private Project CreateProject()
        {
            var user = new User(1, "Tester", "123", "test@test.com", 1);
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);
            return new Project("Test project", member);
        }

        // ========================
        // CREATED STATE
        // ========================

        [Fact]
        public void Sprint_FromCreated_ToActive()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();

            Assert.IsType<ActiveState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromCreated_ToCancelled()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Cancel();

            Assert.IsType<CancelledState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromCreated_ToClosed_Invalid()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Close();

            Assert.IsType<CreatedState>(sprint.CurrentState);
        }

        // ========================
        // ACTIVE STATE
        // ========================

        [Fact]
        public void Sprint_FromActive_ToActive_Invalid()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Start();

            Assert.IsType<ActiveState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromActive_ToFinished()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();

            Assert.IsType<FinishedState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromActive_ToCancelled()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Cancel();

            Assert.IsType<CancelledState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromActive_ToClosed_Invalid()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Close();

            Assert.IsNotType<ClosedState>(sprint.CurrentState);
        }

        // ========================
        // FINISHED STATE
        // ========================

        [Fact]
        public void Sprint_FromFinished_ToFinished_Invalid()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.Finish();

            Assert.IsType<FinishedState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromFinished_ToActive_Invalid()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.Start();

            Assert.IsType<FinishedState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromFinished_ToCancelled()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.Cancel();

            Assert.IsType<CancelledState>(sprint.CurrentState);
        }

        [Fact]
        public void Sprint_FromCancelled_NoTransitionsPossible()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Cancel();

            sprint.Start();
            sprint.Finish();
            sprint.Close();

            Assert.IsType<CancelledState>(sprint.CurrentState);
        }

        // ========================
        // REVIEW SPRINT
        // ========================

        [Fact]
        public void ReviewSprint_Finished_ToClosed()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.Close();

            Assert.IsType<ClosedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReviewSprint_Close_Invalid_BeforeFinished()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Close();

            Assert.IsNotType<ClosedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReviewSprint_StartPipeline_Invalid()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.StartReleasePipeline();

            Assert.IsType<FinishedState>(sprint.CurrentState);
        }

        // ========================
        // RELEASE SPRINT
        // ========================

        [Fact]
        public void ReleaseSprint_Pipeline_Success()
        {
            var sprint = new ReleaseSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            var pipelineMock = new Mock<IPipelineComponent>();
            sprint.SetPipeline(pipelineMock.Object);

            sprint.Start();
            sprint.Finish();
            sprint.StartReleasePipeline();

            pipelineMock.Verify(p => p.Execute(), Times.Once);
            Assert.IsType<ReleasedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReleaseSprint_Pipeline_NoPipeline()
        {
            var sprint = new ReleaseSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.StartReleasePipeline();

            Assert.IsType<FinishedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReleaseSprint_Pipeline_BeforeFinish_Invalid()
        {
            var sprint = new ReleaseSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            var pipelineMock = new Mock<IPipelineComponent>();
            sprint.SetPipeline(pipelineMock.Object);

            sprint.Start();
            sprint.StartReleasePipeline();

            Assert.IsNotType<ReleasedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReleaseSprint_Released_ToClosed()
        {
            var sprint = new ReleaseSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            var pipelineMock = new Mock<IPipelineComponent>();
            sprint.SetPipeline(pipelineMock.Object);

            sprint.Start();
            sprint.Finish();
            sprint.StartReleasePipeline();
            sprint.Close();

            Assert.IsType<ClosedState>(sprint.CurrentState);
        }

        [Fact]
        public void ReleaseSprint_Released_StartPipeline_Again_Invalid()
        {
            var sprint = new ReleaseSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            var pipelineMock = new Mock<IPipelineComponent>();
            sprint.SetPipeline(pipelineMock.Object);

            sprint.Start();
            sprint.Finish();
            sprint.StartReleasePipeline();
            sprint.StartReleasePipeline();

            Assert.IsType<ReleasedState>(sprint.CurrentState);
            pipelineMock.Verify(p => p.Execute(), Times.Once);
        }

        [Fact]
        public void Sprint_FromClosed_NoTransitionsPossible()
        {
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), CreateProject());

            sprint.Start();
            sprint.Finish();
            sprint.Close();

            sprint.Start();
            sprint.Cancel();

            Assert.IsType<ClosedState>(sprint.CurrentState);
        }
    }
}