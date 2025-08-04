using Business.Models;
using RestSharp;
using System.Text.Json;

namespace Tests.API
{
    [TestFixture]
    [Category("APITests")]
    [Parallelizable(ParallelScope.All)]
    public class ApiTests : TestBaseApi
    {
        [Test]
        public async Task Task1_GetUsersList_ShouldReturnValidUsers()
        {
            var request = _apiClient.CreateRequest()
                .WithResource("users")
                .WithMethod(Method.Get)
                .Build();

            var response = await _apiClient.ExecuteAsync(request);

            _logger.Info($"Response received with status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            _logger.Info("Checking if content is not null or empty...");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);

            var users = JsonSerializer.Deserialize<List<User>>(response.Content);

            Assert.That(users, Is.Not.Null.And.Not.Empty);

            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    _logger.Info($"Validating user: {user.Name}...");
                    Assert.That(user.Id, Is.GreaterThan(0));

                    _logger.Info($"Validating user name: {user.Name}...");
                    Assert.That(user.Name, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user username: {user.Username}...");
                    Assert.That(user.Username, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user email: {user.Email}...");
                    Assert.That(user.Email, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user address: {user.Address}...");
                    Assert.That(user.Address, Is.Not.Null);

                    _logger.Info($"Validating user phone: {user.Phone}...");
                    Assert.That(user.Phone, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user website: {user.Website}...");
                    Assert.That(user.Website, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user company: {user.Company}...");
                    Assert.That(user.Company, Is.Not.Null);
                });
            }
        }

        [Test]
        public async Task Task2_ValidateResponseHeader_ContentTypeIsJson()
        {
            var request = _apiClient.CreateRequest()
                .WithResource("users")
                .WithMethod(Method.Get)
                .Build();

            var response = await _apiClient.ExecuteAsync(request);

            _logger.Info($"Response received with status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            _logger.Info("Checking if content is not null or empty...");
            Assert.That(response.Headers, Is.Not.Null);

            _logger.Info("Checking content-type from response.ContentType...");
            Assert.That(response.ContentType, Is.Not.Null.And.Not.Empty, "Brak ContentType w odpowiedzi.");

            _logger.Info("Validating content-type header value...");
            Assert.That(response.ContentType, Does.StartWith("application/json"), $"Oczekiwano application/json, otrzymano: {response.ContentType}");
        }

        [Test]
        public async Task Task3_ValidateUsersArray()
        {
            var request = _apiClient.CreateRequest()
                .WithResource("users")
                .WithMethod(Method.Get)
                .Build();

            var response = await _apiClient.ExecuteAsync(request);

            _logger.Info($"Response received with status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            _logger.Info("Checking if content is not null or empty...");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);

            _logger.Info("Deserializing response content to List<User>...");
            var users = JsonSerializer.Deserialize<List<User>>(response.Content);
            Assert.That(users, Has.Count.EqualTo(10));

            _logger.Info("Validating distinct user IDs...");
            var distinctIds = users.Select(u => u.Id).Distinct().Count();
            Assert.That(distinctIds, Is.EqualTo(10));

            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    _logger.Info($"Validating user: {user.Name}...");
                    Assert.That(user.Name, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user username: {user.Username}...");
                    Assert.That(user.Username, Is.Not.Null.And.Not.Empty);

                    _logger.Info($"Validating user email: {user.Email}...");
                    Assert.That(user.Company, Is.Not.Null);

                    if (user.Company != null)
                    {
                        _logger.Info($"Validating user company name: {user.Company.Name}...");
                        Assert.That(user.Company.Name, Is.Not.Null.And.Not.Empty);
                    }
                    else
                    {
                        _logger.Warn("User company is null.");
                    }
                });
            }
        }

        [Test]
        public async Task Task4_CreateUser_ShouldReturnCreatedUserWithId()
        {
            var newUser = new
            {
                name = "Test User",
                username = "testuser123"
            };

            var request = _apiClient.CreateRequest()
                .WithResource("users")
                .WithMethod(Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(newUser)
                .Build();

            var response = await _apiClient.ExecuteAsync(request);

            _logger.Info($"Response received with status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            _logger.Info("Checking if content is not null or empty...");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);

            _logger.Info("Parsing response content to JSON document...");
            using var doc = JsonDocument.Parse(response.Content);
            Assert.That(doc.RootElement.TryGetProperty("id", out var idElement), Is.True);

            _logger.Info($"Validating created user ID: {idElement.GetInt32()}...");
            Assert.That(idElement.GetInt32(), Is.GreaterThan(0));
        }

        [Test]
        public async Task Task5_RequestInvalidEndpoint_ShouldReturn404()
        {
            var request = _apiClient.CreateRequest()
                .WithResource("invalidendpoint")
                .WithMethod(Method.Get)
                .Build();

            var response = await _apiClient.ExecuteAsync(request);

            _logger.Info($"Response received with status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));

            _logger.Info("Checking if content is not null...");
            Assert.That(response.Content, Is.Not.Null);
        }
    }
}
