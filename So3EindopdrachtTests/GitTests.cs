using Moq;
using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Git;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;
using Xunit;

namespace So3EindopdrachtTests
{
    public class GitTests : BaseTest
    {
        private readonly Project _project;
        private readonly Sprint _sprint;

        public GitTests() : base()
        {
            // Setup basis context voor Git acties
            var user = new User(1, "Dev", "123", "dev@test.nl", 1);
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);
            _project = new Project("DevOps", member);
            _sprint = new ReviewSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7), _project);
        }

        // ============================================================
        // FR-9.1: REPOSITORY & BRANCH MANAGEMENT
        // ============================================================

        [Fact]
        public void GitRepository_ShouldManageMultipleBranches_FR9_1()
        {
            // Arrange
            var repo = _project.Repository;

            // Act
            var master = repo.CreateBranch("master");
            var feature = repo.CreateBranch("feature-login");

            // Assert
            Assert.Equal(2, repo.Branches.Count);
            Assert.Contains(repo.Branches, b => b.Name == "master");
            Assert.Contains(repo.Branches, b => b.Name == "feature-login");
        }

        [Fact]
        public void Branch_ShouldThrowException_OnEmptyName()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Branch(""));
        }

        // ============================================================
        // FR-9.2: COMMITS & TRACEABILITY
        // ============================================================

        [Fact]
        public void Commit_ShouldBeLinkedToASprint_FR9_2()
        {
            // Arrange
            var branch = new Branch("main");
            var commitMessage = "Initial commit voor sprint 1";

            // Act
            var commit = new Commit(commitMessage, _sprint);
            branch.AddCommit(commit);

            // Assert
            Assert.Equal(_sprint, commit.Sprint);
            Assert.Contains(commit, branch.Commits);
            Assert.True(commit.Timestamp <= DateTime.Now);
        }

        [Fact]
        public void Commit_ShouldThrowException_OnEmptyMessage()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Commit("", _sprint));
        }

        [Fact]
        public void Branch_ShouldAllowAddingMultipleCommits()
        {
            // Arrange
            var branch = new Branch("develop");
            var c1 = new Commit("Update 1", _sprint);
            var c2 = new Commit("Update 2", _sprint);

            // Act
            branch.AddCommit(c1);
            branch.AddCommit(c2);

            // Assert
            Assert.Equal(2, branch.Commits.Count);
        }
    }
}