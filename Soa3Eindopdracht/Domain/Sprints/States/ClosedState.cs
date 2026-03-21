using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public class ClosedState : ISprintState
    {
        public ClosedState(Sprint sprint) { }

        public void UpdateSprint(string name, DateTime start, DateTime end) => Invalid();
        public void SetActive() => Invalid();
        public void SetFinished() => Invalid();
        public void StartReleasePipeline() => Invalid();
        public void SetReleased() => Invalid();
        public void SetClosed() => Invalid();
        public void SetCancelled() => Invalid();

        private void Invalid()
            => Console.WriteLine("Sprint is afgesloten. Geen acties meer mogelijk.");
    }
}