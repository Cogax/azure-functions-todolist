using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace Functions
{
    public class CqrsFunctions
    {
        private readonly IMediator _mediator;

        public CqrsFunctions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName(nameof(Command))]
        public async Task Command(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "command/{commandName}")]
            HttpRequest req, string commandName)
        {
            var commandType = typeof(ICommand);

            var type = commandType.Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => commandType.IsAssignableFrom(x))
                .FirstOrDefault(x => x.FullName.Contains(commandName));

            var body    = await req.ReadAsStringAsync();
            var cmd     = JsonConvert.DeserializeObject(body, type);

            await _mediator.Send(cmd);
        }

        [FunctionName(nameof(Query))]
        public async Task<IActionResult> Query(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "query/{queryName}")]
            HttpRequest req, string queryName)
        {
            var queryType = typeof(IQuery<>);
            var type = queryType.Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => x.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == queryType))
                .FirstOrDefault(x => x.FullName.Contains(queryName));

            var queryDictionary = req.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
            var queryJson       = JsonConvert.SerializeObject(queryDictionary);
            var query           = JsonConvert.DeserializeObject(queryJson, type);

            var restult = await _mediator.Send(query);

            return new OkObjectResult(restult);
        }
    }
}
