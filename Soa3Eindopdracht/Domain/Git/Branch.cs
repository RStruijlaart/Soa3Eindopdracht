using Soa3Eindopdracht.Domain.Comment;
using System;
using System.Collections.Generic;

namespace Soa3Eindopdracht.Domain.Git
{
    public class Branch
    {
        public string Name { get; private set; }

        private readonly List<Commit> _commits = new();
        public IReadOnlyList<Commit> Commits => _commits;

        public Branch(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Branch naam is verplicht.");

            Name = name;
        }

        public void AddCommit(Commit commit)
        {
            _commits.Add(commit);
        }
    }
}