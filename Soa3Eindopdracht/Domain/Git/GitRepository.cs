using System.Collections.Generic;

namespace Soa3Eindopdracht.Domain.Git
{
    public class GitRepository
    {
        private readonly List<Branch> _branches = new();

        public IReadOnlyList<Branch> Branches => _branches;

        public Branch CreateBranch(string name)
        {
            var branch = new Branch(name);
            _branches.Add(branch);
            return branch;
        }
    }
}