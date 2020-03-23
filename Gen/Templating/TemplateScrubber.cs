namespace Gen.Templating
{
    public class TemplateScrubber
    {
        public Template ScrubProjectTemplate(Project project)
        {
            return Template.NewBuilder()
                           .AddFile(
                                   File.WithName("$PROJECT NAME$")
                                       .WithContent(
                                               project.Template.Files[0]
                                                      .Content.Replace(
                                                              "Tornado Player",
                                                              "$PROJECT NAME$")))
                           .Build();
        }
    }
}