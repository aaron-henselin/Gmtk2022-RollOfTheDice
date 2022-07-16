using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RollTheDiceGmtk2022.Game;
using RollTheDiceGmtk2022Logic;

namespace RollTheDiceGmtk2022SolutionInjestor
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly CosmosClient client;
        public Function1(ILogger<Function1> log, CosmosClient client)
        {
            _logger = log;
            this.client = client;
        }

        [FunctionName("Injest")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Injest(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Dictionary<int,Card> data = JsonConvert.DeserializeObject<Dictionary<int, Card>>(requestBody);

            var parameters = req.GetQueryParameterDictionary();
            var scenarioId = Convert.ToInt32(parameters["scenario"]);
            var registrar = new ScenarioRegistrar();
            var scenario = registrar.Scenarios[scenarioId];
            var gs2 = new GameState(scenario, data);
            var outcome = gs2.AdvanceGameStateUntilCompletion(50);
            if (outcome.Won)
            {
                var result = new SavedResult
                {
                    IpAddress = req.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Turns = outcome.EndingTurn
                };
                await Save(result);
            }

            return new OkObjectResult(outcome);
        }


        [FunctionName("Histogram")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Histogram([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            var parameters = req.GetQueryParameterDictionary();
            var scenarioId = Convert.ToInt32(parameters["scenario"]);

            var results = await GetSavedResultsAsync(scenarioId);
            return new OkObjectResult(results);

        }

        public async Task Save(SavedResult newScore)
        {
            var existing = await GetSavedResultsAsync(newScore.ScenarioId, newScore.IpAddress);
            if (!existing.Any())
            {
                await Create(newScore);
            }
            else
            {
                if (existing.All(x => x.Turns > newScore.Turns))
                    await Replace(newScore);
            }

        }


        async Task Create(SavedResult queueItem)
        {
            var container = client.GetContainer("GMTK", "SavedResult");
            await container.CreateItemAsync(queueItem, new PartitionKey(queueItem.ScenarioId));
        }
        async Task Replace(SavedResult queueItem)
        {
            var container = client.GetContainer("GMTK", "SavedResult");
            await container.ReplaceItemAsync(queueItem, queueItem.Id, new PartitionKey(queueItem.ScenarioId));
        }


        public async Task<List<SavedResult>> GetSavedResultsAsync(int scenarioId)
        {
            var sqlQueryText = $"SELECT * FROM c WHERE c.ScenarioId = @scenarioId";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            queryDefinition.WithParameter("scenarioId",scenarioId);
            return await QueryItemsAsync(queryDefinition);
        }
        public async Task<List<SavedResult>> GetSavedResultsAsync(int scenarioId, string ipAddress)
        {
            var sqlQueryText = $"SELECT * FROM c WHERE c.ScenarioId = @scenarioId and c.IpAddress = @ipAddress";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            queryDefinition.WithParameter("scenarioId", scenarioId);
            queryDefinition.WithParameter("ipAddress", ipAddress);
            return await QueryItemsAsync(queryDefinition);
        }

        /// <summary>
        /// Run a query (using Azure Cosmos DB SQL syntax) against the container
        /// </summary>
        async Task<List<SavedResult>> QueryItemsAsync(QueryDefinition queryDefinition)
        {
             
            var container = client.GetContainer("GMTK", "SavedResult");

            using FeedIterator<SavedResult> queryResultSetIterator = container.GetItemQueryIterator<SavedResult>(queryDefinition);

            var queueItems = new List<SavedResult>();
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<SavedResult> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (SavedResult queueItem in currentResultSet)
                {
                    queueItems.Add(queueItem);
                }
            }

            return queueItems;
        }

    }

    public class SavedResult
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public int ScenarioId { get; set; }
        public string IpAddress { get; set; }
        public int Turns { get; set; }
    }


    public class InjestorStartup : IWebJobsStartup
    {
        private readonly IConfiguration configuration;

        public InjestorStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(IWebJobsBuilder builder)
        {
            var sc = builder.Services;
            ConfigureServices(sc);
        }


        public void ConfigureServices(IServiceCollection sc)
        {
          
            var cosmosDbConnectionString = configuration.GetConnectionString("CosmosDb");
            sc.AddSingleton(new CosmosClient(cosmosDbConnectionString));
  
        }
    }
}

