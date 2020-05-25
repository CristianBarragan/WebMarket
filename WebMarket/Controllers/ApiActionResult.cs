using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Controllers
{
    [ExcludeFromCodeCoverage]
    public class ApiActionResult : IActionResult
    {
        readonly ApiResponse _response;

        public ApiActionResult(ApiResponse response)
        {
            _response = response;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(JsonConvert.SerializeObject(_response))
            {
                StatusCode = (int)_response.StatusCode
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
