using System;

namespace Soa3Eindopdracht.Domain.Sprints.States
{
    public class FinishedState : ISprintState
    {
        private readonly Sprint _sprint;

        public FinishedState(Sprint sprint)
        {
            _sprint = sprint;
        }

        public void UpdateSprint(string name, DateTime start, DateTime end)
            => Invalid("Finished sprint kan niet gewijzigd worden.");

        public void SetActive() => Invalid("Kan niet terug naar Active.");
        public void SetFinished() => Invalid("Sprint is al finished.");

        // 🔥 PIPELINE LOGICA
        public void StartReleasePipeline()
        {
            if (!_sprint.IsReleaseSprint())
            {
                Invalid("Alleen release sprints mogen pipeline starten.");
                return;
            }

            if (_sprint.Pipeline == null)
            {
                Invalid("Geen pipeline gekoppeld.");
                return;
            }

            Console.WriteLine("Pipeline wordt uitgevoerd...");

            _sprint.Pipeline.Execute();

            SetReleased();
        }

        public void SetReleased()
        {
            if (!_sprint.IsReleaseSprint())
            {
                Invalid("Alleen release sprints kunnen gereleased worden.");
                return;
            }

            _sprint.CurrentState = new ReleasedState(_sprint);
            Console.WriteLine("Sprint is gereleased.");
        }

        public void SetClosed()
        {
            if (_sprint.IsReleaseSprint())
            {
                Invalid("Release sprint moet via pipeline naar Released.");
                return;
            }

            _sprint.CurrentState = new ClosedState(_sprint);
            Console.WriteLine("Review sprint gesloten na review.");
        }

        public void SetCancelled()
        {
            _sprint.CurrentState = new CancelledState(_sprint);
            Console.WriteLine("Release geannuleerd.");
        }

        private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
    }
}