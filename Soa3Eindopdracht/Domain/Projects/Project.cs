using System;
using System.Collections.Generic;
using Soa3Eindopdracht.Domain.Sprints;
using Soa3Eindopdracht.Domain.Git;

namespace Soa3Eindopdracht.Domain.Projects
{
    public class Project
    {
        public string Name { get; private set; }

        private readonly List<Sprint> _sprints = new();
        public IReadOnlyList<Sprint> Sprints => _sprints;

        public GitRepository Repository { get; private set; }

        public Project(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Projectnaam is verplicht.");

            Name = name;
            Repository = new GitRepository();
        }

        public void AddSprint(Sprint sprint)
        {
            _sprints.Add(sprint);
        }

        public void RemoveSprint(Sprint sprint)
        {
            _sprints.Remove(sprint);
        }
    }
}