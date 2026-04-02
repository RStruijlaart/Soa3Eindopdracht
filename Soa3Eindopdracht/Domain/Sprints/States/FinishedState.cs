using Soa3Eindopdracht.Domain.Projects;
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

            try
            {
                Console.WriteLine("Pipeline wordt uitgevoerd...");
                _sprint.Pipeline.Execute();
                SetReleased();
            }
            catch (Exception ex)
            {
                Project project = _sprint.Project;
                if (project.ScrumMaster != null)
                {
                    project.ScrumMaster.SendNotification(
                        $"De pipeline voor sprint {_sprint.Name} is gefaald: {ex.Message}",
                        "Pipeline Failure"
                    );
                }

                Console.WriteLine($"Fout: Pipeline uitvoering gefaald. Scrum Master is op de hoogte gesteld.");
            }

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

            Project project = _sprint.Project;
            string message = $"Release voor sprint {_sprint.Name} is geannuleerd.";
            string subject = "Release Geannuleerd";

            if (project.ProductOwner != null)
            {
                project.ProductOwner.SendNotification(message, subject);
            }

            if (project.ScrumMaster != null)
            {
                project.ScrumMaster.SendNotification(message, subject);
            }

            Console.WriteLine("Release geannuleerd. Product Owner en Scrum Master zijn genotificeerd.");
        }

        private void Invalid(string message) => Console.WriteLine($"Fout: {message}");
    }
}