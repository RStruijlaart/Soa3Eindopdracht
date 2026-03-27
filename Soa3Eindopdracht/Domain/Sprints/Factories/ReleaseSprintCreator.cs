using Soa3Eindopdracht.Domain.Projects;
using System;

namespace Soa3Eindopdracht.Domain.Sprints.Factories
{
    public class ReleaseSprintCreator : SprintFactory
    {
        protected override Sprint CreateSprint(string name, DateTime start, DateTime end, Project project)
        {
            return new ReleaseSprint(name, start, end, project);
        }
    }
}