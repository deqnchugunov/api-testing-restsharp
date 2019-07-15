using NUnit.Framework;
using RestSharp;

namespace ApiTestingRestSharp
{
    [SetUpFixture]
    public class BaseFixture
    {
        protected string baseUrl = "http://localhost:8088";
        protected string todos = "/api/todo";

        protected IRestClient client;

        [OneTimeSetUp]
        public void ClassInit()
        {
            client = new RestClient(baseUrl);
        }
    }
}
