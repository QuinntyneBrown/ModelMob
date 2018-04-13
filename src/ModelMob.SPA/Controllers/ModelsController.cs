using ModelMob.ModelService.Features.Models;
using ModelMob.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ModelMob.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/models")]
    public class ModelsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:44395";

        public ModelsController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [HttpPost]
        public async Task<ActionResult<SaveModelCommand.Response>> Save(SaveModelCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveModelCommand.Response>($"{_baseUrl}/api/models", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Model.ModelId}")]
        public async Task Remove(RemoveModelCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/models/{request.Model.ModelId}");
        }

        [HttpGet("{ModelId}")]
        public async Task<ActionResult<GetModelByIdQuery.Response>> GetById([FromRoute]GetModelByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetModelByIdQuery.Response>($"{_baseUrl}/api/models/{request.ModelId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetModelsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetModelsQuery.Response>($"{_baseUrl}/api/models");
        }
    }
}