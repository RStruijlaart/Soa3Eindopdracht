using Soa3Eindopdracht.Domain.Projects;
using System;

namespace Soa3Eindopdracht.Domain.Sprints.Factories
{
    public class ReviewSprintCreator : SprintFactory
    {
        protected override Sprint CreateSprint(string name, DateTime start, DateTime end, Project project)
        {
            return new ReviewSprint(name, start, end, project);
        }
    }
}