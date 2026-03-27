using Soa3Eindopdracht.Domain.Projects;
using System;

namespace Soa3Eindopdracht.Domain.Sprints.Factories
{
    public abstract class SprintFactory
    {
        public Sprint Create(string name, DateTime start, DateTime end, Project project)
        {
            Validate(name, start, end);
            return CreateSprint(name, start, end, project);
        }

        protected abstract Sprint CreateSprint(string name, DateTime start, DateTime end, Project project);

        private void Validate(string name, DateTime start, DateTime end)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Sprint name is required.");

            if (end <= start)
                throw new ArgumentException("End date must be after start date.");
        }
    }
}