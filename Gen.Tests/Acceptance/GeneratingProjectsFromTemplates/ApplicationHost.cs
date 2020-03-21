namespace Gen.Tests.Acceptance.GeneratingProjectsFromTemplates
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;

    public class ApplicationHost
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public ApplicationHost(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        public async Task Post(string endpoint, string content)
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();

            StringContent stringContent = new StringContent(content);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = stringContent
            };

            await httpClient.SendAsync(request);
        }
    }
}