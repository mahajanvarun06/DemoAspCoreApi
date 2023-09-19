using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SampleCore.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleCoreApi.Test
{
    public class ProductsControllerIntegrationTests : IClassFixture<CustomSampleCoreApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTests(CustomSampleCoreApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new System.Uri("https://localhost:5001/")
            });
        }

        [Fact]
        public async Task GetProducts_Should_Return_Products()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/products");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(stringResponse);

            // assert
            Assert.Contains(products, p => p.Name == "Product1");
        }

        [Fact]
        public async Task GetProductById_Should_Return_NotFound()
        {
            var id = 3;
            
            await AddToken();
            var httpResponse = await _client.GetAsync("/api/products/" +id);

            // assert
            Assert.Equal(404, (int)httpResponse.StatusCode);
        }

        [Fact]
        public async Task GetProductById_Should_Return_Product()
        {
            var id = 1;
            
            await AddToken();
            var httpResponse = await _client.GetAsync("/api/products/" + id);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(stringResponse);

            // assert
            Assert.True(product.Name == "Product1");
        }

        private async Task AddToken()
        {
            var httpResponse = await _client.GetAsync("/api/token/");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Token tokenResponse = JsonConvert.DeserializeObject<Token>(stringResponse);

            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.accessToken}");
        }

        [Fact]
        public async Task PostProduct_Should_Insert_Product()
        {
            await AddToken();
            var product = DummyDataDBInitializer.PostSeedingProduct();

            var httpResponse = await _client.PostAsync("/api/products/", ContentHelper.GetStringContent(product));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteProduct_Should_Delete_Product()
        {
            var id = 2;

            await AddToken();
            var httpResponse = await _client.DeleteAsync("/api/products/" +id);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            int result = JsonConvert.DeserializeObject<int>(stringResponse);

            // assert
            Assert.True(result == 1);
        }

        [Fact]
        public async Task DeleteProduct_Should_Return_NotFound()
        {
            var id = 10;

            await AddToken();
            var httpResponse = await _client.DeleteAsync("/api/products/" + id);
            
            // assert
            Assert.Equal(404, (int)httpResponse.StatusCode);
        }
    }
}
