using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MimoFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            // Safely retrieve the "name" query parameter and handle potential null values
            string? name = req.Query["name"].FirstOrDefault();
            
            if (string.IsNullOrEmpty(name))
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult($"Welcome to Azure Functions, {name}!");
        }
    }
}
