using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public interface ISprintState
    {
        void UpdateSprint(string name, DateTime start, DateTime end);

        void SetActive();
        void SetFinished();

        void StartReleasePipeline();

        void SetReleased();
        void SetClosed();
        void SetCancelled();
    }
}