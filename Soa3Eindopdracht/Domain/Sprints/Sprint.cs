using Soa3Eindopdracht.Domain.Pipelines;
using Soa3Eindopdracht.Domain.Sprints.States;
using System;
using Soa3Eindopdracht.Domain.Reports;

namespace Soa3Eindopdracht.Domain.Sprints
{
    public abstract class Sprint
    {
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Backlog Backlog { get; private set; } = new();

        public ISprintState CurrentState { get; set; }

        public IPipelineComponent Pipeline { get; private set; }

        // 🔥 Belangrijk voor later (pipeline)
        public bool HasPipeline { get; protected set; }

        protected Sprint(string name, DateTime start, DateTime end)
        {
            ValidateDates(start, end);

            Name = name;
            StartDate = start;
            EndDate = end;

            CurrentState = new CreatedState(this);
        }

        // ======================
        // STATE TRIGGERS
        // ======================

        public void Start() => CurrentState.SetActive();

        public void Edit(string name, DateTime start, DateTime end)
        {
            ValidateDates(start, end);
            CurrentState.UpdateSprint(name, start, end);
        }

        public void Finish() => CurrentState.SetFinished();

        public void StartReleasePipeline() => CurrentState.StartReleasePipeline();

        public void Close() => CurrentState.SetClosed();

        public void Cancel() => CurrentState.SetCancelled();

        // ======================
        // DOMAIN LOGIC
        // ======================

        protected void UpdateInternal(string name, DateTime start, DateTime end)
        {
            Name = name;
            StartDate = start;
            EndDate = end;
        }

        private void ValidateDates(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("End date must be after start date.");
        }

        // ======================
        // TYPE BEHAVIOR
        // ======================

        public abstract bool IsReleaseSprint();

        public abstract bool GenerateReport();

        public void SetPipeline(IPipelineComponent pipeline)
        {
            if (!HasPipeline)
                throw new InvalidOperationException("Deze sprint ondersteunt geen pipeline.");

            Pipeline = pipeline;
        }
        public virtual Report GenerateReportObject()
        {
            return new Report(Name, $"Rapport voor sprint van {StartDate} tot {EndDate}");
        }




    }
}