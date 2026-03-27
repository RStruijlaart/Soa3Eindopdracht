using Soa3Eindopdracht.Domain;
using Soa3Eindopdracht.Domain.BacklogItem;
using Soa3Eindopdracht.Domain.Projects;

namespace So3EindopdrachtTests
{
    public class BacklogItemStateTests
    {
        [Fact]
        public void BacklogItemStateChange_FromTodoToTodo()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            backlogItem.setTodo();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item staat al op \"Todo\" naar \"Todo\"", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTodoToDoing()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            backlogItem.SetDoing();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is nu in \"Doing\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTodoToDone()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            backlogItem.SetDone();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: BacklogItem kan niet van \"Todo\" naar \"Done\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTodoToReadyForTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            backlogItem.SetReadyForTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Todo\" naar \"Ready for Testing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTodoToTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            backlogItem.SetTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Todo\" naar \"Testing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTodoToTested()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            //Act
            backlogItem.SetTested();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Todo\" naar \"Tested\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoingToDoing()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDoing();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: BacklogItem staat al op \"Doing\" naar \"Doing\"", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoingToDone()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDone();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: BacklogItem kan niet van \"Doing\" naar \"Done\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoingToReadyForTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetReadyForTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Ready for Testing\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoingToTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: BacklogItem kan niet van \"Doing\" naar \"Testing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoingToTested()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTested();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: BacklogItem kan niet van \"Doing\" naar \"Tested\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoingToTodo()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog ProjectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", ProjectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.setTodo();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Todo\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromReadyForTestingToDoing()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDoing();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Doing\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromReadyForTestingToDone()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDone();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Ready for Testing\" naar \"Done\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromReadyForTestingToReadyForTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetReadyForTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item staat al op \"Ready for Testing\"", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromReadyForTestingToTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Testing\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromReadyForTestingToTested()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTested();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Ready for Testing\" naar \"Tested\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromReadyForTestingToTodo()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.setTodo();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Ready for Testing\" naar \"Todo\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestingToDoing()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDoing();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Testing\" naar \"Doing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestingToDone()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDone();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Testing\" naar \"Done\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestingToReadyForTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetReadyForTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Testing\" naar \"Ready for Testing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestingToTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item staat al op \"Testing\"", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestingToTested()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTested();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Tested\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestingToTodo()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.setTodo();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Todo\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestedToDoing()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDoing();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Tested\" naar \"Doing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestedToDone()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDone();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Done\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestedToReadyForTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetReadyForTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Ready for Testing\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestedToTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Tested\" naar \"Testing\" gezet worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestedToTested()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTested();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item staat al op \"Tested\"", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromTestedToTodo()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.setTodo();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Backlog item: Test is naar \"Todo\" gezet", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoneToDoing()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            backlogItem.SetDone();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDoing();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Done\" gehaalt worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoneToDone()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            backlogItem.SetDone();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetDone();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Done\" gehaalt worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoneToReadyForTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            backlogItem.SetDone();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetReadyForTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Done\" gehaalt worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoneToTesting()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            backlogItem.SetDone();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTesting();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Done\" gehaalt worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoneToTested()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            backlogItem.SetDone();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.SetTested();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Done\" gehaalt worden", output);
        }

        [Fact]
        public void BacklogItemStateChange_FromDoneToTodo()
        {
            //Arrange
            User user = new(1, "Tester", "0123456789", "test@test.nl", 1);
            ProjectMember projectMember = new(user, RoleEnum.DEVELOPER);
            Project project = new("Test project", projectMember);
            ProjectBacklog projectBacklog = new(project);
            BacklogItem backlogItem = new("Test", "Test", projectBacklog);

            var sw = new StringWriter();
            Console.SetOut(sw);

            backlogItem.SetDoing();
            backlogItem.SetReadyForTesting();
            backlogItem.SetTesting();
            backlogItem.SetTested();
            backlogItem.SetDone();
            sw.GetStringBuilder().Clear();

            //Act
            backlogItem.setTodo();

            //Assert
            var output = sw.ToString().Trim();
            Assert.Equal("Fout: Backlog item kan niet van \"Done\" gehaalt worden", output);
        }
    }
}