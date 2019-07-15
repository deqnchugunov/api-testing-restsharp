using ApiTestingRestSharp.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;

namespace ApiTestingRestSharp.Tests
{
    [TestFixture]
    class BaseTests : BaseFixture
    {
        [Test]
        public void GetAllTodoItems()
        {
            // second Todo item from the response
            /*
               {
                "id": 2,
                "name": "Feed the dog",
                "isComplete": false,
                "dateDue": "2019-12-30T00:00:00"
               }
            */

            RestRequest request = new RestRequest();
            request.AddHeader("canAccess", "true");
            request.Resource = todos;
            request.Method = Method.GET;

            var output = client.Execute(request).Content;
            var response = JsonConvert.DeserializeObject<List<Todo>>(output);

            Assert.That(response[1].Id, Is.EqualTo("2"), "Id is not correct");
            Assert.That(response[1].Name, Is.EqualTo("Feed the dog"), "Name is not correct");
            Assert.That(response[1].IsComplete, Is.EqualTo("false"), "isComplete is not correct");
            Assert.That(response[1].DateDue, Is.EqualTo("2019-12-30T00:00:00"), "dateDue is not correct");
        }

        [Test]
        public void GetSingleTodoItem()
        {
            /* 
                 {
                  "id": 1,
                  "name": "Walk the dog",
                  "isComplete": false,
                  "dateDue": "2019-12-31T00:00:00"
                 }
            */

            RestRequest request = new RestRequest();
            request.AddHeader("canAccess", "true");
            request.Resource = todos + "/1";
            request.Method = Method.GET;

            IRestResponse restResponse = client.Execute(request);
            var content = restResponse.Content;
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            Assert.That(restResponse.StatusCode.ToString(), Is.EqualTo("OK"), "Status is not correct");
            Assert.That(response["id"], Is.EqualTo("1"), "Id is not correct");
            Assert.That(response["name"], Is.EqualTo("Walk the dog"), "Name is not correct");
            Assert.That(response["isComplete"], Is.EqualTo("false"), "isComplete is not correct");

        }

        [Test]
        public void GetSingleTodoItemAddUrlSegment()
        {
            /* 
                 {
                  "id": 1,
                  "name": "Walk the dog",
                  "isComplete": false,
                  "dateDue": "2019-12-31T00:00:00"
                 }
            */

            RestRequest request = new RestRequest();
            request.AddHeader("canAccess", "true");
            request.Resource = todos + "/{todoId}";
            request.AddUrlSegment("todoId", "1");
            request.Method = Method.GET;

            IRestResponse restResponse = client.Execute(request);
            var content = restResponse.Content;
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            Assert.That(restResponse.StatusCode.ToString(), Is.EqualTo("OK"), "Status is not correct");
            Assert.That(response["id"], Is.EqualTo("1"), "Id is not correct");
            Assert.That(response["name"], Is.EqualTo("Walk the dog"), "Name is not correct");
            Assert.That(response["isComplete"], Is.EqualTo("false"), "isComplete is not correct");

        }

        [Test]
        public void PostTodoItemAddBodyAnonymous()
        {
            RestRequest request = new RestRequest();
            request.AddHeader("canAccess", "true");
            request.Resource = todos;
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { name = "a2", isComplete = "false", dateDue = "2019-08-10T10:53:53.588Z" });

            IRestResponse restResponse = client.Execute(request);
            var content = restResponse.Content;
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            Assert.That(restResponse.StatusCode.ToString(), Is.EqualTo("Created"), "Status is not correct");
            Assert.That(response["name"], Is.EqualTo("a2"), "Name is not correct");
            Assert.That(response["isComplete"], Is.EqualTo("false"), "isComplete is not correct");
            Assert.That(response["dateDue"], Is.EqualTo("2019-08-10T00:00:00Z"), "dateDue is not correct");
        }

        [Test]
        public void PostTodoItemClassType()
        {
            RestRequest request = new RestRequest();
            request.AddHeader("canAccess", "true");
            request.Resource = todos;
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Todo() { Id = "0", Name = "a31", IsComplete = "false", DateDue = "2019-08-10T10:53:53.588Z" });

            IRestResponse restResponse = client.Execute(request);
            var content = restResponse.Content;
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            Assert.That(restResponse.StatusCode.ToString(), Is.EqualTo("Created"), "Status is not correct");
            Assert.That(response["name"], Is.EqualTo("a31"), "Name is not correct");
            Assert.That(response["isComplete"], Is.EqualTo("false"), "isComplete is not correct");
            Assert.That(response["dateDue"], Is.EqualTo("2019-08-10T00:00:00Z"), "dateDue is not correct");
        }

        [Test]
        public void PostTodoItemGenericDeserialization()
        {
            RestRequest request = new RestRequest();
            request.AddHeader("canAccess", "true");
            request.Resource = todos;
            request.Method = Method.POST;   
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Todo() { Id = "0", Name = "a11", IsComplete = "false", DateDue = "2019-08-10T10:53:53.588Z" });

            var response = client.Execute<Todo>(request).Data;

            Assert.That(response.Name, Is.EqualTo("a11"), "Name is not correct");
            Assert.That(response.IsComplete, Is.EqualTo("False"), "isComplete is not correct");
            Assert.That(response.DateDue, Is.EqualTo("2019-08-10T00:00:00Z"), "dateDue is not correct");
        }
    }
}
