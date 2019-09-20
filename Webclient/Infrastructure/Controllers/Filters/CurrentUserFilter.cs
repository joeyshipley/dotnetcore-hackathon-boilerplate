using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BOS.Webclient.Infrastructure.Controllers.Filters
{
    public class CurrentUserFilter : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserFilter(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            var user = _httpContextAccessor.HttpContext.User;
            
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(id))
            {
                throw new Exception("Unable to determine the logged in user. Please try to log back in.");
                //base.OnActionExecuting(filterContext);
                //return;
            }

            var applicationUserId = Guid.Parse(id);
            filterContext.RouteData.Values.Add("userId", applicationUserId);

            base.OnActionExecuting(filterContext);
        }
    }
}