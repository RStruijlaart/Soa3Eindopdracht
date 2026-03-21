using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public class CancelledState : ISprintState
    {
        public CancelledState(Sprint sprint) { }

        public void UpdateSprint(string name, DateTime start, DateTime end) => Invalid();
        public void SetActive() => Invalid();
        public void SetFinished() => Invalid();
        public void StartReleasePipeline() => Invalid();
        public void SetReleased() => Invalid();
        public void SetClosed() => Invalid();
        public void SetCancelled() => Invalid();

        private void Invalid()
            => Console.WriteLine("Sprint is geannuleerd. Geen acties meer mogelijk.");
    }
}