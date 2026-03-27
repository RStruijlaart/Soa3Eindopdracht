using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.Git;
using Soa3Eindopdracht.Domain.Projects;
using Soa3Eindopdracht.Domain.Sprints;
using Xunit;

namespace So3EindopdrachtTests
{
    public class GitTests
    {
        private Project CreateProject()
        {
            var user = new User(1, "Tester", "123", "test@test.com", 1);
            var member = new ProjectMember(user, RoleEnum.DEVELOPER);
            return new Project("Test project", member);
        }

        // ========================
        // GitRepository
        // ========================

        [Fact]
        public void CreateBranch_AddsBranchToRepository()
        {
            var repo = new GitRepository();

            var branch = repo.CreateBranch("develop");

            Assert.Contains(branch, repo.Branches);
            Assert.Equal("develop", branch.Name);
        }

        // ========================
        // Branch
        // ========================

        [Fact]
        public void Branch_AddCommit_AddsCommitToList()
        {
            var project = CreateProject();
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), project);

            var branch = new Branch("feature");
            var commit = new Commit("Initial commit", sprint);

            branch.AddCommit(commit);

            Assert.Contains(commit, branch.Commits);
        }

        // ========================
        // Commit
        // ========================

        [Fact]
        public void Commit_StoresMessageAndSprint()
        {
            var project = CreateProject();
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), project);

            var commit = new Commit("Fix bug", sprint);

            Assert.Equal("Fix bug", commit.Message);
            Assert.Equal(sprint, commit.Sprint);
        }

        [Fact]
        public void Commit_EmptyMessage_ThrowsException()
        {
            var project = CreateProject();
            var sprint = new ReviewSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(1), project);

            Assert.Throws<ArgumentException>(() => new Commit("", sprint));
        }

        [Fact]
        public void Branch_EmptyName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Branch(""));
        }
        [Fact]
        public void Repository_CanContainMultipleBranches()
        {
            var repo = new GitRepository();

            var b1 = repo.CreateBranch("main");
            var b2 = repo.CreateBranch("dev");

            Assert.Equal(2, repo.Branches.Count);
        }
    }
}