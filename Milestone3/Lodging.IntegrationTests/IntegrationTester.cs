using System;
using System.Net.Http;
using Lodging.WebAPI;
using Xunit;
using Xunit.Abstractions;
namespace Lodging.IntegrationTests
{
    public class IntegrationTester : IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;

        /*
        * <summary>
        * (url, post data) => checks that post is successful with 
        *                     Status Code: 201
        *                     ConentLocation: exists
        *                     ContentLocation: is valid url
        * </summary>
        */
        [MemberData(nameof(StaticTestingData.PostRequests), MemberType = typeof(StaticTestingData))]
        [Theory]
        public async void CheckPostResponse(string url, object data)
        {
            var httpContent = new StringContent(data.ToString());
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //act
            var r = await _client.PostAsync(url, httpContent);

            //assert

            //Status Code Created
            Assert.Equal(System.Net.HttpStatusCode.Created, r.StatusCode);

            //LocationHeader Exists
            Assert.NotNull(r.Content.Headers.ContentLocation);

            //Make Get Request to LocationHeader URL
            var LocationExists = await _client.GetAsync(r.Content.Headers.ContentLocation);

            //LocationHeader Is Valid url
            Assert.Equal(System.Net.HttpStatusCode.OK, LocationExists.StatusCode);

        }
        /*
         * <summary>
         * (url, post data) => checks that post is unsuccessful with
         *                     422 Unprocessable Entity
         *                     
         * </summary>
         */
        [MemberData(nameof(StaticTestingData.Post422Requests), MemberType = typeof(StaticTestingData))]
        [Theory]
        public async void CheckInvalid422PostResponse(string url, string data)
        {
            //arrange
            var httpContent = new StringContent(data);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //act
            var r = await _client.PostAsync(url, httpContent);

            //assert
            //response status code is equal to 422
            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, r.StatusCode);
        }
        /*
        * <summary>
        * (url, post data) => checks that post is unsuccessful with
         *                     409 Conflict
        * </summary>
        */
        [Theory]
        [MemberData(nameof(StaticTestingData.Get409Requests), MemberType = typeof(StaticTestingData))]
        public async void CheckInvalid409PostResponse(string GetUrl, string PostUrl)
        {
            //arange
            var re = await _client.GetAsync(GetUrl);
            var body = re.Content;
            var httpContent = new StringContent(body.ToString());
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //act
            var r = await _client.PostAsync(PostUrl, httpContent);

            //assert
            //response status code is 409
            Assert.Equal(System.Net.HttpStatusCode.Conflict, r.StatusCode);
        }

        /*
        * <summary>
        * (url) =>  checks that get is successful with:
         *                     200 OK
         *                     Content: exists
         *                     ContentType: "application/json; charset=utf-8"
        * </summary>
        */
        [Theory]
        [MemberData(nameof(StaticTestingData.GetRequests), MemberType = typeof(StaticTestingData))]
        public async void CheckGetResponse(string url)
        {
            //act
            var r = await _client.GetAsync(url);

            //assert
            //response status code is OK
            Assert.Equal(System.Net.HttpStatusCode.OK, r.StatusCode);
            //response body is present/has the desired content type
            Assert.NotNull(r.Content);
            Assert.Equal("application/json; charset=utf-8", r.Content.Headers.ContentType.ToString());
        }

        /*
        * <summary>
        * (url) =>  checks that get is unsuccessful with:
         *                     404 Not Found
         *                     
        * </summary>
        */
        [Theory]
        [MemberData(nameof(StaticTestingData.Get404Requests), MemberType = typeof(StaticTestingData))]
        public async void Check404Response(string url)
        {
            //act
            var r = await _client.GetAsync(url);
            Console.WriteLine(await r.Content.ReadAsStringAsync());
            //assert
            //response status code is 404
            Assert.Equal(System.Net.HttpStatusCode.NotFound, r.StatusCode);

        }

        /*
        * <summary>
        * (url) =>  checks that delete is successful with:
         *                     202: Accepted
         *                     
        * </summary>
        */
        [MemberData(nameof(StaticTestingData.DeleteRequests), MemberType = typeof(StaticTestingData))]
        [Theory]
        public async void CheckDeleteResponse(string url)
        {

            //act
            var r = await _client.DeleteAsync(url);

            //assert
            //response status code 202
            Assert.Equal(System.Net.HttpStatusCode.NoContent, r.StatusCode);
        }
        /*
        * <summary>
        * (CustomeWebAppFactoryInMemDB) => client for requests              
        * </summary>
        */
        public IntegrationTester(CustomWebApplicationFactoryInMemDB<Startup> factory)
        {
            _factory = factory;
            var c = _factory.CreateClient();
            this._client = c;
        }

    }
}
