using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Planszex.Filters
{
    public class LanguageFilter : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;

        public LanguageFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            if (controller != null)
            {
                

                string ret = string.Empty;
                controller.Request.Cookies.TryGetValue("Language", out ret);
                if (ret==null)
                {
                    controller.Response.Cookies.Append("Language", "PL");
                    controller.ViewData["Language"] = "PL";
                }
                else
                {
                    controller.ViewData["Language"] = ret;
                }
            }
        }
    }
}
