using Soa3Eindopdracht.Domain.Git;
using Soa3Eindopdracht.Domain.Sprints;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Soa3Eindopdracht.Domain.Projects
{
    public class Project
    {
        public string Name { get; private set; }
        public IBacklog Backlog { get; private set; }
        public List<ProjectMember> Developers { get; private set; } = [];
        public List<ProjectMember> Testers { get; private set; } = [];
        public ProjectMember? ScrumMaster { get; private set; }
        public ProjectMember? ProductOwner { get; private set; }

        private readonly List<Sprint> _sprints = new();
        public IReadOnlyList<Sprint> Sprints => _sprints;

        public GitRepository Repository { get; private set; }

        public Project(string name, ProjectMember projectCreator)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Projectnaam is verplicht.");

            Name = name;
            Repository = new GitRepository();
            Backlog = new ProjectBacklog(this);
            AddProjectMember(projectCreator);
        }

        public void AddSprint(Sprint sprint)
        {
            _sprints.Add(sprint);
        }

        public void RemoveSprint(Sprint sprint)
        {
            _sprints.Remove(sprint);
        }

        public void AddProjectMember(ProjectMember member)
        {
            switch (member.Role)
            {
                // Project.cs aanpassen:
                case RoleEnum.SCRUM_MASTER:
                    if (ScrumMaster == null) // Verander != naar ==
                    {
                        ScrumMaster = member;
                        Console.WriteLine($"Set {member.User.Name} as Scrum Master!");
                    }
                    else
                    {
                        Console.WriteLine("There is already a Scrum Master!");
                    }
                    break;

                case RoleEnum.PRODUCT_OWNER:
                    if (ProductOwner == null) // Verander != naar ==
                    {
                        ProductOwner = member;
                        Console.WriteLine($"Set {member.User.Name} as Product Owner");
                    }
                    else
                    {
                        Console.WriteLine("There is already a Product Owner");
                    }
                    break;
                case RoleEnum.DEVELOPER:
                    Developers.Add(member);
                    Console.WriteLine($"Added {member.User.Name} as a Developer");
                    break;
                case RoleEnum.TESTER:
                    Testers.Add(member);
                    Console.WriteLine($"Added {member.User.Name} as a Tester");
                    break;
                default:
                    Console.WriteLine("Unknown Role");
                    break;
            }
        }
    }
}