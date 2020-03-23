namespace Gen.Tests.Unit.Templating
{
    using Gen.Templating;

    using Xunit;

    public class TemplateScrubberTest
    {
        private readonly TemplateScrubber _templateScrubber;

        public TemplateScrubberTest()
        {
            _templateScrubber = new TemplateScrubber();
        }

        [Fact]
        public void ReplaceVerbatimProjectName()
        {
            Template projectTemplate = TemplateFromSingleFile(
                    File.WithName("Tornado Player.cs")
                        .WithContent("class X { public string ProjName => \"Tornado Player\"; }"));
            Project project = new Project("Tornado Player", projectTemplate);

            Template scrubbedTemplate = _templateScrubber.ScrubProjectTemplate(project);

            Template expectedTemplate = TemplateFromSingleFile(
                    File.WithName("$PROJECT NAME$.cs")
                        .WithContent("class X { public string ProjName => \"$PROJECT NAME$\"; }"));
            Assert.Equal(expectedTemplate, scrubbedTemplate);
        }

        private static Template TemplateFromSingleFile(File file)
        {
            return Template.NewBuilder().AddFile(file).Build();
        }
    }
}