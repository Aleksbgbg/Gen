namespace Gen.Tests.Acceptance.GeneratingProjectsFromTemplates
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;

    using Xunit;

    public class CreateTemplateAcceptanceTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string InputProjectsPath =
                "Acceptance/GeneratingProjectsFromTemplates/InputProject";
        private const string ExpectedTemplatesPath =
                "Acceptance/GeneratingProjectsFromTemplates/ExpectedTemplate";
        private const string OutputTemplatesPath = "Templates";

        private readonly ApplicationHost _applicationHost;

        public CreateTemplateAcceptanceTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _applicationHost = new ApplicationHost(webApplicationFactory);
        }

        [Fact]
        public async Task UploadProjectProducesScrubbedTemplate()
        {
            string serializedProject = SerializeInputProjectFiles("AspNetCore");

            await _applicationHost.Post("/template/AspNetCore", serializedProject);

            ExpectDirectoriesExactMatch(
                    source: ExpectedTemplatesPath + "/AspNetCore",
                    target: OutputTemplatesPath + "/AspNetCore");
        }

        private static string SerializeInputProjectFiles(string projectType)
        {
            string[] files = Directory.GetFiles(
                    path: InputProjectsPath + "/" + projectType,
                    searchPattern: string.Empty,
                    searchOption: SearchOption.AllDirectories);

            Assert.True(
                    files.Length > 0,
                    $"No files discovered for the project type {projectType}.");

            StringBuilder serializedProject = new StringBuilder();

            foreach (string file in files)
            {
                string relativeFilename = GetFilenameRelativeToProject(file);

                serializedProject.Append('^')
                                 .AppendLine(relativeFilename)
                                 .Append(File.ReadAllText(file))
                                 .Append('$');
            }

            return serializedProject.ToString();
        }

        private static void ExpectDirectoriesExactMatch(string source, string target)
        {
            foreach (string relativeFilename in Directory
                                                .GetFiles(source)
                                                .Select(GetFilenameRelativeToProject))
            {
                string sourcePath = Path.Combine(source, relativeFilename);
                string targetPath = Path.Combine(target, relativeFilename);

                Assert.Equal(File.ReadAllText(sourcePath), File.ReadAllText(targetPath));
            }
        }

        private static string GetFilenameRelativeToProject(string file)
        {
            return Regex.Replace(
                    input: Path.GetFullPath(file),
                    pattern: @"^.+AspNetCore(?:\\|/)(.+)$",
                    replacement: "$1");
        }
    }
}