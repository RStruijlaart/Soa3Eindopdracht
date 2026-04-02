using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public class ActiveState : ISprintState
    {
        private readonly Sprint _sprint;

        public ActiveState(Sprint sprint)
        {
            _sprint = sprint;
        }

        public void UpdateSprint(string name, DateTime start, DateTime end)
            => Invalid("Actieve sprint kan niet gewijzigd worden.");

        public void SetActive() => Invalid("Sprint is al actief.");

        public void SetFinished()
        {
            _sprint.CurrentState = new FinishedState(_sprint);
            Console.WriteLine("Sprint is afgerond (Finished).");
            if (_sprint is ReleaseSprint releaseSprint)
            {
                _sprint.StartReleasePipeline();
            }
        }
        public void Finish()
        {
            _sprint.CurrentState = new FinishedState(_sprint);
            Console.WriteLine($"Sprint {_sprint.Name} is afgerond.");

            if (_sprint is ReleaseSprint releaseSprint)
            {
                _sprint.StartReleasePipeline();
            }
        }

        public void StartReleasePipeline() => Invalid("Sprint moet eerst finished zijn.");
        public void SetReleased() => Invalid("Kan niet direct releasen.");

        public void SetClosed() => Invalid("Sprint moet eerst finished zijn.");

        public void SetCancelled()
        {
            _sprint.CurrentState = new CancelledState(_sprint);
            Console.WriteLine("Sprint geannuleerd tijdens uitvoering.");
        }

        private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
    }
}