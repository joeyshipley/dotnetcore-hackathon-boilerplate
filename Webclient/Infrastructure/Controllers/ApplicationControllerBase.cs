using System.Net;
using BOS.Webclient.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace BOS.Webclient.Infrastructure.Controllers
{
    public class ApplicationControllerBase : Controller
    {
        public LoggedInRequest UserRequest()
        {
            var request = LoggedInRequest.For().Populate(RouteData);
            return request;
        }
        
        protected JsonResult Result(VoidInteractorResult interactorResult) 
        {
            return Result(HttpStatusCode.OK, interactorResult);
        }

        protected JsonResult Result<T>(InteractorResult<T> interactorResult) 
            where T : class
        {
            return Result(HttpStatusCode.OK, interactorResult);
        }

        protected JsonResult Result<T>(HttpStatusCode status, InteractorResult<T> interactorResult) 
            where T : class
        {
            var response = new
            {
                ActionSuccess = !interactorResult.Messages.Any(),
                Messages = interactorResult.Messages,
                Data = interactorResult.Data
            };
            var result = new JsonResult(response) 
            {
                StatusCode = (int) status
            };
            return result;
        }
    }
}