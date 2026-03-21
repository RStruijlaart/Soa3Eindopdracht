using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public class CreatedState : ISprintState
    {
        private readonly Sprint _sprint;

        public CreatedState(Sprint sprint)
        {
            _sprint = sprint;
        }

        public void UpdateSprint(string name, DateTime start, DateTime end)
        {
            _sprint.UpdateInternal(name, start, end);
            Console.WriteLine("Sprint bijgewerkt in Created state.");
        }

        public void SetActive()
        {
            _sprint.CurrentState = new ActiveState(_sprint);
            Console.WriteLine("Sprint gestart.");
        }

        public void SetFinished() => Invalid("Sprint moet eerst actief zijn.");
        public void StartReleasePipeline() => Invalid("Pipeline kan nog niet gestart worden.");
        public void SetReleased() => Invalid("Kan niet releasen vanuit Created.");

        public void SetClosed() => Invalid("Sprint moet eerst uitgevoerd worden.");

        public void SetCancelled()
        {
            _sprint.CurrentState = new CancelledState(_sprint);
            Console.WriteLine("Sprint geannuleerd.");
        }

        private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
    }
}