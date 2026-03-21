using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public class ReleasedState : ISprintState
    {
        private readonly Sprint _sprint;

        public ReleasedState(Sprint sprint)
        {
            _sprint = sprint;
        }

        public void UpdateSprint(string name, DateTime start, DateTime end)
            => Invalid("Released sprint kan niet gewijzigd worden.");

        public void SetActive() => Invalid("Kan niet terug.");
        public void SetFinished() => Invalid("Al afgerond.");
        public void StartReleasePipeline() => Invalid("Pipeline al uitgevoerd.");
        public void SetReleased() => Invalid("Sprint is al gereleased.");

        public void SetClosed()
        {
            _sprint.CurrentState = new ClosedState(_sprint);
            Console.WriteLine("Sprint definitief gesloten.");
        }

        public void SetCancelled()
            => Invalid("Released sprint kan niet meer geannuleerd worden.");

        private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
    }
}