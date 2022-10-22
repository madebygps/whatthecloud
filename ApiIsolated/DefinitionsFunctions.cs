using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ApiIsolated
{
    public class DefinitionsFunctions
    {
        private readonly ILogger _logger;

        private readonly DefinitionsRepository _repository;

        public DefinitionsFunctions(ILoggerFactory loggerFactory, DefinitionsRepository repository)
        {
            _logger = loggerFactory.CreateLogger<DefinitionsFunctions>();
            _repository = repository;
        }

        [Function("GetDefinitions")]
        public async Task<HttpResponseData> GetDefinitions([HttpTrigger(AuthorizationLevel.Function, "get", Route = "definitions")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            var definitions = await _repository.GetDefinitionsAsync();
            string jsonString = JsonSerializer.Serialize(definitions);
            response.WriteString(jsonString);
            return response;
        }
        [Function("GetDefinition")]
        public async Task<HttpResponseData> GetDefinition([HttpTrigger(AuthorizationLevel.Function, "get", Route = "definitions/{id}")] HttpRequestData req, string id)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            var definition = await _repository.GetDefinitionAsync(id);
            string jsonString = JsonSerializer.Serialize(definition);
            response.WriteString(jsonString);
            return response;
        }

        [Function("CreateDefinition")]
        public async Task<HttpResponseData> CreateDefinition([HttpTrigger(AuthorizationLevel.Function, "post", Route = "definitions")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;
            var streamReader = new StreamReader(request);
            var newDefinition = JsonSerializer.Deserialize<Definition>(streamReader.ReadToEnd());
            await _repository.CreateAsync(newDefinition);
            string jsonString = JsonSerializer.Serialize(newDefinition);
            response.WriteString(jsonString);
            return response;
        }
        [Function("DeleteDefinition")]
        public async Task<HttpResponseData> DeleteDefinition([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "definitions/{id}")] HttpRequestData req, string id)
        {
            var response = req.CreateResponse(HttpStatusCode.NoContent);
            await _repository.RemoveAsync(id);
            return response;
        }

        [Function("UpdateDefinition")]
        public async Task<HttpResponseData> UpdateDefinition(
       [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "definitions/{id}")] HttpRequestData req, string id)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            var existingDefinition = await _repository.GetDefinitionAsync(id);
            if (existingDefinition == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            var request = req.Body;
            var streamReader = new StreamReader(request);
            var updatedDefinition = JsonSerializer.Deserialize<Definition>(streamReader.ReadToEnd());

            await _repository.UpdateAsync(id, updatedDefinition);
            return response;
        }
    }
}
